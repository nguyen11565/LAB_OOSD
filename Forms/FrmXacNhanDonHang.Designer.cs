namespace shopping.Forms
{
    partial class FrmXacNhanDonHang
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblLoiCamOn = new System.Windows.Forms.Label();
            this.lblMaDonHang = new System.Windows.Forms.Label();
            this.lblThongBaoEmail = new System.Windows.Forms.Label();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.lblTheMasked = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.lblDiaChiNhan = new System.Windows.Forms.Label();
            this.lblNguoiNhan = new System.Windows.Forms.Label();
            this.btnVeTrangChu = new System.Windows.Forms.Button();
            this.gbThongTin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
            this.lblTieuDe.Location = new System.Drawing.Point(60, 25);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(534, 29);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "🎉 ĐẶT MUA HÀNG VÀ THANH TOÁN THÀNH CÔNG!";
            // 
            // lblLoiCamOn
            // 
            this.lblLoiCamOn.AutoSize = true;
            this.lblLoiCamOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblLoiCamOn.Location = new System.Drawing.Point(30, 70);
            this.lblLoiCamOn.Name = "lblLoiCamOn";
            this.lblLoiCamOn.Size = new System.Drawing.Size(434, 20);
            this.lblLoiCamOn.TabIndex = 1;
            this.lblLoiCamOn.Text = "Cảm ơn Quý khách đã mua sắm tại Cửa hàng trực tuyến ABC!";
            // 
            // lblMaDonHang
            // 
            this.lblMaDonHang.AutoSize = true;
            this.lblMaDonHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblMaDonHang.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.lblMaDonHang.Location = new System.Drawing.Point(30, 100);
            this.lblMaDonHang.Name = "lblMaDonHang";
            this.lblMaDonHang.Size = new System.Drawing.Size(189, 20);
            this.lblMaDonHang.TabIndex = 2;
            this.lblMaDonHang.Text = "MÃ ĐƠN HÀNG: #ORD";
            // 
            // lblThongBaoEmail
            // 
            this.lblThongBaoEmail.AutoSize = true;
            this.lblThongBaoEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.lblThongBaoEmail.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblThongBaoEmail.Location = new System.Drawing.Point(30, 130);
            this.lblThongBaoEmail.Name = "lblThongBaoEmail";
            this.lblThongBaoEmail.Size = new System.Drawing.Size(466, 18);
            this.lblThongBaoEmail.TabIndex = 3;
            this.lblThongBaoEmail.Text = "Thông báo biên nhận đã được gửi tự động qua email (Bảo mật BR12, BR13)";
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.lblTheMasked);
            this.gbThongTin.Controls.Add(this.lblTongTien);
            this.gbThongTin.Controls.Add(this.lblHinhThuc);
            this.gbThongTin.Controls.Add(this.lblDiaChiNhan);
            this.gbThongTin.Controls.Add(this.lblNguoiNhan);
            this.gbThongTin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbThongTin.Location = new System.Drawing.Point(30, 165);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(590, 200);
            this.gbThongTin.TabIndex = 4;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "CHI TIẾT ĐƠN HÀNG";
            // 
            // lblTheMasked
            // 
            this.lblTheMasked.AutoSize = true;
            this.lblTheMasked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTheMasked.Location = new System.Drawing.Point(20, 160);
            this.lblTheMasked.Name = "lblTheMasked";
            this.lblTheMasked.Size = new System.Drawing.Size(262, 18);
            this.lblTheMasked.TabIndex = 4;
            this.lblTheMasked.Text = "Thẻ thanh toán: VISA **** **** **** 1234";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.lblTongTien.Location = new System.Drawing.Point(20, 130);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(306, 20);
            this.lblTongTien.TabIndex = 3;
            this.lblTongTien.Text = "TỔNG TIỀN ĐÃ TRẢ: 000.000 VNĐ";
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblHinhThuc.Location = new System.Drawing.Point(20, 100);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(206, 18);
            this.lblHinhThuc.TabIndex = 2;
            this.lblHinhThuc.Text = "Hình thức giao: Tiêu chuẩn (3-5 ngày)";
            // 
            // lblDiaChiNhan
            // 
            this.lblDiaChiNhan.AutoSize = true;
            this.lblDiaChiNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblDiaChiNhan.Location = new System.Drawing.Point(20, 68);
            this.lblDiaChiNhan.Name = "lblDiaChiNhan";
            this.lblDiaChiNhan.Size = new System.Drawing.Size(125, 18);
            this.lblDiaChiNhan.TabIndex = 1;
            this.lblDiaChiNhan.Text = "Địa chỉ: TP.HCM";
            // 
            // lblNguoiNhan
            // 
            this.lblNguoiNhan.AutoSize = true;
            this.lblNguoiNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblNguoiNhan.Location = new System.Drawing.Point(20, 35);
            this.lblNguoiNhan.Name = "lblNguoiNhan";
            this.lblNguoiNhan.Size = new System.Drawing.Size(183, 18);
            this.lblNguoiNhan.TabIndex = 0;
            this.lblNguoiNhan.Text = "Người nhận: Nguyễn Văn B";
            // 
            // btnVeTrangChu
            // 
            this.btnVeTrangChu.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.btnVeTrangChu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVeTrangChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnVeTrangChu.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnVeTrangChu.Location = new System.Drawing.Point(210, 385);
            this.btnVeTrangChu.Name = "btnVeTrangChu";
            this.btnVeTrangChu.Size = new System.Drawing.Size(230, 45);
            this.btnVeTrangChu.TabIndex = 5;
            this.btnVeTrangChu.Text = "VỀ TRANG CHỦ MUA SẮM";
            this.btnVeTrangChu.UseVisualStyleBackColor = false;
            this.btnVeTrangChu.Click += new System.EventHandler(this.btnVeTrangChu_Click);
            // 
            // FrmXacNhanDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 455);
            this.Controls.Add(this.btnVeTrangChu);
            this.Controls.Add(this.gbThongTin);
            this.Controls.Add(this.lblThongBaoEmail);
            this.Controls.Add(this.lblMaDonHang);
            this.Controls.Add(this.lblLoiCamOn);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmXacNhanDonHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đặt hàng và Thanh toán thành công";
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblLoiCamOn;
        private System.Windows.Forms.Label lblMaDonHang;
        private System.Windows.Forms.Label lblThongBaoEmail;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.Label lblTheMasked;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblHinhThuc;
        private System.Windows.Forms.Label lblDiaChiNhan;
        private System.Windows.Forms.Label lblNguoiNhan;
        private System.Windows.Forms.Button btnVeTrangChu;
    }
}