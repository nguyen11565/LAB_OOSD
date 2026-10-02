using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using eShopping.Data;
using eShopping.Models;

namespace shopping.Forms
{
    public partial class FrmTrangChu_DanhMuc : Form
    {
        // Giỏ hàng dùng chung trong toàn bộ phiên mua sắm
        public static Cart GlobalCart = new Cart();
        public static string CurrentCustomerId = null; // Lưu mã khách khi đã đăng nhập

        public FrmTrangChu_DanhMuc()
        {
            InitializeComponent();
        }

        private void FrmTrangChu_DanhMuc_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
            CapNhatNutGioHang();
        }

        void LoadCategories()
        {
            try
            {
                DataTable dt = Db.Query("SELECT category_id, category_name FROM PRODUCT_CATEGORY");
                DataRow dr = dt.NewRow();
                dr["category_id"] = "ALL";
                dr["category_name"] = "-- Tất cả sản phẩm --";
                dt.Rows.InsertAt(dr, 0);

                cboNhomSP.DataSource = dt;
                cboNhomSP.DisplayMember = "category_name";
                cboNhomSP.ValueMember = "category_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
            }
        }

        void LoadProducts(string categoryId = "ALL", string keyword = "")
        {
            try
            {
                string sql = @"SELECT product_id AS [Mã SP], product_name AS [Tên sản phẩm], 
                               manufacturer AS [Nhà sản xuất], current_price AS [Giá bán (VNĐ)], 
                               CASE WHEN in_stock = 1 THEN N'Còn hàng' ELSE N'Tạm hết hàng' END AS [Tình trạng]
                               FROM PRODUCT WHERE 1=1";

                if (categoryId != "ALL" && !string.IsNullOrEmpty(categoryId))
                {
                    sql += " AND category_id = '" + categoryId + "'";
                }

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " AND (product_name LIKE N'%" + keyword + "%' OR product_id LIKE '%" + keyword + "%')";
                }

                dgvSanPham.DataSource = Db.Query(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message);
            }
        }

        void CapNhatNutGioHang()
        {
            int totalQty = 0;
            foreach (var item in GlobalCart.Items) totalQty += item.Quantity;
            btnGioHang.Text = $"🛒 Giỏ hàng ({totalQty})";
        }

        private void cboNhomSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhomSP.SelectedValue != null)
            {
                LoadProducts(cboNhomSP.SelectedValue.ToString(), txtTimKiem.Text.Trim());
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string catId = cboNhomSP.SelectedValue != null ? cboNhomSP.SelectedValue.ToString() : "ALL";
            LoadProducts(catId, txtTimKiem.Text.Trim());
        }

        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;

            string status = dgvSanPham.CurrentRow.Cells["Tình trạng"].Value.ToString();
            if (status == "Tạm hết hàng")
            {
                MessageBox.Show("Sản phẩm này hiện đang tạm hết hàng, quý khách vui lòng chọn món khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pId = dgvSanPham.CurrentRow.Cells["Mã SP"].Value.ToString();
            string pName = dgvSanPham.CurrentRow.Cells["Tên sản phẩm"].Value.ToString();
            decimal price = Convert.ToDecimal(dgvSanPham.CurrentRow.Cells["Giá bán (VNĐ)"].Value);

            var product = new Product
            {
                ProductId = pId,
                ProductName = pName,
                CurrentPrice = price,
                InStock = true
            };

            GlobalCart.AddItem(product, 1);
            CapNhatNutGioHang();
            MessageBox.Show($"Đã thêm '{pName}' vào giỏ hàng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGioHang_Click(object sender, EventArgs e)
        {
            using (var f = new FrmGioHang())
            {
                f.ShowDialog(this);
                CapNhatNutGioHang();
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;
            string pId = dgvSanPham.CurrentRow.Cells["Mã SP"].Value.ToString();
            using (var f = new FrmChiTietSanPham(pId))
            {
                f.ShowDialog(this);
                CapNhatNutGioHang();
            }
        }

        private void btnDangNhapNav_Click(object sender, EventArgs e)
        {
            using (var f = new FrmDangNhap_DangKy())
            {
                f.ShowDialog(this);
            }
        }
    }
}