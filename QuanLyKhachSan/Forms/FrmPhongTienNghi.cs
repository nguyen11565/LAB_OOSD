using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;
namespace
QuanLyKhachSan.Forms
{
    public partial class FrmPhongTienNghi : Form
    {
        readonly
PhongTienNghiService s = new PhongTienNghiService();
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private ComboBox cboTN;
        private Label label4;
        private Label label2;
        private Label label1;
        private Label label3;
        private DataGridView dgvLapDat;
        private TextBox txtSoLD;
        private Button btnLapDat;
        private Button button1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private NumericUpDown numericUpDown1;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ComboBox comboBox1;
        private NumericUpDown numericUpDown2;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private ComboBox cboLoai;
        private NumericUpDown numSTT;
        private TextBox txtTinhTrang;
        private TextBox txtMaTN;
        private Button btnThemTN;
        private DataGridView dgvTN;
        private ComboBox cboPhong;
        private Label label15;
        private Label label14;
        private Label label13;
        private TextBox txtGhiChu;
        private ComboBox cboNV;
        private TextBox txtTTLD;
        private DateTimePicker dtNgay;
        readonly DanhMucService dm = new
DanhMucService(); public FrmPhongTienNghi() { InitializeComponent(); }
        private void
Frm_Load(object a, EventArgs
e)
        {
            cboTN.DataSource = dm.LayKhuVuc(); cboTN.DisplayMember = "TenKhuVuc"; cboKhu.Val
ueMember = "MaKhuVuc"; cboLoai.DataSource = dm.LayLoaiTienNghi(); cboLoai.DisplayMemb
er = "TenLoaiTN"; cboLoai.ValueMember = "MaLoaiTN"; cboTN.DataSource = s.LayTienNghi(); cb
oTN.DisplayMember = "MaTienNghi"; cboTN.ValueMember = "MaTienNghi"; cboPhong.DataSou
rce = s.LayPhong(); cboPhong.DisplayMember = "SoPhong"; cboPhong.ValueMember = "SoPhong"
; cboNV.DataSource = dm.LayNhanVien(); cboNV.DisplayMember = "HoTen"; cboNV.ValueMem
ber = "MaNV"; Tai();
        }
        void
Tai()
        {
            dgvLapDat.DataSource = s.LayPhong(); dgvTN.DataSource = s.LayTienNghi(); dgvLD.DataS
ource = s.LayLapDat();
        }
        void H(KetQuaXuLy
k)
        { MessageBox.Show(k.ThongBao); if (k.ThanhCong) Tai(); }
        private void
btnThemPhong_Click(object a, EventArgs
e)
        { H(s.ThemPhong(txtSoLD.Text.Trim(), V(cboTN), (int)numMax.Value, numGia.Value)); }
        private void btnThemTN_Click(object a, EventArgs
        e)
        {
            H(s.ThemTienNghi(txtMaTN.Text.Trim(), V(cboLoai), (int)numSTT.Value, txtTinhTrang.Tex
        t.Trim()));
        }
        private void btnLapDat_Click(object a, EventArgs
        e)
        {
            H(s.LapDat(txtSoLD.Text.Trim(), V(cboTN), V(cboPhong), dtNgay.Value, txtTTLD.Text.Tri
        m(), V(cboNV), txtGhiChu.Text.Trim()));
        }
        string V(ComboBox c)
        {
            return
        c.SelectedValue == null ? "" : c.SelectedValue.ToString();
        }
        private void btnDong_Click(object
        a, EventArgs e)
        { Close(); }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cboTN = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvLapDat = new System.Windows.Forms.DataGridView();
            this.txtSoLD = new System.Windows.Forms.TextBox();
            this.btnLapDat = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.txtMaTN = new System.Windows.Forms.TextBox();
            this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.numSTT = new System.Windows.Forms.NumericUpDown();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvTN = new System.Windows.Forms.DataGridView();
            this.btnThemTN = new System.Windows.Forms.Button();
            this.cboPhong = new System.Windows.Forms.ComboBox();
            this.dtNgay = new System.Windows.Forms.DateTimePicker();
            this.txtTTLD = new System.Windows.Forms.TextBox();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLapDat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(40, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1047, 414);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.numericUpDown1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.numericUpDown2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1039, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "[Phòng]";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnThemTN);
            this.tabPage2.Controls.Add(this.dgvTN);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.cboLoai);
            this.tabPage2.Controls.Add(this.numSTT);
            this.tabPage2.Controls.Add(this.txtTinhTrang);
            this.tabPage2.Controls.Add(this.txtMaTN);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1039, 385);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "[Tiện nghi]";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.txtGhiChu);
            this.tabPage3.Controls.Add(this.cboNV);
            this.tabPage3.Controls.Add(this.txtTTLD);
            this.tabPage3.Controls.Add(this.dtNgay);
            this.tabPage3.Controls.Add(this.cboPhong);
            this.tabPage3.Controls.Add(this.btnLapDat);
            this.tabPage3.Controls.Add(this.txtSoLD);
            this.tabPage3.Controls.Add(this.dgvLapDat);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.cboTN);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1039, 385);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "[Phiếu lắp đặt]";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cboTN
            // 
            this.cboTN.FormattingEnabled = true;
            this.cboTN.Location = new System.Drawing.Point(172, 53);
            this.cboTN.Name = "cboTN";
            this.cboTN.Size = new System.Drawing.Size(121, 24);
            this.cboTN.TabIndex = 34;
            this.cboTN.SelectedIndexChanged += new System.EventHandler(this.cboKhu_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 35;
            this.label3.Text = "Ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 36;
            this.label1.Text = "Phòng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 37;
            this.label2.Text = "Tiện nghi:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 38;
            this.label4.Text = "Sốphiếu:";
            // 
            // dgvLapDat
            // 
            this.dgvLapDat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLapDat.Location = new System.Drawing.Point(74, 141);
            this.dgvLapDat.Name = "dgvLapDat";
            this.dgvLapDat.RowHeadersWidth = 51;
            this.dgvLapDat.RowTemplate.Height = 24;
            this.dgvLapDat.Size = new System.Drawing.Size(860, 224);
            this.dgvLapDat.TabIndex = 40;
            // 
            // txtSoLD
            // 
            this.txtSoLD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSoLD.Location = new System.Drawing.Point(172, 21);
            this.txtSoLD.Name = "txtSoLD";
            this.txtSoLD.Size = new System.Drawing.Size(121, 22);
            this.txtSoLD.TabIndex = 41;
            // 
            // btnLapDat
            // 
            this.btnLapDat.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnLapDat.Location = new System.Drawing.Point(851, 48);
            this.btnLapDat.Name = "btnLapDat";
            this.btnLapDat.Size = new System.Drawing.Size(135, 45);
            this.btnLapDat.TabIndex = 42;
            this.btnLapDat.Text = "Lập phiếu";
            this.btnLapDat.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.Location = new System.Drawing.Point(841, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 45);
            this.button1.TabIndex = 52;
            this.button1.Text = "Thêm phòng";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1.Location = new System.Drawing.Point(162, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 22);
            this.textBox1.TabIndex = 51;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(63, 142);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(860, 204);
            this.dataGridView1.TabIndex = 50;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(652, 94);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(74, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 48;
            this.label5.Text = "Số phòng:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 47;
            this.label6.Text = "Khu vực:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(540, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 16);
            this.label7.TabIndex = 46;
            this.label7.Text = "Sức chứa:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(540, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 16);
            this.label8.TabIndex = 45;
            this.label8.Text = "Đơn giá/ngày:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(162, 78);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 44;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(652, 43);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown2.TabIndex = 43;
            // 
            // txtMaTN
            // 
            this.txtMaTN.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtMaTN.Location = new System.Drawing.Point(236, 45);
            this.txtMaTN.Name = "txtMaTN";
            this.txtMaTN.Size = new System.Drawing.Size(121, 22);
            this.txtMaTN.TabIndex = 52;
            // 
            // txtTinhTrang
            // 
            this.txtTinhTrang.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTinhTrang.Location = new System.Drawing.Point(649, 86);
            this.txtTinhTrang.Name = "txtTinhTrang";
            this.txtTinhTrang.Size = new System.Drawing.Size(121, 22);
            this.txtTinhTrang.TabIndex = 53;
            // 
            // numSTT
            // 
            this.numSTT.Location = new System.Drawing.Point(650, 45);
            this.numSTT.Name = "numSTT";
            this.numSTT.Size = new System.Drawing.Size(120, 22);
            this.numSTT.TabIndex = 54;
            // 
            // cboLoai
            // 
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Location = new System.Drawing.Point(236, 86);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(121, 24);
            this.cboLoai.TabIndex = 55;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(566, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 16);
            this.label9.TabIndex = 56;
            this.label9.Text = "Tình trạng:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(566, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 16);
            this.label10.TabIndex = 57;
            this.label10.Text = "Số thứ tự:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(142, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 16);
            this.label11.TabIndex = 58;
            this.label11.Text = "Loại tiện nghi:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(142, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 16);
            this.label12.TabIndex = 59;
            this.label12.Text = "Mã tiện nghi:";
            // 
            // dgvTN
            // 
            this.dgvTN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTN.Location = new System.Drawing.Point(95, 138);
            this.dgvTN.Name = "dgvTN";
            this.dgvTN.RowHeadersWidth = 51;
            this.dgvTN.RowTemplate.Height = 24;
            this.dgvTN.Size = new System.Drawing.Size(849, 218);
            this.dgvTN.TabIndex = 60;
            // 
            // btnThemTN
            // 
            this.btnThemTN.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThemTN.Location = new System.Drawing.Point(823, 47);
            this.btnThemTN.Name = "btnThemTN";
            this.btnThemTN.Size = new System.Drawing.Size(135, 45);
            this.btnThemTN.TabIndex = 61;
            this.btnThemTN.Text = "Thêm tien nghi";
            this.btnThemTN.UseVisualStyleBackColor = false;
            // 
            // cboPhong
            // 
            this.cboPhong.FormattingEnabled = true;
            this.cboPhong.Location = new System.Drawing.Point(172, 83);
            this.cboPhong.Name = "cboPhong";
            this.cboPhong.Size = new System.Drawing.Size(121, 24);
            this.cboPhong.TabIndex = 43;
            // 
            // dtNgay
            // 
            this.dtNgay.Location = new System.Drawing.Point(172, 113);
            this.dtNgay.Name = "dtNgay";
            this.dtNgay.Size = new System.Drawing.Size(121, 22);
            this.dtNgay.TabIndex = 44;
            this.dtNgay.ValueChanged += new System.EventHandler(this.dtNgay_ValueChanged);
            // 
            // txtTTLD
            // 
            this.txtTTLD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTTLD.Location = new System.Drawing.Point(603, 27);
            this.txtTTLD.Name = "txtTTLD";
            this.txtTTLD.Size = new System.Drawing.Size(121, 22);
            this.txtTTLD.TabIndex = 45;
            // 
            // cboNV
            // 
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Location = new System.Drawing.Point(604, 59);
            this.cboNV.Name = "cboNV";
            this.cboNV.Size = new System.Drawing.Size(121, 24);
            this.cboNV.TabIndex = 46;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtGhiChu.Location = new System.Drawing.Point(603, 96);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(121, 22);
            this.txtGhiChu.TabIndex = 47;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(505, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 16);
            this.label13.TabIndex = 48;
            this.label13.Text = "Ghi chú:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(505, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 16);
            this.label14.TabIndex = 49;
            this.label14.Text = "Nhân viên:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(505, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 16);
            this.label15.TabIndex = 50;
            this.label15.Text = "Tình trạng:";
            // 
            // FrmPhongTienNghi
            // 
            this.ClientSize = new System.Drawing.Size(1099, 459);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmPhongTienNghi";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLapDat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).EndInit();
            this.ResumeLayout(false);

        }

        private void cboKhu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtNgay_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}