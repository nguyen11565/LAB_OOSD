namespace shopping.Forms
{
    partial class FrmThongTinGiaoHang
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
            this.gbNguoiNhan = new System.Windows.Forms.GroupBox();
            this.radTangQua = new System.Windows.Forms.RadioButton();
            this.radNguoiMua = new System.Windows.Forms.RadioButton();
            this.cboKhuVuc = new System.Windows.Forms.ComboBox();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtNguoiNhan = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.gbLoaiPhieu = new System.Windows.Forms.GroupBox();
            this.radTrongNgay = new System.Windows.Forms.RadioButton();
            this.radCPN = new System.Windows.Forms.RadioButton();
            this.radThuong = new System.Windows.Forms.RadioButton();
            this.pnlTongKet = new System.Windows.Forms.Panel();
            this.btnSangThanhToan = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.lblPhiShip = new System.Windows.Forms.Label();
            this.lblTienHang = new System.Windows.Forms.Label();
            this.gbNguoiNhan.SuspendLayout();
            this.gbLoaiPhieu.SuspendLayout();
            this.pnlTongKet.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbNguoiNhan
            // 
            this.gbNguoiNhan.Controls.Add(this.radTangQua);
            this.gbNguoiNhan.Controls.Add(this.radNguoiMua);
            this.gbNguoiNhan.Controls.Add(this.cboKhuVuc);
            this.gbNguoiNhan.Controls.Add(this.lblKhuVuc);
            this.gbNguoiNhan.Controls.Add(this.txtDiaChi);
            this.gbNguoiNhan.Controls.Add(this.lblDiaChi);
            this.gbNguoiNhan.Controls.Add(this.txtSDT);
            this.gbNguoiNhan.Controls.Add(this.lblSDT);
            this.gbNguoiNhan.Controls.Add(this.txtNguoiNhan);
            this.gbNguoiNhan.Controls.Add(this.lblHoTen);
            this.gbNguoiNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbNguoiNhan.Location = new System.Drawing.Point(20, 20);
            this.gbNguoiNhan.Name = "gbNguoiNhan";
            this.gbNguoiNhan.Size = new System.Drawing.Size(680, 230);
            this.gbNguoiNhan.TabIndex = 0;
            this.gbNguoiNhan.TabStop = false;
            this.gbNguoiNhan.Text = "1. THÔNG TIN NGƯỜI NHẬN HÀNG (HỖ TRỢ TẶNG QUÀ - BR06)";
            // 
            // radTangQua
            // 
            this.radTangQua.AutoSize = true;
            this.radTangQua.Checked = true;
            this.radTangQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.radTangQua.Location = new System.Drawing.Point(340, 32);
            this.radTangQua.Name = "radTangQua";
            this.radTangQua.Size = new System.Drawing.Size(306, 22);
            this.radTangQua.TabIndex = 1;
            this.radTangQua.TabStop = true;
            this.radTangQua.Text = "Gửi quà tặng cho người thân/bạn bè (BR06)";
            this.radTangQua.UseVisualStyleBackColor = true;
            this.radTangQua.CheckedChanged += new System.EventHandler(this.radCheDoNhan_CheckedChanged);
            // 
            // radNguoiMua
            // 
            this.radNguoiMua.AutoSize = true;
            this.radNguoiMua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.radNguoiMua.Location = new System.Drawing.Point(30, 32);
            this.radNguoiMua.Name = "radNguoiMua";
            this.radNguoiMua.Size = new System.Drawing.Size(262, 22);
            this.radNguoiMua.TabIndex = 0;
            this.radNguoiMua.Text = "Người nhận là tôi (Lấy từ tài khoản)";
            this.radNguoiMua.UseVisualStyleBackColor = true;
            this.radNguoiMua.CheckedChanged += new System.EventHandler(this.radCheDoNhan_CheckedChanged);
            // 
            // cboKhuVuc
            // 
            this.cboKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhuVuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboKhuVuc.FormattingEnabled = true;
            this.cboKhuVuc.Items.AddRange(new object[] {
            "Nội thành TP.HCM / Hà Nội",
            "Ngoại thành TP.HCM / Hà Nội",
            "Các tỉnh thành khác"});
            this.cboKhuVuc.Location = new System.Drawing.Point(200, 185);
            this.cboKhuVuc.Name = "cboKhuVuc";
            this.cboKhuVuc.Size = new System.Drawing.Size(450, 26);
            this.cboKhuVuc.TabIndex = 9;
            this.cboKhuVuc.SelectedIndexChanged += new System.EventHandler(this.cboKhuVuc_SelectedIndexChanged);
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblKhuVuc.Location = new System.Drawing.Point(30, 188);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(155, 18);
            this.lblKhuVuc.TabIndex = 8;
            this.lblKhuVuc.Text = "Khu vực giao (BR15): *";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtDiaChi.Location = new System.Drawing.Point(200, 145);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(450, 24);
            this.txtDiaChi.TabIndex = 7;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblDiaChi.Location = new System.Drawing.Point(30, 148);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(133, 18);
            this.lblDiaChi.TabIndex = 6;
            this.lblDiaChi.Text = "Địa chỉ nhận hàng: *";
            // 
            // txtSDT
            // 
            this.txtSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtSDT.Location = new System.Drawing.Point(200, 108);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(450, 24);
            this.txtSDT.TabIndex = 5;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblSDT.Location = new System.Drawing.Point(30, 111);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(107, 18);
            this.lblSDT.TabIndex = 4;
            this.lblSDT.Text = "Số điện thoại: *";
            // 
            // txtNguoiNhan
            // 
            this.txtNguoiNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtNguoiNhan.Location = new System.Drawing.Point(200, 70);
            this.txtNguoiNhan.Name = "txtNguoiNhan";
            this.txtNguoiNhan.Size = new System.Drawing.Size(450, 24);
            this.txtNguoiNhan.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblHoTen.Location = new System.Drawing.Point(30, 73);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(149, 18);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên người nhận: *";
            // 
            // gbLoaiPhieu
            // 
            this.gbLoaiPhieu.Controls.Add(this.radTrongNgay);
            this.gbLoaiPhieu.Controls.Add(this.radCPN);
            this.gbLoaiPhieu.Controls.Add(this.radThuong);
            this.gbLoaiPhieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbLoaiPhieu.Location = new System.Drawing.Point(20, 260);
            this.gbLoaiPhieu.Name = "gbLoaiPhieu";
            this.gbLoaiPhieu.Size = new System.Drawing.Size(680, 140);
            this.gbLoaiPhieu.TabIndex = 1;
            this.gbLoaiPhieu.TabStop = false;
            this.gbLoaiPhieu.Text = "2. CHỌN 1 TRONG 3 LOẠI PHIẾU ĐẶT HÀNG (BR03)";
            // 
            // radTrongNgay
            // 
            this.radTrongNgay.AutoSize = true;
            this.radTrongNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.radTrongNgay.Location = new System.Drawing.Point(30, 100);
            this.radTrongNgay.Name = "radTrongNgay";
            this.radTrongNgay.Size = new System.Drawing.Size(612, 22);
            this.radTrongNgay.TabIndex = 2;
            this.radTrongNgay.Text = "3. Phiếu chuyển phát nhanh trong ngày (trong 24h) -- MIỄN PHÍ nếu đơn hàng >= 5.0" +
    "00.000đ";
            this.radTrongNgay.UseVisualStyleBackColor = true;
            this.radTrongNgay.CheckedChanged += new System.EventHandler(this.radLoaiPhieu_CheckedChanged);
            // 
            // radCPN
            // 
            this.radCPN.AutoSize = true;
            this.radCPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.radCPN.Location = new System.Drawing.Point(30, 65);
            this.radCPN.Name = "radCPN";
            this.radCPN.Size = new System.Drawing.Size(564, 22);
            this.radCPN.TabIndex = 1;
            this.radCPN.Text = "2. Phiếu chuyển phát nhanh (1-2 ngày) -- MIỄN PHÍ nếu đơn hàng >= 1.000.000đ (BR0" +
    "4)";
            this.radCPN.UseVisualStyleBackColor = true;
            this.radCPN.CheckedChanged += new System.EventHandler(this.radLoaiPhieu_CheckedChanged);
            // 
            // radThuong
            // 
            this.radThuong.AutoSize = true;
            this.radThuong.Checked = true;
            this.radThuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.radThuong.Location = new System.Drawing.Point(30, 30);
            this.radThuong.Name = "radThuong";
            this.radThuong.Size = new System.Drawing.Size(325, 22);
            this.radThuong.TabIndex = 0;
            this.radThuong.TabStop = true;
            this.radThuong.Text = "1. Phiếu đặt hàng thường (3 - 5 ngày làm việc)";
            this.radThuong.UseVisualStyleBackColor = true;
            this.radThuong.CheckedChanged += new System.EventHandler(this.radLoaiPhieu_CheckedChanged);
            // 
            // pnlTongKet
            // 
            this.pnlTongKet.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlTongKet.Controls.Add(this.btnSangThanhToan);
            this.pnlTongKet.Controls.Add(this.btnQuayLai);
            this.pnlTongKet.Controls.Add(this.lblTongCong);
            this.pnlTongKet.Controls.Add(this.lblPhiShip);
            this.pnlTongKet.Controls.Add(this.lblTienHang);
            this.pnlTongKet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongKet.Location = new System.Drawing.Point(0, 415);
            this.pnlTongKet.Name = "pnlTongKet";
            this.pnlTongKet.Size = new System.Drawing.Size(724, 135);
            this.pnlTongKet.TabIndex = 2;
            // 
            // btnSangThanhToan
            // 
            this.btnSangThanhToan.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.btnSangThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSangThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSangThanhToan.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnSangThanhToan.Location = new System.Drawing.Point(440, 75);
            this.btnSangThanhToan.Name = "btnSangThanhToan";
            this.btnSangThanhToan.Size = new System.Drawing.Size(260, 45);
            this.btnSangThanhToan.TabIndex = 4;
            this.btnSangThanhToan.Text = "TIẾP TỤC THANH TOÁN >>";
            this.btnSangThanhToan.UseVisualStyleBackColor = false;
            this.btnSangThanhToan.Click += new System.EventHandler(this.btnSangThanhToan_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnQuayLai.Location = new System.Drawing.Point(24, 75);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(160, 45);
            this.btnQuayLai.TabIndex = 3;
            this.btnQuayLai.Text = "<< Sửa giỏ hàng";
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTongCong.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.lblTongCong.Location = new System.Drawing.Point(440, 20);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(236, 24);
            this.lblTongCong.TabIndex = 2;
            this.lblTongCong.Text = "TỔNG CỘNG: 000.000 đ";
            // 
            // lblPhiShip
            // 
            this.lblPhiShip.AutoSize = true;
            this.lblPhiShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPhiShip.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
            this.lblPhiShip.Location = new System.Drawing.Point(220, 22);
            this.lblPhiShip.Name = "lblPhiShip";
            this.lblPhiShip.Size = new System.Drawing.Size(143, 20);
            this.lblPhiShip.TabIndex = 1;
            this.lblPhiShip.Text = "Phí ship: 30.000đ";
            // 
            // lblTienHang
            // 
            this.lblTienHang.AutoSize = true;
            this.lblTienHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblTienHang.Location = new System.Drawing.Point(20, 22);
            this.lblTienHang.Name = "lblTienHang";
            this.lblTienHang.Size = new System.Drawing.Size(164, 20);
            this.lblTienHang.TabIndex = 0;
            this.lblTienHang.Text = "Tiền hàng: 000.000 đ";
            // 
            // FrmThongTinGiaoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 550);
            this.Controls.Add(this.pnlTongKet);
            this.Controls.Add(this.gbLoaiPhieu);
            this.Controls.Add(this.gbNguoiNhan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmThongTinGiaoHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bước 1/2: Thông tin giao nhận quà tặng & Loại phiếu";
            this.Load += new System.EventHandler(this.FrmThongTinGiaoHang_Load);
            this.gbNguoiNhan.ResumeLayout(false);
            this.gbNguoiNhan.PerformLayout();
            this.gbLoaiPhieu.ResumeLayout(false);
            this.gbLoaiPhieu.PerformLayout();
            this.pnlTongKet.ResumeLayout(false);
            this.pnlTongKet.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbNguoiNhan;
        private System.Windows.Forms.RadioButton radTangQua;
        private System.Windows.Forms.RadioButton radNguoiMua;
        private System.Windows.Forms.ComboBox cboKhuVuc;
        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtNguoiNhan;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.GroupBox gbLoaiPhieu;
        private System.Windows.Forms.RadioButton radTrongNgay;
        private System.Windows.Forms.RadioButton radCPN;
        private System.Windows.Forms.RadioButton radThuong;
        private System.Windows.Forms.Panel pnlTongKet;
        private System.Windows.Forms.Button btnSangThanhToan;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Label lblPhiShip;
        private System.Windows.Forms.Label lblTienHang;
    }
}