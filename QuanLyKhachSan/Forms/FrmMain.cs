using System;
using System.Windows.Forms;
namespace QuanLyKhachSan.Forms
{
    public
partial class FrmMain : Form
    {
        private Button btnDanhMuc;
        private Button btnPhong;
        private Button btnDatPhong;
        private Button btnDichVu;
        private Button btnTraPhong;
        private Button btnThongKe;
        private Label label1;
        private Button btnThoat;

        public FrmMain() { InitializeComponent(); }
        private void
btnDanhMuc_Click(object s, EventArgs e)
        {
            using (var f = new
FrmDanhMuc()) f.ShowDialog(this);
        }
        private void btnPhong_Click(object s, EventArgs
e)
        { using (var f = new FrmPhongTienNghi()) f.ShowDialog(this); }
        private void
btnDatPhong_Click(object s, EventArgs e)
        {
            using (var f = new
FrmDatPhong()) f.ShowDialog(this);
        }
        private void btnDichVu_Click(object s, EventArgs
e)
        { using (var f = new FrmDichVu()) f.ShowDialog(this); }
        private void
btnTraPhong_Click(object s, EventArgs e)
        {
            using (var f = new
FrmTraPhong()) f.ShowDialog(this);
        }
        private void btnThongKe_Click(object s, EventArgs
e)
        { using (var f = new FrmThongKe()) f.ShowDialog(this); }
        private void btnThoat_Click(object
s, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát?", "Xác
nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)Close();}
}

        private void InitializeComponent()
        {
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnPhong = new System.Windows.Forms.Button();
            this.btnDatPhong = new System.Windows.Forms.Button();
            this.btnDichVu = new System.Windows.Forms.Button();
            this.btnTraPhong = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDanhMuc
            // 
            this.btnDanhMuc.Location = new System.Drawing.Point(91, 113);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(162, 56);
            this.btnDanhMuc.TabIndex = 0;
            this.btnDanhMuc.Text = " Danh mục";
            this.btnDanhMuc.UseVisualStyleBackColor = true;
            this.btnDanhMuc.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPhong
            // 
            this.btnPhong.Location = new System.Drawing.Point(324, 108);
            this.btnPhong.Name = "btnPhong";
            this.btnPhong.Size = new System.Drawing.Size(172, 61);
            this.btnPhong.TabIndex = 1;
            this.btnPhong.Text = "Phòng - Tiện nghi";
            this.btnPhong.UseVisualStyleBackColor = true;
            this.btnPhong.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnDatPhong
            // 
            this.btnDatPhong.Location = new System.Drawing.Point(584, 108);
            this.btnDatPhong.Name = "btnDatPhong";
            this.btnDatPhong.Size = new System.Drawing.Size(166, 61);
            this.btnDatPhong.TabIndex = 2;
            this.btnDatPhong.Text = "Đặt / Nhận phòng";
            this.btnDatPhong.UseVisualStyleBackColor = true;
            this.btnDatPhong.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDichVu
            // 
            this.btnDichVu.Location = new System.Drawing.Point(91, 192);
            this.btnDichVu.Name = "btnDichVu";
            this.btnDichVu.Size = new System.Drawing.Size(162, 59);
            this.btnDichVu.TabIndex = 3;
            this.btnDichVu.Text = "Sử dụng dịch vụ";
            this.btnDichVu.UseVisualStyleBackColor = true;
            this.btnDichVu.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnTraPhong
            // 
            this.btnTraPhong.Location = new System.Drawing.Point(324, 192);
            this.btnTraPhong.Name = "btnTraPhong";
            this.btnTraPhong.Size = new System.Drawing.Size(172, 60);
            this.btnTraPhong.TabIndex = 4;
            this.btnTraPhong.Text = " Trả phòng - Thanh toán";
            this.btnTraPhong.UseVisualStyleBackColor = true;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(584, 192);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(166, 59);
            this.btnThongKe.TabIndex = 5;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(324, 298);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(159, 67);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.button7_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(135, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(599, 42);
            this.label1.TabIndex = 7;
            this.label1.Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(863, 408);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnTraPhong);
            this.Controls.Add(this.btnDichVu);
            this.Controls.Add(this.btnDatPhong);
            this.Controls.Add(this.btnPhong);
            this.Controls.Add(this.btnDanhMuc);
            this.Name = "FrmMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }