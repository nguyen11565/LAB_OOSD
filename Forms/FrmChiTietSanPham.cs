using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using eShopping.Data;
using eShopping.Models;

namespace shopping.Forms
{
    public partial class FrmChiTietSanPham : Form
    {
        public string SelectedProductId { get; set; }
        private Product currentProduct = null;

        public FrmChiTietSanPham()
        {
            InitializeComponent();
        }

        public FrmChiTietSanPham(string productId) : this()
        {
            SelectedProductId = productId;
        }

        private void FrmChiTietSanPham_Load(object sender, EventArgs e)
        {
            LoadProductDetails();
        }

        void LoadProductDetails()
        {
            if (string.IsNullOrEmpty(SelectedProductId)) return;

            try
            {
                DataTable dt = Db.Query("SELECT * FROM PRODUCT WHERE product_id = @id", new SqlParameter("@id", SelectedProductId));
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    currentProduct = new Product
                    {
                        ProductId = r["product_id"].ToString(),
                        ProductName = r["product_name"].ToString(),
                        Manufacturer = r["manufacturer"].ToString(),
                        CurrentPrice = Convert.ToDecimal(r["current_price"]),
                        InStock = Convert.ToBoolean(r["in_stock"]),
                        Description = r["description"].ToString(),
                        TechnicalSpecs = r["technical_specs"].ToString()
                    };

                    lblTenSP.Text = currentProduct.ProductName;
                    lblMaSP.Text = "Mã sản phẩm: " + currentProduct.ProductId;
                    lblHangSX.Text = "Nhà sản xuất: " + currentProduct.Manufacturer;
                    lblGiaBan.Text = "Giá bán: " + currentProduct.CurrentPrice.ToString("N0") + " VNĐ";

                    if (currentProduct.InStock)
                    {
                        lblTinhTrang.Text = "Tình trạng: CÒN HÀNG";
                        lblTinhTrang.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
                        btnThemGio.Enabled = true;
                    }
                    else
                    {
                        lblTinhTrang.Text = "Tình trạng: TẠM HẾT HÀNG";
                        lblTinhTrang.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
                        btnThemGio.Enabled = false;
                        btnThemGio.Text = "Tạm hết hàng";
                    }

                    txtMoTa.Text = currentProduct.Description;
                    txtThongSo.Text = currentProduct.TechnicalSpecs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy thông tin: " + ex.Message);
            }
        }

        private void btnThemGio_Click(object sender, EventArgs e)
        {
            if (currentProduct == null || !currentProduct.InStock) return;

            int qty = (int)numSoLuong.Value;
            FrmTrangChu_DanhMuc.GlobalCart.AddItem(currentProduct, qty);
            MessageBox.Show($"Đã thêm {qty} sản phẩm '{currentProduct.ProductName}' vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}