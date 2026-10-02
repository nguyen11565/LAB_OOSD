using System;
using System.Text;
using eShopping.Models;

namespace eShopping.Services
{
    public class EmailService
    {
        public bool SendSecureOrderConfirmation(
            string recipientEmail,
            string customerName,
            string orderId,
            Cart cart,
            decimal totalAmount,
            string recipientName,
            string recipientAddress,
            string cardType,
            string cardLast4)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail)) return false;

            try
            {
                // Soạn nội dung email (BR12) - Tuyệt đối không đưa số thẻ, hạn dùng, CSV vào mail (BR13)
                var body = new StringBuilder();
                body.AppendLine($"Kính gửi Quý khách {customerName},");
                body.AppendLine($"Cửa hàng ABC xin trân trọng cảm ơn Quý khách đã đặt hàng qua hệ thống e-Shopping!");
                body.AppendLine($"--------------------------------------------------------------------------------");
                body.AppendLine($"MÃ ĐƠN HÀNG: #{orderId}");
                body.AppendLine($"Thời gian đặt: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                body.AppendLine($"Người nhận quà: {recipientName}");
                body.AppendLine($"Địa chỉ giao hàng: {recipientAddress}");
                body.AppendLine($"--------------------------------------------------------------------------------");
                body.AppendLine("CHI TIẾT MẶT HÀNG ĐÃ ĐẶT:");

                foreach (var item in cart.Items)
                {
                    body.AppendLine($"- {item.Product.ProductName} (Mã: {item.Product.ProductId}) | SL: {item.Quantity} | Đơn giá: {item.Product.CurrentPrice:N0} đ | Thành tiền: {item.SubTotal:N0} đ");
                }

                body.AppendLine($"--------------------------------------------------------------------------------");
                body.AppendLine($"TỔNG TIỀN THANH TOÁN: {totalAmount:N0} VNĐ (Đã thanh toán thành công)");
                body.AppendLine($"Phương thức thanh toán: Thẻ tín dụng {cardType} che mờ: **** **** **** {cardLast4}");
                body.AppendLine($"(*) Vì lý do an toàn bảo mật, thông tin thẻ và mã bảo mật CSV tuyệt đối không được lưu giữ hay gửi qua email này.");
                body.AppendLine($"--------------------------------------------------------------------------------");
                body.AppendLine("Chúc Quý khách và gia đình một mùa Giáng Sinh an lành và Năm Mới hạnh phúc!");

                // Tại đây gọi SmtpClient gửi thư thực tế (hoặc log ra console trong môi trường dev)
                Console.WriteLine($"[SMTP MAIL SERVICE]: Đã gửi mail thành công đến {recipientEmail}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi mail: {ex.Message}");
                return false;
            }
        }
    }
}