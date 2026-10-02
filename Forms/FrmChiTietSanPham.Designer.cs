namespace shopping.Forms
{
    partial class FrmChiTietSanPham
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
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblMaSP = new System.Windows.Forms.Label();
            this.lblHangSX = new System.Windows.Forms.Label();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblThongSo = new System.Windows.Forms.Label();
            this.txtThongSo = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThemGio = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTenSP.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.lblTenSP.Location = new System.Drawing.Point(30, 25);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(193, 26);
            this.lblTenSP.TabIndex = 0;
            this.lblTenSP.Text = "TÊN SẢN PHẨM";
            // 
            // lblMaSP
            // 
            this.lblMaSP.AutoSize = true;
            this.lblMaSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblMaSP.Location = new System.Drawing.Point(32, 65);
            this.lblMaSP.Name = "lblMaSP";
            this.lblMaSP.Size = new System.Drawing.Size(102, 18);
            this.lblMaSP.TabIndex = 1;
            this.lblMaSP.Text = "Mã sản phẩm: ";
            // 
            // lblHangSX
            // 
            this.lblHangSX.AutoSize = true;
            this.lblHangSX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblHangSX.Location = new System.Drawing.Point(300, 65);
            this.lblHangSX.Name = "lblHangSX";
            this.lblHangSX.Size = new System.Drawing.Size(102, 18);
            this.lblHangSX.TabIndex = 2;
            this.lblHangSX.Text = "Nhà sản xuất: ";
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.AutoSize = true;
            this.lblGiaBan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblGiaBan.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
            this.lblGiaBan.Location = new System.Drawing.Point(31, 100);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(192, 24);
            this.lblGiaBan.TabIndex = 3;
            this.lblGiaBan.Text = "Giá bán: 000.000 đ";
            // 
            // lblTinhTrang
            // 
            this.lblTinhTrang.AutoSize = true;
            this.lblTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTinhTrang.Location = new System.Drawing.Point(300, 103);
            this.lblTinhTrang.Name = "lblTinhTrang";
            this.lblTinhTrang.Size = new System.Drawing.Size(176, 20);
            this.lblTinhTrang.TabIndex = 4;
            this.lblTinhTrang.Text = "Tình trạng: Còn hàng";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblMoTa.Location = new System.Drawing.Point(32, 145);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(129, 18);
            this.lblMoTa.TabIndex = 5;
            this.lblMoTa.Text = "Mô tả sản phẩm:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMoTa.Location = new System.Drawing.Point(35, 170);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ReadOnly = true;
            this.txtMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTa.Size = new System.Drawing.Size(610, 80);
            this.txtMoTa.TabIndex = 6;
            // 
            // lblThongSo
            // 
            this.lblThongSo.AutoSize = true;
            this.lblThongSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblThongSo.Location = new System.Drawing.Point(32, 265);
            this.lblThongSo.Name = "lblThongSo";
            this.lblThongSo.Size = new System.Drawing.Size(150, 18);
            this.lblThongSo.TabIndex = 7;
            this.lblThongSo.Text = "Thông số kỹ thuật:";
            // 
            // txtThongSo
            // 
            this.txtThongSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtThongSo.Location = new System.Drawing.Point(35, 290);
            this.txtThongSo.Multiline = true;
            this.txtThongSo.Name = "txtThongSo";
            this.txtThongSo.ReadOnly = true;
            this.txtThongSo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtThongSo.Size = new System.Drawing.Size(610, 90);
            this.txtThongSo.TabIndex = 8;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoLuong.Location = new System.Drawing.Point(32, 405);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(120, 18);
            this.lblSoLuong.TabIndex = 9;
            this.lblSoLuong.Text = "Số lượng mua: ";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numSoLuong.Location = new System.Drawing.Point(160, 402);
            this.numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(80, 26);
            this.numSoLuong.TabIndex = 10;
            this.numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThemGio
            // 
            this.btnThemGio.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.btnThemGio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemGio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemGio.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnThemGio.Location = new System.Drawing.Point(320, 396);
            this.btnThemGio.Name = "btnThemGio";
            this.btnThemGio.Size = new System.Drawing.Size(180, 38);
            this.btnThemGio.TabIndex = 11;
            this.btnThemGio.Text = "+ Thêm vào giỏ hàng";
            this.btnThemGio.UseVisualStyleBackColor = false;
            this.btnThemGio.Click += new System.EventHandler(this.btnThemGio_Click);
            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDong.Location = new System.Drawing.Point(525, 396);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 38);
            this.btnDong.TabIndex = 12;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmChiTietSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 465);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnThemGio);
            this.Controls.Add(this.numSoLuong);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.txtThongSo);
            this.Controls.Add(this.lblThongSo);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.lblTinhTrang);
            this.Controls.Add(this.lblGiaBan);
            this.Controls.Add(this.lblHangSX);
            this.Controls.Add(this.lblMaSP);
            this.Controls.Add(this.lblTenSP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmChiTietSanPham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết sản phẩm & Thông số kỹ thuật";
            this.Load += new System.EventHandler(this.FrmChiTietSanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.Label lblHangSX;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblThongSo;
        private System.Windows.Forms.TextBox txtThongSo;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Button btnThemGio;
        private System.Windows.Forms.Button btnDong;
    }
}