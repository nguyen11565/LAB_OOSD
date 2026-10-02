using System;
using System.Data;
using System.Data.SqlClient;
using eShopping.Data;
using eShopping.Models;

namespace eShopping.Services
{
    public class OrderService
    {
        private readonly PaymentGatewayService _paymentService = new PaymentGatewayService();
        private readonly EmailService _emailService = new EmailService();

        public (bool Success, string Message, string OrderId) CheckoutAndPay(
            string customerId,
            string customerName,
            string customerEmail,
            Cart cart,
            string methodId,
            string recipientName,
            string recipientAddress,
            string recipientPhone,
            decimal shippingFee,
            CreditCardInfo card)
        {
            if (cart.Items.Count == 0)
                return (false, "Giỏ hàng rỗng, không thể thanh toán.", null);

            // 1. Tính toán chi phí
            decimal subTotal = cart.TotalAmount;
            decimal paymentFee = _paymentService.CalculatePaymentFee(card.CardType, subTotal);
            decimal totalAmount = subTotal + shippingFee + paymentFee;

            // 2. Thẩm định và trừ tiền qua Cổng thanh toán (BR10)
            var payResult = _paymentService.ProcessPayment(card, totalAmount);
            if (!payResult.Approved)
            {
                return (false, payResult.Error, null);
            }

            // 3. Thực hiện Transaction lưu Đơn hàng và Chi tiết vào CSDL (BR11)
            string orderId = "ORD-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string transactionId = Guid.NewGuid().ToString();
            string cleanCard = card.CardNumber.Replace("-", "").Replace(" ", "");
            string cardLast4 = cleanCard.Substring(cleanCard.Length - 4);

            using (var conn = Db.OpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    // 3.1. Chèn bảng ORDERS
                    var cmdOrder = new SqlCommand(@"
                        INSERT INTO ORDERS (order_id, customer_id, method_id, order_date, recipient_name, recipient_address, recipient_phone, sub_total, shipping_fee, payment_fee, total_amount, order_status)
                        VALUES (@oid, @cid, @mid, GETDATE(), @rname, @raddr, @rphone, @sub, @ship, @payfee, @total, 'PAID')", conn, tx);

                    cmdOrder.Parameters.AddWithValue("@oid", orderId);
                    cmdOrder.Parameters.AddWithValue("@cid", customerId);
                    cmdOrder.Parameters.AddWithValue("@mid", methodId);
                    cmdOrder.Parameters.AddWithValue("@rname", recipientName);
                    cmdOrder.Parameters.AddWithValue("@raddr", recipientAddress);
                    cmdOrder.Parameters.AddWithValue("@rphone", recipientPhone);
                    cmdOrder.Parameters.AddWithValue("@sub", subTotal);
                    cmdOrder.Parameters.AddWithValue("@ship", shippingFee);
                    cmdOrder.Parameters.AddWithValue("@payfee", paymentFee);
                    cmdOrder.Parameters.AddWithValue("@total", totalAmount);
                    cmdOrder.ExecuteNonQuery();

                    // 3.2. Chèn bảng ORDER_DETAIL
                    foreach (var item in cart.Items)
                    {
                        var cmdDetail = new SqlCommand(@"
                            INSERT INTO ORDER_DETAIL (order_id, product_id, quantity, unit_price)
                            VALUES (@oid, @pid, @qty, @price)", conn, tx);

                        cmdDetail.Parameters.AddWithValue("@oid", orderId);
                        cmdDetail.Parameters.AddWithValue("@pid", item.Product.ProductId);
                        cmdDetail.Parameters.AddWithValue("@qty", item.Quantity);
                        cmdDetail.Parameters.AddWithValue("@price", item.Product.CurrentPrice);
                        cmdDetail.ExecuteNonQuery();
                    }

                    // 3.3. Chèn bảng PAYMENT_TRANSACTION
                    var cmdPay = new SqlCommand(@"
                        INSERT INTO PAYMENT_TRANSACTION (transaction_id, order_id, card_type, card_last4, cardholder_name, amount, transaction_fee, gateway_ref, created_at, status)
                        VALUES (@tid, @oid, @type, @last4, @holder, @amt, @fee, @gw, GETDATE(), 'SUCCESS')", conn, tx);

                    cmdPay.Parameters.AddWithValue("@tid", transactionId);
                    cmdPay.Parameters.AddWithValue("@oid", orderId);
                    cmdPay.Parameters.AddWithValue("@type", card.CardType.ToUpper());
                    cmdPay.Parameters.AddWithValue("@last4", cardLast4);
                    cmdPay.Parameters.AddWithValue("@holder", card.CardholderName.ToUpper());
                    cmdPay.Parameters.AddWithValue("@amt", totalAmount);
                    cmdPay.Parameters.AddWithValue("@fee", paymentFee);
                    cmdPay.Parameters.AddWithValue("@gw", payResult.GatewayRef);
                    cmdPay.ExecuteNonQuery();

                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return (false, "Lỗi ghi nhận đơn hàng: " + ex.Message, null);
                }
            }

            // 4. Gửi email xác nhận bảo mật (BR12, BR13)
            _emailService.SendSecureOrderConfirmation(
                customerEmail, customerName, orderId, cart, totalAmount,
                recipientName, recipientAddress, card.CardType, cardLast4);

            return (true, "Đặt hàng và thanh toán thành công!", orderId);
        }
    }
}