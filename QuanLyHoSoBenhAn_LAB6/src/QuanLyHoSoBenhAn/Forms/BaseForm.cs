using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>Reusable code-only WinForms UI, so the project does not depend on Designer files.</summary>
    public abstract class BaseForm : Form
    {
        protected readonly FlowLayoutPanel Toolbar = new FlowLayoutPanel();
        protected readonly DataGridView Grid = new DataGridView();
        protected readonly Label Status = new Label();
        protected readonly DanhMucService Catalog = new DanhMucService();

        protected BaseForm(string title, int toolbarHeight = 205)
        {
            Text = title;
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.FromArgb(248, 250, 253);
            FormBorderStyle = FormBorderStyle.None;
            Toolbar.Dock = DockStyle.Top;
            Toolbar.Height = toolbarHeight;
            Toolbar.Padding = new Padding(9, 7, 5, 0);
            Toolbar.WrapContents = true;
            Toolbar.AutoScroll = true;
            Toolbar.BackColor = Color.White;
            Toolbar.FlowDirection = FlowDirection.LeftToRight;
            Grid.Dock = DockStyle.Fill;
            Grid.ReadOnly = true;
            Grid.AllowUserToAddRows = false;
            Grid.AllowUserToDeleteRows = false;
            Grid.MultiSelect = false;
            Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Grid.RowHeadersVisible = false;
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            Grid.BackgroundColor = Color.White;
            Grid.BorderStyle = BorderStyle.FixedSingle;
            Grid.RowTemplate.Height = 27;
            Grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(239, 244, 252);
            Status.Dock = DockStyle.Bottom;
            Status.Height = 30;
            Status.ForeColor = Color.FromArgb(20, 70, 120);
            Status.TextAlign = ContentAlignment.MiddleLeft;
            Status.Padding = new Padding(12, 0, 0, 0);
            Status.Text = "Chọn một chức năng để bắt đầu.";
            Controls.Add(Grid);
            Controls.Add(Status);
            Controls.Add(Toolbar);
        }

        protected Panel Cell(string label, Control control, int width = 170)
        {
            var panel = new Panel { Width = width, Height = 54, Margin = new Padding(3, 2, 6, 6) };
            var title = new Label { Dock = DockStyle.Top, Text = label, Height = 20, AutoEllipsis = true };
            control.Dock = DockStyle.Bottom;
            control.Height = 26;
            panel.Controls.Add(control);
            panel.Controls.Add(title);
            Toolbar.Controls.Add(panel);
            return panel;
        }
        protected TextBox TextField(string label, int width = 170, string value = "")
        {
            var t = new TextBox { Text = value };
            Cell(label, t, width);
            return t;
        }
        protected NumericUpDown Number(string label, decimal initial = 0, int width = 125,
            decimal maximum = 999999999)
        {
            var n = new NumericUpDown { Minimum = 0, Maximum = maximum, Value = initial,
                ThousandsSeparator = true, DecimalPlaces = 0 };
            Cell(label, n, width);
            return n;
        }
        protected NumericUpDown DecimalField(string label, decimal initial, int width = 145)
        {
            var n = new NumericUpDown { Minimum = 0, Maximum = 999999999, Value = initial,
                DecimalPlaces = 1, Increment = 0.1m };
            Cell(label, n, width);
            return n;
        }
        protected DateTimePicker DateField(string label, DateTime initial, bool dateTime = false, int width = 165)
        {
            var d = new DateTimePicker { Value = initial,
               Format = DateTimePickerFormat.Custom,
               CustomFormat = dateTime ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy" };
            Cell(label, d, width);
            return d;
        }
        protected ComboBox Choices(string label, params string[] options)
        {
            var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            combo.Items.AddRange(options);
            if (combo.Items.Count > 0) combo.SelectedIndex = 0;
            Cell(label, combo, 155);
            return combo;
        }
        protected ComboBox Reference(string label, DataTable values, string display, string key, int width = 190)
        {
            var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList,
                DisplayMember = display, ValueMember = key, DataSource = values };
            Cell(label, combo, width);
            return combo;
        }
        protected static int SelectedId(ComboBox combo)
        {
            if (combo.SelectedValue == null || combo.SelectedValue is DataRowView) return 0;
            return Convert.ToInt32(combo.SelectedValue);
        }
        protected Button ActionButton(string caption, Action task)
        {
            var b = new Button { Text = caption, AutoSize = true, Height = 35,
                BackColor = Color.FromArgb(225, 237, 252), FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5, 22, 4, 5) };
            b.FlatAppearance.BorderColor = Color.FromArgb(170, 198, 233);
            b.Click += (sender, e) => Run(task);
            Toolbar.Controls.Add(b);
            return b;
        }
        protected void Run(Action task)
        {
            try { task(); }
            catch (Exception ex)
            {
                var message = ex.Message;
                Status.Text = "Lỗi: " + message;
                MessageBox.Show(message, "Không thực hiện được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        protected void ShowTable(DataTable table)
        {
            Grid.DataSource = table;
            Status.Text = "Hiển thị " + table.Rows.Count + " dòng.";
        }
        protected void Success(string message)
        {
            Status.Text = message;
            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        protected static int RowId(DataGridView grid, string column)
        {
            if (grid.CurrentRow == null || !grid.Columns.Contains(column)) return 0;
            var v = grid.CurrentRow.Cells[column].Value;
            return v == null || v == DBNull.Value ? 0 : Convert.ToInt32(v);
        }
    }
}
