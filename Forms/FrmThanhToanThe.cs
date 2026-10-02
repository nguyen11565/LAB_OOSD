using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using eShopping.Data;
using eShopping.Models;
using eShopping.Services;

namespace shopping.Forms
{
    public partial class FrmThanhToanThe : Form
    {
        private readonly string _methodId;
        private readonly string _recipientName;
        private readonly string _recipientAddress;
        private readonly string _recipientPhone;
        private readonly decimal _shippingFee;

        private readonly OrderService _orderService = new OrderService();
        private readonly PaymentGatewayService _paySvc = new PaymentGatewayService();

        public FrmThanhToanThe()
        {
            InitializeComponent();
        }

        public FrmThanhToanThe(string methodId, string rName, string rAddr, string rPhone, decimal shipFee) : this()
        {
            _methodId = methodId;
            _recipientName = rName;
            _recipientAddress = rAddr;
            _recipientPhone = rPhone;
            _shippingFee = shipFee;
        }

        private void FrmThanhToanThe_Load(object sender, EventArgs e)
        {
            cboLoaiThe.SelectedIndex = 0;
            CapNhatTongTien();
        }

        string LayLoaiThe()
        {
            if (cboLoaiThe.SelectedIndex == 1) return "MASTERCARD";
            if (cboLoaiThe.SelectedIndex == 2) return "DISCOVER";
            if (cboLoaiThe.SelectedIndex == 3) return "AMEX";
            return "VISA";
        }

        void CapNhatTongTien()
        {
            decimal subTotal = FrmTrangChu_DanhMuc.GlobalCart.TotalAmount;
            decimal payFee = _paySvc.CalculatePaymentFee(LayLoaiThe(), subTotal);
            decimal total = subTotal + _shippingFee + payFee;

            lblTienHang.Text = $"Tiền hàng: {subTotal:N0} đ";
            lblTienShip.Text = $"Phí ship: {_shippingFee:N0} đ";
            lblPhiThe.Text = $"Phí thẻ: {payFee:N0} đ";
            lblTongThanhToan.Text = $"TỔNG TIỀN TRỪ THẺ: {total:N0} đ";
        }

        private void cboLoaiThe_SelectedIndexChanged(object sender, EventArgs e) => CapNhatTongTien();

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoThe.Text) || string.IsNullOrWhiteSpace(txtChuThe.Text) || string.IsNullOrWhiteSpace(txtCSV.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin thẻ tín dụng!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(txtExpThang.Text, out int m);
            int.TryParse(txtExpNam.Text, out int y);

            var card = new CreditCardInfo
            {
                CardType = LayLoaiThe(),
                CardNumber = txtSoThe.Text.Trim(),
                CardholderName = txtChuThe.Text.Trim(),
                ExpMonth = m,
                ExpYear = y,
                CSV = txtCSV.Text.Trim()
            };

            // Lấy thông tin khách mua
            string custName = "Khách Hàng";
            string custEmail = "khachhang@gmail.com";
            try
            {
                DataTable dt = Db.Query("SELECT full_name, email FROM CUSTOMER WHERE customer_id=@id",
                    new SqlParameter("@id", FrmTrangChu_DanhMuc.CurrentCustomerId));
                if (dt.Rows.Count > 0)
                {
                    custName = dt.Rows[0]["full_name"].ToString();
                    custEmail = dt.Rows[0]["email"].ToString();
                }
            }
            catch { }

            // Gọi Transaction thực hiện đặt hàng, trừ tiền và gửi email
            var result = _orderService.CheckoutAndPay(
                FrmTrangChu_DanhMuc.CurrentCustomerId,
                custName,
                custEmail,
                FrmTrangChu_DanhMuc.GlobalCart,
                _methodId,
                _recipientName,
                _recipientAddress,
                _recipientPhone,
                _shippingFee,
                card);

            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Giao dịch không thành công", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string cleanCard = card.CardNumber.Replace("-", "").Replace(" ", "");
            string cardLast4 = cleanCard.Substring(cleanCard.Length - 4);

            // Mở màn hình xác nhận đơn hàng
            using (var fSuccess = new FrmXacNhanDonHang(
                result.OrderId,
                custName,
                custEmail,
                _recipientName,
                _recipientAddress,
                _methodId,
                FrmTrangChu_DanhMuc.GlobalCart.TotalAmount + _shippingFee + _paySvc.CalculatePaymentFee(card.CardType, FrmTrangChu_DanhMuc.GlobalCart.TotalAmount),
                card.CardType,
                cardLast4))
            {
                fSuccess.ShowDialog(this);
            }

            // Xóa sạch giỏ hàng sau khi hoàn tất mua sắm
            FrmTrangChu_DanhMuc.GlobalCart.Clear();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnQuayLai_Click(object sender, EventArgs e) => Close();
    }
}