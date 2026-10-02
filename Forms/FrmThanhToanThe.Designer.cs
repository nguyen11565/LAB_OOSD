namespace shopping.Forms
{
    partial class FrmThanhToanThe
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
            this.gbThe = new System.Windows.Forms.GroupBox();
            this.txtCSV = new System.Windows.Forms.TextBox();
            this.lblCSV = new System.Windows.Forms.Label();
            this.txtExpNam = new System.Windows.Forms.TextBox();
            this.lblGach = new System.Windows.Forms.Label();
            this.txtExpThang = new System.Windows.Forms.TextBox();
            this.lblHanDung = new System.Windows.Forms.Label();
            this.txtChuThe = new System.Windows.Forms.TextBox();
            this.lblChuThe = new System.Windows.Forms.Label();
            this.txtSoThe = new System.Windows.Forms.TextBox();
            this.lblSoThe = new System.Windows.Forms.Label();
            this.cboLoaiThe = new System.Windows.Forms.ComboBox();
            this.lblLoaiThe = new System.Windows.Forms.Label();
            this.pnlTongTien = new System.Windows.Forms.Panel();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.lblTongThanhToan = new System.Windows.Forms.Label();
            this.lblPhiThe = new System.Windows.Forms.Label();
            this.lblTienShip = new System.Windows.Forms.Label();
            this.lblTienHang = new System.Windows.Forms.Label();
            this.gbThe.SuspendLayout();
            this.pnlTongTien.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.lblTieuDe.Location = new System.Drawing.Point(20, 20);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(601, 24);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "BƯỚC 2/2: THANH TOÁN THẺ TÍN DỤNG QUỐC TẾ (BR07, BR10)";
            // 
            // gbThe
            // 
            this.gbThe.Controls.Add(this.txtCSV);
            this.gbThe.Controls.Add(this.lblCSV);
            this.gbThe.Controls.Add(this.txtExpNam);
            this.gbThe.Controls.Add(this.lblGach);
            this.gbThe.Controls.Add(this.txtExpThang);
            this.gbThe.Controls.Add(this.lblHanDung);
            this.gbThe.Controls.Add(this.txtChuThe);
            this.gbThe.Controls.Add(this.lblChuThe);
            this.gbThe.Controls.Add(this.txtSoThe);
            this.gbThe.Controls.Add(this.lblSoThe);
            this.gbThe.Controls.Add(this.cboLoaiThe);
            this.gbThe.Controls.Add(this.lblLoaiThe);
            this.gbThe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.gbThe.Location = new System.Drawing.Point(20, 60);
            this.gbThe.Name = "gbThe";
            this.gbThe.Size = new System.Drawing.Size(640, 240);
            this.gbThe.TabIndex = 1;
            this.gbThe.TabStop = false;
            this.gbThe.Text = "THÔNG TIN THẺ TÍN DỤNG";
            // 
            // txtCSV
            // 
            this.txtCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCSV.Location = new System.Drawing.Point(440, 185);
            this.txtCSV.Name = "txtCSV";
            this.txtCSV.PasswordChar = '*';
            this.txtCSV.Size = new System.Drawing.Size(80, 26);
            this.txtCSV.TabIndex = 11;
            // 
            // lblCSV
            // 
            this.lblCSV.AutoSize = true;
            this.lblCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCSV.Location = new System.Drawing.Point(340, 188);
            this.lblCSV.Name = "lblCSV";
            this.lblCSV.Size = new System.Drawing.Size(84, 18);
            this.lblCSV.TabIndex = 10;
            this.lblCSV.Text = "Mã CSV: *";
            // 
            // txtExpNam
            // 
            this.txtExpNam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtExpNam.Location = new System.Drawing.Point(240, 185);
            this.txtExpNam.Name = "txtExpNam";
            this.txtExpNam.Size = new System.Drawing.Size(60, 26);
            this.txtExpNam.TabIndex = 9;
            this.txtExpNam.Text = "2028";
            // 
            // lblGach
            // 
            this.lblGach.AutoSize = true;
            this.lblGach.Location = new System.Drawing.Point(220, 188);
            this.lblGach.Name = "lblGach";
            this.lblGach.Size = new System.Drawing.Size(13, 18);
            this.lblGach.TabIndex = 8;
            this.lblGach.Text = "/";
            // 
            // txtExpThang
            // 
            this.txtExpThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtExpThang.Location = new System.Drawing.Point(170, 185);
            this.txtExpThang.Name = "txtExpThang";
            this.txtExpThang.Size = new System.Drawing.Size(45, 26);
            this.txtExpThang.TabIndex = 7;
            this.txtExpThang.Text = "12";
            // 
            // lblHanDung
            // 
            this.lblHanDung.AutoSize = true;
            this.lblHanDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblHanDung.Location = new System.Drawing.Point(30, 188);
            this.lblHanDung.Name = "lblHanDung";
            this.lblHanDung.Size = new System.Drawing.Size(124, 18);
            this.lblHanDung.TabIndex = 6;
            this.lblHanDung.Text = "Hạn dùng (M/Y): *";
            // 
            // txtChuThe
            // 
            this.txtChuThe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtChuThe.Location = new System.Drawing.Point(170, 135);
            this.txtChuThe.Name = "txtChuThe";
            this.txtChuThe.Size = new System.Drawing.Size(440, 26);
            this.txtChuThe.TabIndex = 5;
            // 
            // lblChuThe
            // 
            this.lblChuThe.AutoSize = true;
            this.lblChuThe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblChuThe.Location = new System.Drawing.Point(30, 138);
            this.lblChuThe.Name = "lblChuThe";
            this.lblChuThe.Size = new System.Drawing.Size(117, 18);
            this.lblChuThe.TabIndex = 4;
            this.lblChuThe.Text = "Họ tên chủ thẻ: *";
            // 
            // txtSoThe
            // 
            this.txtSoThe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSoThe.Location = new System.Drawing.Point(170, 85);
            this.txtSoThe.Name = "txtSoThe";
            this.txtSoThe.Size = new System.Drawing.Size(440, 26);
            this.txtSoThe.TabIndex = 3;
            // 
            // lblSoThe
            // 
            this.lblSoThe.AutoSize = true;
            this.lblSoThe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblSoThe.Location = new System.Drawing.Point(30, 88);
            this.lblSoThe.Name = "lblSoThe";
            this.lblSoThe.Size = new System.Drawing.Size(109, 18);
            this.lblSoThe.TabIndex = 2;
            this.lblSoThe.Text = "Số hiệu thẻ: *";
            // 
            // cboLoaiThe
            // 
            this.cboLoaiThe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiThe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboLoaiThe.FormattingEnabled = true;
            this.cboLoaiThe.Items.AddRange(new object[] {
            "VISA (16 số, CSV 3 số)",
            "MASTERCARD (16 số, CSV 3 số)",
            "DISCOVER (16 số, CSV 3 số)",
            "AMEX (15 số, CSV 4 số)"});
            this.cboLoaiThe.Location = new System.Drawing.Point(170, 35);
            this.cboLoaiThe.Name = "cboLoaiThe";
            this.cboLoaiThe.Size = new System.Drawing.Size(440, 26);
            this.cboLoaiThe.TabIndex = 1;
            this.cboLoaiThe.SelectedIndexChanged += new System.EventHandler(this.cboLoaiThe_SelectedIndexChanged);
            // 
            // lblLoaiThe
            // 
            this.lblLoaiThe.AutoSize = true;
            this.lblLoaiThe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLoaiThe.Location = new System.Drawing.Point(30, 38);
            this.lblLoaiThe.Name = "lblLoaiThe";
            this.lblLoaiThe.Size = new System.Drawing.Size(77, 18);
            this.lblLoaiThe.TabIndex = 0;
            this.lblLoaiThe.Text = "Loại thẻ: *";
            // 
            // pnlTongTien
            // 
            this.pnlTongTien.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlTongTien.Controls.Add(this.btnXacNhan);
            this.pnlTongTien.Controls.Add(this.btnQuayLai);
            this.pnlTongTien.Controls.Add(this.lblTongThanhToan);
            this.pnlTongTien.Controls.Add(this.lblPhiThe);
            this.pnlTongTien.Controls.Add(this.lblTienShip);
            this.pnlTongTien.Controls.Add(this.lblTienHang);
            this.pnlTongTien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongTien.Location = new System.Drawing.Point(0, 320);
            this.pnlTongTien.Name = "pnlTongTien";
            this.pnlTongTien.Size = new System.Drawing.Size(684, 150);
            this.pnlTongTien.TabIndex = 2;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnXacNhan.Location = new System.Drawing.Point(390, 85);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(270, 48);
            this.btnXacNhan.TabIndex = 5;
            this.btnXacNhan.Text = "XÁC NHẬN THANH TOÁN";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnQuayLai.Location = new System.Drawing.Point(24, 85);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(160, 48);
            this.btnQuayLai.TabIndex = 4;
            this.btnQuayLai.Text = "<< Sửa giao hàng";
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // lblTongThanhToan
            // 
            this.lblTongThanhToan.AutoSize = true;
            this.lblTongThanhToan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongThanhToan.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            this.lblTongThanhToan.Location = new System.Drawing.Point(20, 50);
            this.lblTongThanhToan.Name = "lblTongThanhToan";
            this.lblTongThanhToan.Size = new System.Drawing.Size(288, 24);
            this.lblTongThanhToan.TabIndex = 3;
            this.lblTongThanhToan.Text = "TỔNG TIỀN TRỪ THẺ: 000 đ";
            // 
            // lblPhiThe
            // 
            this.lblPhiThe.AutoSize = true;
            this.lblPhiThe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPhiThe.Location = new System.Drawing.Point(450, 18);
            this.lblPhiThe.Name = "lblPhiThe";
            this.lblPhiThe.Size = new System.Drawing.Size(126, 18);
            this.lblPhiThe.TabIndex = 2;
            this.lblPhiThe.Text = "Phí quẹt thẻ: 0 đ";
            // 
            // lblTienShip
            // 
            this.lblTienShip.AutoSize = true;
            this.lblTienShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTienShip.Location = new System.Drawing.Point(240, 18);
            this.lblTienShip.Name = "lblTienShip";
            this.lblTienShip.Size = new System.Drawing.Size(95, 18);
            this.lblTienShip.TabIndex = 1;
            this.lblTienShip.Text = "Phí ship: 0 đ";
            // 
            // lblTienHang
            // 
            this.lblTienHang.AutoSize = true;
            this.lblTienHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTienHang.Location = new System.Drawing.Point(20, 18);
            this.lblTienHang.Name = "lblTienHang";
            this.lblTienHang.Size = new System.Drawing.Size(146, 18);
            this.lblTienHang.TabIndex = 0;
            this.lblTienHang.Text = "Tiền hàng: 000.000 đ";
            // 
            // FrmThanhToanThe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 470);
            this.Controls.Add(this.pnlTongTien);
            this.Controls.Add(this.gbThe);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmThanhToanThe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bước 2/2: Thanh toán thẻ tín dụng quốc tế";
            this.Load += new System.EventHandler(this.FrmThanhToanThe_Load);
            this.gbThe.ResumeLayout(false);
            this.gbThe.PerformLayout();
            this.pnlTongTien.ResumeLayout(false);
            this.pnlTongTien.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox gbThe;
        private System.Windows.Forms.TextBox txtCSV;
        private System.Windows.Forms.Label lblCSV;
        private System.Windows.Forms.TextBox txtExpNam;
        private System.Windows.Forms.Label lblGach;
        private System.Windows.Forms.TextBox txtExpThang;
        private System.Windows.Forms.Label lblHanDung;
        private System.Windows.Forms.TextBox txtChuThe;
        private System.Windows.Forms.Label lblChuThe;
        private System.Windows.Forms.TextBox txtSoThe;
        private System.Windows.Forms.Label lblSoThe;
        private System.Windows.Forms.ComboBox cboLoaiThe;
        private System.Windows.Forms.Label lblLoaiThe;
        private System.Windows.Forms.Panel pnlTongTien;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Label lblTongThanhToan;
        private System.Windows.Forms.Label lblPhiThe;
        private System.Windows.Forms.Label lblTienShip;
        private System.Windows.Forms.Label lblTienHang;
    }
}