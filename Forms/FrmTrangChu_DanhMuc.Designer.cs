namespace shopping.Forms
{
    partial class FrmTrangChu_DanhMuc
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnGioHang = new System.Windows.Forms.Button();
            this.btnDangNhapNav = new System.Windows.Forms.Button();
            this.pnlCategory = new System.Windows.Forms.Panel();
            this.lblChonNhom = new System.Windows.Forms.Label();
            this.cboNhomSP = new System.Windows.Forms.ComboBox();
            this.lblBanner = new System.Windows.Forms.Label();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.btnThemVaoGio = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.pnlTop.Controls.Add(this.btnDangNhapNav);
            this.pnlTop.Controls.Add(this.btnGioHang);
            this.pnlTop.Controls.Add(this.btnTim);
            this.pnlTop.Controls.Add(this.txtTimKiem);
            this.pnlTop.Controls.Add(this.lblLogo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(984, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.lblLogo.Location = new System.Drawing.Point(20, 15);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(225, 29);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "e-SHOPPING ABC";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTimKiem.Location = new System.Drawing.Point(280, 16);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(320, 26);
            this.txtTimKiem.TabIndex = 1;
            // 
            // btnTim
            // 
            this.btnTim.BackColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.btnTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTim.Location = new System.Drawing.Point(610, 14);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 30);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = false;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnGioHang
            // 
            this.btnGioHang.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGioHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGioHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnGioHang.Location = new System.Drawing.Point(710, 14);
            this.btnGioHang.Name = "btnGioHang";
            this.btnGioHang.Size = new System.Drawing.Size(130, 30);
            this.btnGioHang.TabIndex = 3;
            this.btnGioHang.Text = "🛒 Giỏ hàng (0)";
            this.btnGioHang.UseVisualStyleBackColor = false;
            this.btnGioHang.Click += new System.EventHandler(this.btnGioHang_Click);
            // 
            // btnDangNhapNav
            // 
            this.btnDangNhapNav.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnDangNhapNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangNhapNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDangNhapNav.Location = new System.Drawing.Point(850, 14);
            this.btnDangNhapNav.Name = "btnDangNhapNav";
            this.btnDangNhapNav.Size = new System.Drawing.Size(110, 30);
            this.btnDangNhapNav.TabIndex = 4;
            this.btnDangNhapNav.Text = "Đăng nhập";
            this.btnDangNhapNav.UseVisualStyleBackColor = false;
            this.btnDangNhapNav.Click += new System.EventHandler(this.btnDangNhapNav_Click);
            // 
            // pnlCategory
            // 
            this.pnlCategory.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlCategory.Controls.Add(this.lblBanner);
            this.pnlCategory.Controls.Add(this.cboNhomSP);
            this.pnlCategory.Controls.Add(this.lblChonNhom);
            this.pnlCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCategory.Location = new System.Drawing.Point(0, 60);
            this.pnlCategory.Name = "pnlCategory";
            this.pnlCategory.Size = new System.Drawing.Size(984, 70);
            this.pnlCategory.TabIndex = 1;
            // 
            // lblChonNhom
            // 
            this.lblChonNhom.AutoSize = true;
            this.lblChonNhom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblChonNhom.Location = new System.Drawing.Point(20, 15);
            this.lblChonNhom.Name = "lblChonNhom";
            this.lblChonNhom.Size = new System.Drawing.Size(133, 18);
            this.lblChonNhom.TabIndex = 0;
            this.lblChonNhom.Text = "Nhóm sản phẩm:";
            // 
            // cboNhomSP
            // 
            this.cboNhomSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboNhomSP.FormattingEnabled = true;
            this.cboNhomSP.Location = new System.Drawing.Point(160, 12);
            this.cboNhomSP.Name = "cboNhomSP";
            this.cboNhomSP.Size = new System.Drawing.Size(250, 26);
            this.cboNhomSP.TabIndex = 1;
            this.cboNhomSP.SelectedIndexChanged += new System.EventHandler(this.cboNhomSP_SelectedIndexChanged);
            // 
            // lblBanner
            // 
            this.lblBanner.AutoSize = true;
            this.lblBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblBanner.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
            this.lblBanner.Location = new System.Drawing.Point(20, 45);
            this.lblBanner.Name = "lblBanner";
            this.lblBanner.Size = new System.Drawing.Size(635, 18);
            this.lblBanner.TabIndex = 2;
            this.lblBanner.Text = "🎄 KHUYẾN MÃI GIÁNG SINH: FREESHIP CPN ĐƠN TỪ 1.000.000đ - FREESHIP 24H ĐƠN TỪ 5.000.000đ";
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.AllowUserToDeleteRows = false;
            this.dgvSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSanPham.Location = new System.Drawing.Point(0, 130);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.RowHeadersWidth = 51;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(984, 430);
            this.dgvSanPham.TabIndex = 2;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlBottom.Controls.Add(this.btnThemVaoGio);
            this.pnlBottom.Controls.Add(this.btnXemChiTiet);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 500);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(984, 60);
            this.pnlBottom.TabIndex = 3;
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnXemChiTiet.Location = new System.Drawing.Point(620, 12);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(140, 36);
            this.btnXemChiTiet.TabIndex = 0;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.UseVisualStyleBackColor = true;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnThemVaoGio
            // 
            this.btnThemVaoGio.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.btnThemVaoGio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoGio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemVaoGio.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnThemVaoGio.Location = new System.Drawing.Point(780, 12);
            this.btnThemVaoGio.Name = "btnThemVaoGio";
            this.btnThemVaoGio.Size = new System.Drawing.Size(180, 36);
            this.btnThemVaoGio.TabIndex = 1;
            this.btnThemVaoGio.Text = "+ Thêm vào giỏ hàng";
            this.btnThemVaoGio.UseVisualStyleBackColor = false;
            this.btnThemVaoGio.Click += new System.EventHandler(this.btnThemVaoGio_Click);
            // 
            // FrmTrangChu_DanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 560);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.dgvSanPham);
            this.Controls.Add(this.pnlCategory);
            this.Controls.Add(this.pnlTop);
            this.Name = "FrmTrangChu_DanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "e-Shopping ABC - Mua sắm mùa Giáng Sinh & Năm Mới";
            this.Load += new System.EventHandler(this.FrmTrangChu_DanhMuc_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlCategory.ResumeLayout(false);
            this.pnlCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnGioHang;
        private System.Windows.Forms.Button btnDangNhapNav;
        private System.Windows.Forms.Panel pnlCategory;
        private System.Windows.Forms.Label lblChonNhom;
        private System.Windows.Forms.ComboBox cboNhomSP;
        private System.Windows.Forms.Label lblBanner;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnThemVaoGio;
    }
}