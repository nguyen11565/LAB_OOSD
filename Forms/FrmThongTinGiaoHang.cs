using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using eShopping.Data;
using eShopping.Services;

namespace shopping.Forms
{
    public partial class FrmThongTinGiaoHang : Form
    {
        readonly ShippingCalculator _shipCalc = new ShippingCalculator();
        private decimal currentShipFee = 0;

        public FrmThongTinGiaoHang()
        {
            InitializeComponent();
        }

        private void FrmThongTinGiaoHang_Load(object sender, EventArgs e)
        {
            cboKhuVuc.SelectedIndex = 0;
            NapThongTinNguoiMua();
            CapNhatChiPhi();
        }

        void NapThongTinNguoiMua()
        {
            if (string.IsNullOrEmpty(FrmTrangChu_DanhMuc.CurrentCustomerId)) return;

            try
            {
                DataTable dt = Db.Query("SELECT full_name, phone, address FROM CUSTOMER WHERE customer_id=@id",
                    new SqlParameter("@id", FrmTrangChu_DanhMuc.CurrentCustomerId));
                if (dt.Rows.Count > 0 && radNguoiMua.Checked)
                {
                    txtNguoiNhan.Text = dt.Rows[0]["full_name"].ToString();
                    txtSDT.Text = dt.Rows[0]["phone"].ToString();
                    txtDiaChi.Text = dt.Rows[0]["address"].ToString();
                }
            }
            catch { }
        }

        string LayMaLoaiPhieu()
        {
            if (radTrongNgay.Checked) return "SAME_DAY";
            if (radCPN.Checked) return "EXPRESS";
            return "STANDARD";
        }

        void CapNhatChiPhi()
        {
            decimal subTotal = FrmTrangChu_DanhMuc.GlobalCart.TotalAmount;
            string method = LayMaLoaiPhieu();
            string region = cboKhuVuc.SelectedItem != null ? cboKhuVuc.SelectedItem.ToString() : "Nội thành TP.HCM / Hà Nội";

            currentShipFee = _shipCalc.CalculateShippingFee(subTotal, method, region);

            lblTienHang.Text = $"Tiền hàng: {subTotal:N0} đ";
            lblPhiShip.Text = currentShipFee == 0 ? "Phí ship: MIỄN PHÍ" : $"Phí ship: {currentShipFee:N0} đ";
            lblTongCong.Text = $"TỔNG CỘNG: {(subTotal + currentShipFee):N0} đ";
        }

        private void radCheDoNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (radNguoiMua.Checked)
            {
                NapThongTinNguoiMua();
            }
            else
            {
                txtNguoiNhan.Clear();
                txtSDT.Clear();
                txtDiaChi.Clear();
            }
        }

        private void radLoaiPhieu_CheckedChanged(object sender, EventArgs e) => CapNhatChiPhi();
        private void cboKhuVuc_SelectedIndexChanged(object sender, EventArgs e) => CapNhatChiPhi();

        private void btnSangThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNguoiNhan.Text) || string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin người nhận hàng!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var fPay = new FrmThanhToanThe(
                LayMaLoaiPhieu(),
                txtNguoiNhan.Text.Trim(),
                txtDiaChi.Text.Trim() + " (" + cboKhuVuc.Text + ")",
                txtSDT.Text.Trim(),
                currentShipFee))
            {
                if (fPay.ShowDialog(this) == DialogResult.OK)
                {
                    Close();
                }
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e) => Close();
    }
}