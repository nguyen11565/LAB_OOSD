using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using eShopping.Data;

namespace shopping.Forms
{
    public partial class FrmDangNhap_DangKy : Form
    {
        public FrmDangNhap_DangKy()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string u = txtLoginUser.Text.Trim();
            string p = txtLoginPass.Text.Trim();

            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dt = Db.Query(
                    "SELECT a.customer_id, c.full_name, c.email FROM ACCOUNT a JOIN CUSTOMER c ON a.customer_id=c.customer_id WHERE a.username=@u AND a.password_hash=@p AND a.is_active=1",
                    new SqlParameter("@u", u),
                    new SqlParameter("@p", p));

                if (dt.Rows.Count > 0)
                {
                    FrmTrangChu_DanhMuc.CurrentCustomerId = dt.Rows[0]["customer_id"].ToString();
                    MessageBox.Show("Đăng nhập thành công! Chào mừng " + dt.Rows[0]["full_name"], "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoten = txtDK_HoTen.Text.Trim();
            string cmnd = txtDK_CMND.Text.Trim();
            string diachi = txtDK_DiaChi.Text.Trim();
            string sdt = txtDK_SDT.Text.Trim();
            string email = txtDK_Email.Text.Trim();
            string user = txtDK_User.Text.Trim();
            string pass = txtDK_Pass.Text.Trim();

            if (string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(cmnd) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin bắt buộc!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newCustId = "CUST-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            string newAccId = "ACC-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            using (var conn = Db.OpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var cmdCust = new SqlCommand(
                        "INSERT INTO CUSTOMER (customer_id, full_name, dob, identity_no, address, phone, email) VALUES (@id, @name, @dob, @cmnd, @addr, @phone, @email)", conn, tx);
                    cmdCust.Parameters.AddWithValue("@id", newCustId);
                    cmdCust.Parameters.AddWithValue("@name", hoten);
                    cmdCust.Parameters.AddWithValue("@dob", dtDK_NgaySinh.Value.Date);
                    cmdCust.Parameters.AddWithValue("@cmnd", cmnd);
                    cmdCust.Parameters.AddWithValue("@addr", diachi);
                    cmdCust.Parameters.AddWithValue("@phone", sdt);
                    cmdCust.Parameters.AddWithValue("@email", email);
                    cmdCust.ExecuteNonQuery();

                    var cmdAcc = new SqlCommand(
                        "INSERT INTO ACCOUNT (account_id, customer_id, username, password_hash, is_active) VALUES (@aid, @cid, @u, @p, 1)", conn, tx);
                    cmdAcc.Parameters.AddWithValue("@aid", newAccId);
                    cmdAcc.Parameters.AddWithValue("@cid", newCustId);
                    cmdAcc.Parameters.AddWithValue("@u", user);
                    cmdAcc.Parameters.AddWithValue("@p", pass);
                    cmdAcc.ExecuteNonQuery();

                    tx.Commit();

                    FrmTrangChu_DanhMuc.CurrentCustomerId = newCustId;
                    MessageBox.Show("Tạo tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (SqlException sqlex)
                {
                    tx.Rollback();
                    if (sqlex.Number == 2627 || sqlex.Number == 2601)
                        MessageBox.Show("Tên đăng nhập, số CMND hoặc Email đã tồn tại trên hệ thống!", "Trùng lặp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Lỗi tạo tài khoản: " + sqlex.Message);
                }
            }
        }
    }
}