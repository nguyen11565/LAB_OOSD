using System;
using System.Windows.Forms;

namespace shopping.Forms
{
    public partial class FrmXacNhanDonHang : Form
    {
        public FrmXacNhanDonHang()
        {
            InitializeComponent();
        }

        public FrmXacNhanDonHang(
            string orderId,
            string customerName,
            string email,
            string recipientName,
            string recipientAddress,
            string methodId,
            decimal totalAmount,
            string cardType,
            string cardLast4) : this()
        {
            lblLoiCamOn.Text = $"Cảm ơn Quý khách {customerName} đã mua sắm tại Cửa hàng ABC!";
            lblMaDonHang.Text = $"MÃ ĐƠN HÀNG: #{orderId}";
            lblThongBaoEmail.Text = $"Biên nhận chi tiết đã được gửi an toàn đến email: {email} (BR12)";

            lblNguoiNhan.Text = "Người nhận: " + recipientName;
            lblDiaChiNhan.Text = "Địa chỉ: " + recipientAddress;

            string tenHinhThuc = "Đặt hàng thường (3-5 ngày)";
            if (methodId == "EXPRESS") tenHinhThuc = "Chuyển phát nhanh (1-2 ngày)";
            if (methodId == "SAME_DAY") tenHinhThuc = "Chuyển phát nhanh trong ngày (24h)";
            lblHinhThuc.Text = "Hình thức: " + tenHinhThuc;

            lblTongTien.Text = $"TỔNG TIỀN ĐÃ TRẢ: {totalAmount:N0} VNĐ";
            lblTheMasked.Text = $"Thẻ thanh toán: {cardType} **** **** **** {cardLast4}";
        }

        private void btnVeTrangChu_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}