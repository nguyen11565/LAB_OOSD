using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;
namespace
QuanLyKhachSan.Forms
{
    public partial class FrmDanhMuc : Form
    {
        private Label label1;
        private Label label2;
        private Button button1;
        private Button btnThemQD;
        private TabControl tabControl1;
        private TabPage tabDanhMuc;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private Button btnThemKhu;
        private Label label5;
        private Label label4;
        private Label label3;
        private DataGridView dgvKhu;
        private Label label7;
        private TextBox txtKhuMa;
        private TextBox txtKhuTen;
        private Label label8;
        private TextBox txtNVMa;
        private TextBox txtNVVaiTro;
        private TextBox txtNVSDT;
        private TextBox txtNVTen;
        private DataGridView dgvNV;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Button btnThemNV;
        private DataGridView dgvLoaiTN;
        private DataGridView dgvDV;
        private DataGridView dgvQD;
        private TextBox txtLoaiMa;
        private TextBox txtLoaiTen;
        private Label label14;
        private Label label13;
        private Button btnThemLoaiTN;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private TextBox txtDVMa;
        private TextBox txtDVTen;
        private TextBox txtDVDVT;
        private NumericUpDown numDVGia;
        private Button btnThemDV;
        private NumericUpDown numQDTien;
        private TextBox txtQDMa;
        private TextBox txtQDMucDo;
        private ComboBox cboQDLoai;
        private Label label19;
        readonly DanhMucService
s = new DanhMucService(); public FrmDanhMuc() { InitializeComponent(); }
        private void
FrmDanhMuc_Load(object a, EventArgs e)
        { Tai(); }
        voidTai()
        {
            dgvKhu.DataSource = s.LayKhuVuc(); dgvNV.DataSource = s.LayNhanVien(); dgvLoaiTN.
DataSource = s.LayLoaiTienNghi(); dgvDV.DataSource = s.LayDichVu(); dgvQD.DataSource = s.L
ayQuyDinhDenBu(); cboQDLoai.DataSource = s.LayLoaiTienNghi(); cboQDLoai.DisplayMembe
r = "TenLoaiTN"; cboQDLoai.ValueMember = "MaLoaiTN";
        }
        void H(KetQuaXuLy
k)
        { MessageBox.Show(k.ThongBao); if (k.ThanhCong) Tai(); }
        private void
btnThemKhu_Click(object a, EventArgs
e)
        { H(s.ThemKhu(txtKhuMa.Text.Trim(), txtKhuTen.Text.Trim())); }
        private void
btnThemNV_Click(object a, EventArgs
e)
        {
            H(s.ThemNhanVien(txtNVMa.Text.Trim(), txtNVTen.Text.Trim(), txtNVVaiTro.Text.Trim(),
txtNVSDT.Text.Trim()));
        }
        private void btnThemLoaiTN_Click(object a, EventArgs
e)
        { H(s.ThemLoaiTN(txtLoaiMa.Text.Trim(), txtLoaiTen.Text.Trim())); }
        private void
btnThemDV_Click(object a, EventArgs
e)
        {
            H(s.ThemDichVu(txtDVMa.Text.Trim(), txtDVTen.Text.Trim(), txtDVDVT.Text.Trim(), num
DVGia.Value));
        }
        private void btnThemQD_Click(object a, EventArgs
e)
        {
            H(s.ThemQuyDinh(txtQDMa.Text.Trim(), cboQDLoai.SelectedValue == null ? "" : cboQDLoai.
SelectedValue.ToString(), txtQDMucDo.Text.Trim(), numQDTien.Value));
        }
        private void
btnDong_Click(object a, EventArgs e)
        { Close(); }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnThemQD = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDanhMuc = new System.Windows.Forms.TabPage();
            this.txtKhuTen = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKhuMa = new System.Windows.Forms.TextBox();
            this.dgvKhu = new System.Windows.Forms.DataGridView();
            this.btnThemKhu = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnThemNV = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNVMa = new System.Windows.Forms.TextBox();
            this.txtNVVaiTro = new System.Windows.Forms.TextBox();
            this.txtNVSDT = new System.Windows.Forms.TextBox();
            this.txtNVTen = new System.Windows.Forms.TextBox();
            this.dgvNV = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnThemLoaiTN = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLoaiMa = new System.Windows.Forms.TextBox();
            this.txtLoaiTen = new System.Windows.Forms.TextBox();
            this.dgvLoaiTN = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.numDVGia = new System.Windows.Forms.NumericUpDown();
            this.btnThemDV = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDVMa = new System.Windows.Forms.TextBox();
            this.txtDVTen = new System.Windows.Forms.TextBox();
            this.txtDVDVT = new System.Windows.Forms.TextBox();
            this.dgvDV = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.numQDTien = new System.Windows.Forms.NumericUpDown();
            this.txtQDMa = new System.Windows.Forms.TextBox();
            this.txtQDMucDo = new System.Windows.Forms.TextBox();
            this.cboQDLoai = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dgvQD = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabDanhMuc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhu)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiTN)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDVGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQDTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQD)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 26);
            this.button1.TabIndex = 2;
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnThemQD
            // 
            this.btnThemQD.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThemQD.Location = new System.Drawing.Point(760, 28);
            this.btnThemQD.Name = "btnThemQD";
            this.btnThemQD.Size = new System.Drawing.Size(83, 45);
            this.btnThemQD.TabIndex = 5;
            this.btnThemQD.Text = "Thêm";
            this.btnThemQD.UseVisualStyleBackColor = false;
            this.btnThemQD.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDanhMuc);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(3, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(971, 360);
            this.tabControl1.TabIndex = 6;
            // 
            // tabDanhMuc
            // 
            this.tabDanhMuc.Controls.Add(this.txtKhuTen);
            this.tabDanhMuc.Controls.Add(this.label8);
            this.tabDanhMuc.Controls.Add(this.label7);
            this.tabDanhMuc.Controls.Add(this.txtKhuMa);
            this.tabDanhMuc.Controls.Add(this.dgvKhu);
            this.tabDanhMuc.Controls.Add(this.btnThemKhu);
            this.tabDanhMuc.Location = new System.Drawing.Point(4, 25);
            this.tabDanhMuc.Name = "tabDanhMuc";
            this.tabDanhMuc.Padding = new System.Windows.Forms.Padding(3);
            this.tabDanhMuc.Size = new System.Drawing.Size(963, 331);
            this.tabDanhMuc.TabIndex = 0;
            this.tabDanhMuc.Text = "[Khu vực]";
            this.tabDanhMuc.UseVisualStyleBackColor = true;
            this.tabDanhMuc.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // txtKhuTen
            // 
            this.txtKhuTen.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtKhuTen.Location = new System.Drawing.Point(462, 47);
            this.txtKhuTen.Name = "txtKhuTen";
            this.txtKhuTen.Size = new System.Drawing.Size(100, 22);
            this.txtKhuTen.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(357, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Tên khu vực:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Mã khu vực:";
            // 
            // txtKhuMa
            // 
            this.txtKhuMa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtKhuMa.Location = new System.Drawing.Point(163, 44);
            this.txtKhuMa.Name = "txtKhuMa";
            this.txtKhuMa.Size = new System.Drawing.Size(100, 22);
            this.txtKhuMa.TabIndex = 10;
            // 
            // dgvKhu
            // 
            this.dgvKhu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhu.Location = new System.Drawing.Point(59, 97);
            this.dgvKhu.Name = "dgvKhu";
            this.dgvKhu.RowHeadersWidth = 51;
            this.dgvKhu.RowTemplate.Height = 24;
            this.dgvKhu.Size = new System.Drawing.Size(860, 204);
            this.dgvKhu.TabIndex = 9;
            // 
            // btnThemKhu
            // 
            this.btnThemKhu.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThemKhu.Location = new System.Drawing.Point(731, 40);
            this.btnThemKhu.Name = "btnThemKhu";
            this.btnThemKhu.Size = new System.Drawing.Size(83, 37);
            this.btnThemKhu.TabIndex = 6;
            this.btnThemKhu.Text = "Thêm";
            this.btnThemKhu.UseVisualStyleBackColor = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnThemNV);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.txtNVMa);
            this.tabPage2.Controls.Add(this.txtNVVaiTro);
            this.tabPage2.Controls.Add(this.txtNVSDT);
            this.tabPage2.Controls.Add(this.txtNVTen);
            this.tabPage2.Controls.Add(this.dgvNV);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(963, 331);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "[Nhân viên]";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnThemNV
            // 
            this.btnThemNV.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThemNV.Location = new System.Drawing.Point(731, 49);
            this.btnThemNV.Name = "btnThemNV";
            this.btnThemNV.Size = new System.Drawing.Size(83, 37);
            this.btnThemNV.TabIndex = 19;
            this.btnThemNV.Text = "Thêm";
            this.btnThemNV.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(59, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 16);
            this.label12.TabIndex = 18;
            this.label12.Text = "Họ tên:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(418, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = " Vai trò:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(432, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 16);
            this.label10.TabIndex = 16;
            this.label10.Text = "SĐT:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Mã NV:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // txtNVMa
            // 
            this.txtNVMa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNVMa.Location = new System.Drawing.Point(158, 31);
            this.txtNVMa.Name = "txtNVMa";
            this.txtNVMa.Size = new System.Drawing.Size(100, 22);
            this.txtNVMa.TabIndex = 14;
            // 
            // txtNVVaiTro
            // 
            this.txtNVVaiTro.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNVVaiTro.Location = new System.Drawing.Point(519, 28);
            this.txtNVVaiTro.Name = "txtNVVaiTro";
            this.txtNVVaiTro.Size = new System.Drawing.Size(100, 22);
            this.txtNVVaiTro.TabIndex = 13;
            // 
            // txtNVSDT
            // 
            this.txtNVSDT.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNVSDT.Location = new System.Drawing.Point(519, 70);
            this.txtNVSDT.Name = "txtNVSDT";
            this.txtNVSDT.Size = new System.Drawing.Size(100, 22);
            this.txtNVSDT.TabIndex = 12;
            // 
            // txtNVTen
            // 
            this.txtNVTen.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNVTen.Location = new System.Drawing.Point(158, 70);
            this.txtNVTen.Name = "txtNVTen";
            this.txtNVTen.Size = new System.Drawing.Size(100, 22);
            this.txtNVTen.TabIndex = 11;
            this.txtNVTen.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dgvNV
            // 
            this.dgvNV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNV.Location = new System.Drawing.Point(52, 121);
            this.dgvNV.Name = "dgvNV";
            this.dgvNV.RowHeadersWidth = 51;
            this.dgvNV.RowTemplate.Height = 24;
            this.dgvNV.Size = new System.Drawing.Size(860, 204);
            this.dgvNV.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnThemLoaiTN);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.txtLoaiMa);
            this.tabPage3.Controls.Add(this.txtLoaiTen);
            this.tabPage3.Controls.Add(this.dgvLoaiTN);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(963, 331);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "[Loại tiện nghi]";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnThemLoaiTN
            // 
            this.btnThemLoaiTN.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThemLoaiTN.Location = new System.Drawing.Point(747, 31);
            this.btnThemLoaiTN.Name = "btnThemLoaiTN";
            this.btnThemLoaiTN.Size = new System.Drawing.Size(83, 37);
            this.btnThemLoaiTN.TabIndex = 20;
            this.btnThemLoaiTN.Text = "Thêm";
            this.btnThemLoaiTN.UseVisualStyleBackColor = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(447, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 16);
            this.label14.TabIndex = 18;
            this.label14.Text = "Tên loại:";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(133, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 16);
            this.label13.TabIndex = 17;
            this.label13.Text = "Mã loại:";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // txtLoaiMa
            // 
            this.txtLoaiMa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtLoaiMa.Location = new System.Drawing.Point(228, 32);
            this.txtLoaiMa.Name = "txtLoaiMa";
            this.txtLoaiMa.Size = new System.Drawing.Size(100, 22);
            this.txtLoaiMa.TabIndex = 16;
            this.txtLoaiMa.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtLoaiTen
            // 
            this.txtLoaiTen.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtLoaiTen.Location = new System.Drawing.Point(514, 35);
            this.txtLoaiTen.Name = "txtLoaiTen";
            this.txtLoaiTen.Size = new System.Drawing.Size(100, 22);
            this.txtLoaiTen.TabIndex = 15;
            this.txtLoaiTen.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // dgvLoaiTN
            // 
            this.dgvLoaiTN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoaiTN.Location = new System.Drawing.Point(41, 89);
            this.dgvLoaiTN.Name = "dgvLoaiTN";
            this.dgvLoaiTN.RowHeadersWidth = 51;
            this.dgvLoaiTN.RowTemplate.Height = 24;
            this.dgvLoaiTN.Size = new System.Drawing.Size(860, 204);
            this.dgvLoaiTN.TabIndex = 11;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.numDVGia);
            this.tabPage4.Controls.Add(this.btnThemDV);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.txtDVMa);
            this.tabPage4.Controls.Add(this.txtDVTen);
            this.tabPage4.Controls.Add(this.txtDVDVT);
            this.tabPage4.Controls.Add(this.dgvDV);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(963, 331);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "[Dịch vụ]";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // numDVGia
            // 
            this.numDVGia.Location = new System.Drawing.Point(537, 63);
            this.numDVGia.Name = "numDVGia";
            this.numDVGia.Size = new System.Drawing.Size(120, 22);
            this.numDVGia.TabIndex = 26;
            // 
            // btnThemDV
            // 
            this.btnThemDV.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThemDV.Location = new System.Drawing.Point(757, 45);
            this.btnThemDV.Name = "btnThemDV";
            this.btnThemDV.Size = new System.Drawing.Size(83, 37);
            this.btnThemDV.TabIndex = 25;
            this.btnThemDV.Text = "Thêm";
            this.btnThemDV.UseVisualStyleBackColor = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(118, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 16);
            this.label18.TabIndex = 24;
            this.label18.Text = "Mã DV:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(118, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 16);
            this.label17.TabIndex = 23;
            this.label17.Text = "Tên DV:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(454, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 16);
            this.label16.TabIndex = 22;
            this.label16.Text = "Đơn vị tính:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(454, 69);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 21;
            this.label15.Text = "Đơn giá:";
            // 
            // txtDVMa
            // 
            this.txtDVMa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtDVMa.Location = new System.Drawing.Point(178, 28);
            this.txtDVMa.Name = "txtDVMa";
            this.txtDVMa.Size = new System.Drawing.Size(114, 22);
            this.txtDVMa.TabIndex = 20;
            // 
            // txtDVTen
            // 
            this.txtDVTen.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtDVTen.Location = new System.Drawing.Point(178, 63);
            this.txtDVTen.Name = "txtDVTen";
            this.txtDVTen.Size = new System.Drawing.Size(114, 22);
            this.txtDVTen.TabIndex = 19;
            // 
            // txtDVDVT
            // 
            this.txtDVDVT.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtDVDVT.Location = new System.Drawing.Point(537, 28);
            this.txtDVDVT.Name = "txtDVDVT";
            this.txtDVDVT.Size = new System.Drawing.Size(120, 22);
            this.txtDVDVT.TabIndex = 18;
            // 
            // dgvDV
            // 
            this.dgvDV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDV.Location = new System.Drawing.Point(55, 109);
            this.dgvDV.Name = "dgvDV";
            this.dgvDV.RowHeadersWidth = 51;
            this.dgvDV.RowTemplate.Height = 24;
            this.dgvDV.Size = new System.Drawing.Size(860, 204);
            this.dgvDV.TabIndex = 11;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.numQDTien);
            this.tabPage5.Controls.Add(this.txtQDMa);
            this.tabPage5.Controls.Add(this.txtQDMucDo);
            this.tabPage5.Controls.Add(this.cboQDLoai);
            this.tabPage5.Controls.Add(this.label19);
            this.tabPage5.Controls.Add(this.dgvQD);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Controls.Add(this.label3);
            this.tabPage5.Controls.Add(this.btnThemQD);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(963, 331);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "[Quy định đền bù]";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // numQDTien
            // 
            this.numQDTien.Location = new System.Drawing.Point(514, 59);
            this.numQDTien.Name = "numQDTien";
            this.numQDTien.Size = new System.Drawing.Size(120, 22);
            this.numQDTien.TabIndex = 32;
            // 
            // txtQDMa
            // 
            this.txtQDMa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtQDMa.Location = new System.Drawing.Point(128, 22);
            this.txtQDMa.Name = "txtQDMa";
            this.txtQDMa.Size = new System.Drawing.Size(121, 22);
            this.txtQDMa.TabIndex = 31;
            // 
            // txtQDMucDo
            // 
            this.txtQDMucDo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtQDMucDo.Location = new System.Drawing.Point(514, 25);
            this.txtQDMucDo.Name = "txtQDMucDo";
            this.txtQDMucDo.Size = new System.Drawing.Size(120, 22);
            this.txtQDMucDo.TabIndex = 30;
            // 
            // cboQDLoai
            // 
            this.cboQDLoai.FormattingEnabled = true;
            this.cboQDLoai.Location = new System.Drawing.Point(128, 57);
            this.cboQDLoai.Name = "cboQDLoai";
            this.cboQDLoai.Size = new System.Drawing.Size(121, 24);
            this.cboQDLoai.TabIndex = 28;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(398, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(102, 16);
            this.label19.TabIndex = 12;
            this.label19.Text = "Mức độ thiệt hại:";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // dgvQD
            // 
            this.dgvQD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQD.Location = new System.Drawing.Point(41, 101);
            this.dgvQD.Name = "dgvQD";
            this.dgvQD.RowHeadersWidth = 51;
            this.dgvQD.RowTemplate.Height = 24;
            this.dgvQD.Size = new System.Drawing.Size(860, 204);
            this.dgvQD.TabIndex = 11;
            this.dgvQD.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mức đền bù:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Loại tiện nghi:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mã quy định:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // FrmDanhMuc
            // 
            this.ClientSize = new System.Drawing.Size(997, 490);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmDanhMuc";
            this.Load += new System.EventHandler(this.FrmDanhMuc_Load_1);
            this.tabControl1.ResumeLayout(false);
            this.tabDanhMuc.ResumeLayout(false);
            this.tabDanhMuc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhu)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiTN)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDVGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQDTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void FrmDanhMuc_Load_1(object sender, EventArgs e)
        {

        }
    }
}