namespace shopping.Forms
{
    partial class FrmDangNhap_DangKy
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDN = new System.Windows.Forms.TabPage();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.txtLoginPass = new System.Windows.Forms.TextBox();
            this.txtLoginUser = new System.Windows.Forms.TextBox();
            this.lblLoginPass = new System.Windows.Forms.Label();
            this.lblLoginUser = new System.Windows.Forms.Label();
            this.tabDK = new System.Windows.Forms.TabPage();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.txtDK_Pass = new System.Windows.Forms.TextBox();
            this.lblDK_Pass = new System.Windows.Forms.Label();
            this.txtDK_User = new System.Windows.Forms.TextBox();
            this.lblDK_User = new System.Windows.Forms.Label();
            this.txtDK_Email = new System.Windows.Forms.TextBox();
            this.lblDK_Email = new System.Windows.Forms.Label();
            this.txtDK_SDT = new System.Windows.Forms.TextBox();
            this.lblDK_SDT = new System.Windows.Forms.Label();
            this.txtDK_DiaChi = new System.Windows.Forms.TextBox();
            this.lblDK_DiaChi = new System.Windows.Forms.Label();
            this.txtDK_CMND = new System.Windows.Forms.TextBox();
            this.lblDK_CMND = new System.Windows.Forms.Label();
            this.dtDK_NgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblDK_NgaySinh = new System.Windows.Forms.Label();
            this.txtDK_HoTen = new System.Windows.Forms.TextBox();
            this.lblDK_HoTen = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabDN.SuspendLayout();
            this.tabDK.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDN);
            this.tabControl1.Controls.Add(this.tabDK);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(534, 461);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDN
            // 
            this.tabDN.Controls.Add(this.btnDangNhap);
            this.tabDN.Controls.Add(this.txtLoginPass);
            this.tabDN.Controls.Add(this.txtLoginUser);
            this.tabDN.Controls.Add(this.lblLoginPass);
            this.tabDN.Controls.Add(this.lblLoginUser);
            this.tabDN.Location = new System.Drawing.Point(4, 25);
            this.tabDN.Name = "tabDN";
            this.tabDN.Padding = new System.Windows.Forms.Padding(3);
            this.tabDN.Size = new System.Drawing.Size(526, 432);
            this.tabDN.TabIndex = 0;
            this.tabDN.Text = "Đăng nhập";
            this.tabDN.UseVisualStyleBackColor = true;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnDangNhap.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnDangNhap.Location = new System.Drawing.Point(160, 200);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(220, 42);
            this.btnDangNhap.TabIndex = 4;
            this.btnDangNhap.Text = "ĐĂNG NHẬP";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // txtLoginPass
            // 
            this.txtLoginPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLoginPass.Location = new System.Drawing.Point(160, 130);
            this.txtLoginPass.Name = "txtLoginPass";
            this.txtLoginPass.PasswordChar = '*';
            this.txtLoginPass.Size = new System.Drawing.Size(260, 26);
            this.txtLoginPass.TabIndex = 3;
            // 
            // txtLoginUser
            // 
            this.txtLoginUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLoginUser.Location = new System.Drawing.Point(160, 75);
            this.txtLoginUser.Name = "txtLoginUser";
            this.txtLoginUser.Size = new System.Drawing.Size(260, 26);
            this.txtLoginUser.TabIndex = 1;
            // 
            // lblLoginPass
            // 
            this.lblLoginPass.AutoSize = true;
            this.lblLoginPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLoginPass.Location = new System.Drawing.Point(40, 133);
            this.lblLoginPass.Name = "lblLoginPass";
            this.lblLoginPass.Size = new System.Drawing.Size(73, 18);
            this.lblLoginPass.TabIndex = 2;
            this.lblLoginPass.Text = "Mật khẩu:";
            // 
            // lblLoginUser
            // 
            this.lblLoginUser.AutoSize = true;
            this.lblLoginUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLoginUser.Location = new System.Drawing.Point(40, 78);
            this.lblLoginUser.Name = "lblLoginUser";
            this.lblLoginUser.Size = new System.Drawing.Size(109, 18);
            this.lblLoginUser.TabIndex = 0;
            this.lblLoginUser.Text = "Tên đăng nhập:";
            // 
            // tabDK
            // 
            this.tabDK.Controls.Add(this.btnDangKy);
            this.tabDK.Controls.Add(this.txtDK_Pass);
            this.tabDK.Controls.Add(this.lblDK_Pass);
            this.tabDK.Controls.Add(this.txtDK_User);
            this.tabDK.Controls.Add(this.lblDK_User);
            this.tabDK.Controls.Add(this.txtDK_Email);
            this.tabDK.Controls.Add(this.lblDK_Email);
            this.tabDK.Controls.Add(this.txtDK_SDT);
            this.tabDK.Controls.Add(this.lblDK_SDT);
            this.tabDK.Controls.Add(this.txtDK_DiaChi);
            this.tabDK.Controls.Add(this.lblDK_DiaChi);
            this.tabDK.Controls.Add(this.txtDK_CMND);
            this.tabDK.Controls.Add(this.lblDK_CMND);
            this.tabDK.Controls.Add(this.dtDK_NgaySinh);
            this.tabDK.Controls.Add(this.lblDK_NgaySinh);
            this.tabDK.Controls.Add(this.txtDK_HoTen);
            this.tabDK.Controls.Add(this.lblDK_HoTen);
            this.tabDK.Location = new System.Drawing.Point(4, 25);
            this.tabDK.Name = "tabDK";
            this.tabDK.Padding = new System.Windows.Forms.Padding(3);
            this.tabDK.Size = new System.Drawing.Size(526, 432);
            this.tabDK.TabIndex = 1;
            this.tabDK.Text = "Đăng ký mới";
            this.tabDK.UseVisualStyleBackColor = true;
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnDangKy.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnDangKy.Location = new System.Drawing.Point(150, 365);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(240, 42);
            this.btnDangKy.TabIndex = 16;
            this.btnDangKy.Text = "TẠO TÀI KHOẢN MỚI";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // txtDK_Pass
            // 
            this.txtDK_Pass.Location = new System.Drawing.Point(150, 320);
            this.txtDK_Pass.Name = "txtDK_Pass";
            this.txtDK_Pass.PasswordChar = '*';
            this.txtDK_Pass.Size = new System.Drawing.Size(320, 22);
            this.txtDK_Pass.TabIndex = 15;
            // 
            // lblDK_Pass
            // 
            this.lblDK_Pass.AutoSize = true;
            this.lblDK_Pass.Location = new System.Drawing.Point(25, 323);
            this.lblDK_Pass.Name = "lblDK_Pass";
            this.lblDK_Pass.Size = new System.Drawing.Size(64, 16);
            this.lblDK_Pass.TabIndex = 14;
            this.lblDK_Pass.Text = "Mật khẩu:";
            // 
            // txtDK_User
            // 
            this.txtDK_User.Location = new System.Drawing.Point(150, 280);
            this.txtDK_User.Name = "txtDK_User";
            this.txtDK_User.Size = new System.Drawing.Size(320, 22);
            this.txtDK_User.TabIndex = 13;
            // 
            // lblDK_User
            // 
            this.lblDK_User.AutoSize = true;
            this.lblDK_User.Location = new System.Drawing.Point(25, 283);
            this.lblDK_User.Name = "lblDK_User";
            this.lblDK_User.Size = new System.Drawing.Size(101, 16);
            this.lblDK_User.TabIndex = 12;
            this.lblDK_User.Text = "Tên đăng nhập:";
            // 
            // txtDK_Email
            // 
            this.txtDK_Email.Location = new System.Drawing.Point(150, 240);
            this.txtDK_Email.Name = "txtDK_Email";
            this.txtDK_Email.Size = new System.Drawing.Size(320, 22);
            this.txtDK_Email.TabIndex = 11;
            // 
            // lblDK_Email
            // 
            this.lblDK_Email.AutoSize = true;
            this.lblDK_Email.Location = new System.Drawing.Point(25, 243);
            this.lblDK_Email.Name = "lblDK_Email";
            this.lblDK_Email.Size = new System.Drawing.Size(44, 16);
            this.lblDK_Email.TabIndex = 10;
            this.lblDK_Email.Text = "Email:";
            // 
            // txtDK_SDT
            // 
            this.txtDK_SDT.Location = new System.Drawing.Point(150, 200);
            this.txtDK_SDT.Name = "txtDK_SDT";
            this.txtDK_SDT.Size = new System.Drawing.Size(320, 22);
            this.txtDK_SDT.TabIndex = 9;
            // 
            // lblDK_SDT
            // 
            this.lblDK_SDT.AutoSize = true;
            this.lblDK_SDT.Location = new System.Drawing.Point(25, 203);
            this.lblDK_SDT.Name = "lblDK_SDT";
            this.lblDK_SDT.Size = new System.Drawing.Size(88, 16);
            this.lblDK_SDT.TabIndex = 8;
            this.lblDK_SDT.Text = "Số điện thoại:";
            // 
            // txtDK_DiaChi
            // 
            this.txtDK_DiaChi.Location = new System.Drawing.Point(150, 155);
            this.txtDK_DiaChi.Name = "txtDK_DiaChi";
            this.txtDK_DiaChi.Size = new System.Drawing.Size(320, 22);
            this.txtDK_DiaChi.TabIndex = 7;
            // 
            // lblDK_DiaChi
            // 
            this.lblDK_DiaChi.AutoSize = true;
            this.lblDK_DiaChi.Location = new System.Drawing.Point(25, 158);
            this.lblDK_DiaChi.Name = "lblDK_DiaChi";
            this.lblDK_DiaChi.Size = new System.Drawing.Size(50, 16);
            this.lblDK_DiaChi.TabIndex = 6;
            this.lblDK_DiaChi.Text = "Địa chỉ:";
            // 
            // txtDK_CMND
            // 
            this.txtDK_CMND.Location = new System.Drawing.Point(150, 110);
            this.txtDK_CMND.Name = "txtDK_CMND";
            this.txtDK_CMND.Size = new System.Drawing.Size(320, 22);
            this.txtDK_CMND.TabIndex = 5;
            // 
            // lblDK_CMND
            // 
            this.lblDK_CMND.AutoSize = true;
            this.lblDK_CMND.Location = new System.Drawing.Point(25, 113);
            this.lblDK_CMND.Name = "lblDK_CMND";
            this.lblDK_CMND.Size = new System.Drawing.Size(117, 16);
            this.lblDK_CMND.TabIndex = 4;
            this.lblDK_CMND.Text = "CMND / Căn cước:";
            // 
            // dtDK_NgaySinh
            // 
            this.dtDK_NgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDK_NgaySinh.Location = new System.Drawing.Point(150, 65);
            this.dtDK_NgaySinh.Name = "dtDK_NgaySinh";
            this.dtDK_NgaySinh.Size = new System.Drawing.Size(150, 22);
            this.dtDK_NgaySinh.TabIndex = 3;
            // 
            // lblDK_NgaySinh
            // 
            this.lblDK_NgaySinh.AutoSize = true;
            this.lblDK_NgaySinh.Location = new System.Drawing.Point(25, 70);
            this.lblDK_NgaySinh.Name = "lblDK_NgaySinh";
            this.lblDK_NgaySinh.Size = new System.Drawing.Size(70, 16);
            this.lblDK_NgaySinh.TabIndex = 2;
            this.lblDK_NgaySinh.Text = "Ngày sinh:";
            // 
            // txtDK_HoTen
            // 
            this.txtDK_HoTen.Location = new System.Drawing.Point(150, 25);
            this.txtDK_HoTen.Name = "txtDK_HoTen";
            this.txtDK_HoTen.Size = new System.Drawing.Size(320, 22);
            this.txtDK_HoTen.TabIndex = 1;
            // 
            // lblDK_HoTen
            // 
            this.lblDK_HoTen.AutoSize = true;
            this.lblDK_HoTen.Location = new System.Drawing.Point(25, 28);
            this.lblDK_HoTen.Name = "lblDK_HoTen";
            this.lblDK_HoTen.Size = new System.Drawing.Size(67, 16);
            this.lblDK_HoTen.TabIndex = 0;
            this.lblDK_HoTen.Text = "Họ và tên:";
            // 
            // FrmDangNhap_DangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmDangNhap_DangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác thực tài khoản khách hàng";
            this.tabControl1.ResumeLayout(false);
            this.tabDN.ResumeLayout(false);
            this.tabDN.PerformLayout();
            this.tabDK.ResumeLayout(false);
            this.tabDK.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDN;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.TextBox txtLoginPass;
        private System.Windows.Forms.TextBox txtLoginUser;
        private System.Windows.Forms.Label lblLoginPass;
        private System.Windows.Forms.Label lblLoginUser;
        private System.Windows.Forms.TabPage tabDK;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.TextBox txtDK_Pass;
        private System.Windows.Forms.Label lblDK_Pass;
        private System.Windows.Forms.TextBox txtDK_User;
        private System.Windows.Forms.Label lblDK_User;
        private System.Windows.Forms.TextBox txtDK_Email;
        private System.Windows.Forms.Label lblDK_Email;
        private System.Windows.Forms.TextBox txtDK_SDT;
        private System.Windows.Forms.Label lblDK_SDT;
        private System.Windows.Forms.TextBox txtDK_DiaChi;
        private System.Windows.Forms.Label lblDK_DiaChi;
        private System.Windows.Forms.TextBox txtDK_CMND;
        private System.Windows.Forms.Label lblDK_CMND;
        private System.Windows.Forms.DateTimePicker dtDK_NgaySinh;
        private System.Windows.Forms.Label lblDK_NgaySinh;
        private System.Windows.Forms.TextBox txtDK_HoTen;
        private System.Windows.Forms.Label lblDK_HoTen;
    }
}