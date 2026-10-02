using System;
using System.Data;
using System.Windows.Forms;
using eShopping.Models;

namespace shopping.Forms
{
    public partial class FrmGioHang : Form
    {
        public FrmGioHang()
        {
            InitializeComponent();
        }

        private void FrmGioHang_Load(object sender, EventArgs e)
        {
            HienThiGioHang();
        }

        void HienThiGioHang()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã SP");
            dt.Columns.Add("Tên sản phẩm");
            dt.Columns.Add("Đơn giá", typeof(decimal));
            dt.Columns.Add("Số lượng", typeof(int));
            dt.Columns.Add("Thành tiền", typeof(decimal));

            foreach (var item in FrmTrangChu_DanhMuc.GlobalCart.Items)
            {
                dt.Rows.Add(
                    item.Product.ProductId,
                    item.Product.ProductName,
                    item.Product.CurrentPrice,
                    item.Quantity,
                    item.SubTotal
                );
            }

            dgvGioHang.DataSource = dt;

            decimal total = FrmTrangChu_DanhMuc.GlobalCart.TotalAmount;
            lblTongTien.Text = "Tổng tiền hàng: " + total.ToString("N0") + " VNĐ";

            // Kiểm tra gợi ý ưu đãi Freeship theo quy tắc Giáng Sinh (BR04, BR05)
            if (total >= 5000000m)
            {
                lblKhuyenMaiFreeship.Text = "🎉 Chúc mừng! Đơn hàng của bạn đủ điều kiện MIỄN PHÍ CPN TRONG NGÀY (24H)!";
                lblKhuyenMaiFreeship.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
            }
            else if (total >= 1000000m)
            {
                lblKhuyenMaiFreeship.Text = "🎉 Chúc mừng! Đơn hàng của bạn đủ điều kiện MIỄN PHÍ CHUYỂN PHÁT NHANH (1-2 ngày)!";
                lblKhuyenMaiFreeship.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
            }
            else
            {
                decimal thieu = 1000000m - total;
                lblKhuyenMaiFreeship.Text = $"💡 Mua thêm {thieu:N0} đ để được MIỄN PHÍ VẬN CHUYỂN Chuyển phát nhanh!";
                lblKhuyenMaiFreeship.ForeColor = System.Drawing.Color.FromArgb(180, 80, 0);
            }
        }

        private void btnTang_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null) return;
            string pId = dgvGioHang.CurrentRow.Cells["Mã SP"].Value.ToString();
            var item = FrmTrangChu_DanhMuc.GlobalCart.Items.Find(i => i.Product.ProductId == pId);
            if (item != null)
            {
                FrmTrangChu_DanhMuc.GlobalCart.UpdateQuantity(pId, item.Quantity + 1);
                HienThiGioHang();
            }
        }

        private void btnGiam_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null) return;
            string pId = dgvGioHang.CurrentRow.Cells["Mã SP"].Value.ToString();
            var item = FrmTrangChu_DanhMuc.GlobalCart.Items.Find(i => i.Product.ProductId == pId);
            if (item != null)
            {
                FrmTrangChu_DanhMuc.GlobalCart.UpdateQuantity(pId, item.Quantity - 1);
                HienThiGioHang();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null) return;
            string pId = dgvGioHang.CurrentRow.Cells["Mã SP"].Value.ToString();
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này khỏi giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FrmTrangChu_DanhMuc.GlobalCart.UpdateQuantity(pId, 0);
                HienThiGioHang();
            }
        }

        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (FrmTrangChu_DanhMuc.GlobalCart.Items.Count == 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang trống! Vui lòng chọn sản phẩm trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Quy tắc BR02: Bắt buộc đăng nhập trước khi tính tiền
            if (string.IsNullOrEmpty(FrmTrangChu_DanhMuc.CurrentCustomerId))
            {
                MessageBox.Show("Quý khách cần đăng nhập hệ thống trước khi tiến hành tính tiền!", "Yêu cầu đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (var fLogin = new FrmDangNhap_DangKy())
                {
                    if (fLogin.ShowDialog(this) == DialogResult.OK)
                    {
                        // Sau khi đăng nhập thành công, chuyển tiếp sang bước giao hàng
                        using (var fShip = new FrmThongTinGiaoHang())
                        {
                            fShip.ShowDialog(this);
                            HienThiGioHang();
                        }
                    }
                }
            }
            else
            {
                using (var fShip = new FrmThongTinGiaoHang())
                {
                    fShip.ShowDialog(this);
                    HienThiGioHang();
                }
            }
        }
    }
}