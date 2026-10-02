using System;
using System.Text.RegularExpressions;
using eShopping.Models;

namespace eShopping.Services
{
    public class PaymentGatewayService
    {
        public (bool IsValid, string ErrorMessage) ValidateCreditCard(CreditCardInfo card)
        {
            if (card == null) return (false, "Thông tin thẻ không tồn tại.");
            if (string.IsNullOrWhiteSpace(card.CardholderName))
                return (false, "Họ tên chủ thẻ không được để trống.");

            string cleanNumber = card.CardNumber?.Replace("-", "").Replace(" ", "") ?? "";

            switch (card.CardType.ToUpper())
            {
                case "VISA":
                case "MASTERCARD":
                case "DISCOVER":
                    if (!Regex.IsMatch(cleanNumber, @"^\d{16}$"))
                        return (false, $"{card.CardType} yêu cầu số thẻ phải gồm đúng 16 chữ số.");
                    if (!Regex.IsMatch(card.CSV ?? "", @"^\d{3}$"))
                        return (false, $"{card.CardType} yêu cầu mã bảo mật CSV gồm đúng 3 chữ số.");
                    break;

                case "AMEX":
                    if (!Regex.IsMatch(cleanNumber, @"^\d{15}$"))
                        return (false, "American Express yêu cầu số thẻ phải gồm đúng 15 chữ số.");
                    if (!Regex.IsMatch(card.CSV ?? "", @"^\d{4}$"))
                        return (false, "American Express yêu cầu mã bảo mật CSV gồm đúng 4 chữ số.");
                    break;

                default:
                    return (false, "Loại thẻ tín dụng không được hỗ trợ.");
            }

            var now = DateTime.Now;
            if (card.ExpYear < now.Year || (card.ExpYear == now.Year && card.ExpMonth < now.Month))
                return (false, "Thẻ tín dụng đã hết hạn sử dụng.");

            return (true, string.Empty);
        }

        public decimal CalculatePaymentFee(string cardType, decimal subTotal)
        {
            decimal feeRate = 0m;
            switch (cardType.ToUpper())
            {
                case "VISA": feeRate = 0.015m; break;
                case "MASTERCARD": feeRate = 0.018m; break;
                case "DISCOVER": feeRate = 0.020m; break;
                case "AMEX": feeRate = 0.025m; break;
            }
            return Math.Round(subTotal * feeRate, 0);
        }

        public (bool Approved, string GatewayRef, string Error) ProcessPayment(CreditCardInfo card, decimal amount)
        {
            var validation = ValidateCreditCard(card);
            if (!validation.IsValid) return (false, null, validation.ErrorMessage);

            string cleanNumber = card.CardNumber.Replace("-", "").Replace(" ", "");
            if (cleanNumber.EndsWith("9999"))
                return (false, null, "Giao dịch bị từ chối: Thẻ không đủ hạn mức hoặc bị ngân hàng khóa tạm thời.");

            string authCode = "GW-AUTH-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return (true, authCode, null);
        }
    }
}