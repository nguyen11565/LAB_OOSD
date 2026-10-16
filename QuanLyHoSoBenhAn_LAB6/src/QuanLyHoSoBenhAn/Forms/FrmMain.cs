using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmMain : Form
    {
        private readonly TabControl tabs = new TabControl();
        private readonly Label connectionState = new Label();

        public FrmMain()
        {
            Text = "LAB6 | QUẢN LÝ HỒ SƠ BỆNH ÁN - .NET FRAMEWORK 4.7.2";
            Font = new Font("Segoe UI",9.5F);
            MinimumSize = new Size(1000,640);
            WindowState = FormWindowState.Maximized;
            BackColor = Color.White;
            var title=new Label { Text="HỆ THỐNG QUẢN LÝ HỒ SƠ BỆNH ÁN",
                 Font=new Font("Segoe UI",16,FontStyle.Bold), ForeColor=Color.FromArgb(20,64,117),
                 Dock=DockStyle.Top,Height=54,Padding=new Padding(14,8,0,0) };
            connectionState.Dock=DockStyle.Bottom;
            connectionState.Height=26;
            connectionState.Padding=new Padding(12,3,0,0);
            connectionState.Text="Đang kiểm tra kết nối SQL Server...";
            tabs.Dock=DockStyle.Fill;
            tabs.Font=new Font("Segoe UI",9.2F,FontStyle.Bold);
            Controls.Add(tabs);
            Controls.Add(connectionState);
            Controls.Add(title);
            try
            {
            Db.TestConnection();
            AddTab("1. Đăng ký / Lệ phí",new FrmDangKy());
            AddTab("2. Đặt lịch",new FrmLichHen());
            AddTab("3. Khám bệnh",new FrmKhamBenh());
            AddTab("4. Xét nghiệm",new FrmXetNghiem());
            AddTab("5. Toa thuốc",new FrmToaThuoc());
            AddTab("6. Viện phí",new FrmVienPhi());
            AddTab("7. Tra cứu lịch sử",new FrmLichSu());
            AddTab("8. Danh mục",new FrmDanhMuc());
            }
            catch(Exception ex)
            {
                var page=new TabPage("Cấu hình SQL Server");
                page.Controls.Add(new Label { Dock=DockStyle.Fill, Font=new Font("Segoe UI",12),
                  Text="Không nạp được dữ liệu. Hãy chạy các script SQL theo thứ tự 01 → 02 → 03, chỉnh App.config rồi khởi động lại.\n\n"+ex.Message,
                  Padding=new Padding(24), AutoSize=false });
                tabs.TabPages.Add(page);
            }
            Shown += (s,e) => CheckConnection();
        }
        private void AddTab(string title, Form form)
        {
            var page = new TabPage(title);
            form.TopLevel=false;
            form.FormBorderStyle=FormBorderStyle.None;
            form.Dock=DockStyle.Fill;
            page.Controls.Add(form);
            tabs.TabPages.Add(page);
            form.Show();
        }
        private void CheckConnection()
        {
            try { Db.TestConnection(); connectionState.ForeColor=Color.DarkGreen;
                  connectionState.Text="● Đã kết nối SQL Server / QuanLyHoSoBenhAn_LAB6"; }
            catch(Exception ex) { connectionState.ForeColor=Color.DarkRed;
                 connectionState.Text="● Lỗi kết nối - mở App.config, sửa Data Source: "+ex.Message;
                 MessageBox.Show("Không kết nối được SQL Server. Hãy chạy Database/01, 02, 03 và sửa App.config.\n\n"+ex.Message,
                       "Kiểm tra kết nối",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
        }
    }
}
