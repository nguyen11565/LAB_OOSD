using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public static class Ui
    {
        public static Form Page(string title)
        { return new Form { Text = title, Width = 1100, Height = 720, StartPosition = FormStartPosition.CenterScreen, Font = new Font("Segoe UI",10), BackColor=Color.White }; }
        public static TableLayoutPanel Editor(int rows)
        {
            return new TableLayoutPanel { Dock=DockStyle.Left,Width=480,AutoScroll=true,ColumnCount=2,RowCount=rows,Padding=new Padding(12),BackColor=Color.FromArgb(246,248,251) };
        }
        public static void Add(TableLayoutPanel panel, string caption, Control input)
        {
            int row=panel.Controls.Count/2;
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute,48));
            panel.ColumnStyles.Clear();panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,165));panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
            var label=new Label {Text=caption, AutoSize=true,Anchor=AnchorStyles.Left,Margin=new Padding(2,11,2,2)};
            input.Dock=DockStyle.Fill;input.Margin=new Padding(2,4,4,6);
            panel.Controls.Add(label,0,row);panel.Controls.Add(input,1,row);
        }
        public static TextBox Text(string value="") { return new TextBox {Text=value}; }
        public static NumericUpDown Number(decimal max=1000000000, int decimalPlaces=0)
        { return new NumericUpDown {Maximum=max,Minimum=0,DecimalPlaces=decimalPlaces,ThousandsSeparator=true}; }
        public static DateTimePicker Date() {return new DateTimePicker {Format=DateTimePickerFormat.Short,Value=DateTime.Today};}
        public static ComboBox Combo(LookupType type, string key, string display)
        {
            var c=new ComboBox {DropDownStyle=ComboBoxStyle.DropDownList};
            ReloadCombo(c,type,key,display);return c;
        }
        public static void ReloadCombo(ComboBox c,LookupType type,string key,string display)
        {
            var tbl=new LookupService().List(type);
            c.DisplayMember=display;c.ValueMember=key;c.DataSource=tbl;
        }
        public static string Selected(ComboBox combo)
        {
            if(combo.SelectedValue==null||combo.SelectedValue is DataRowView)throw new ArgumentException("Vui long chon muc hop le.");
            return Convert.ToString(combo.SelectedValue);
        }
        public static DataGridView Grid()
        {
            return new DataGridView {Dock=DockStyle.Fill,ReadOnly=true,AllowUserToAddRows=false,AllowUserToDeleteRows=false,
                SelectionMode=DataGridViewSelectionMode.FullRowSelect,MultiSelect=false,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.DisplayedCells,
                BackgroundColor=Color.White,RowHeadersVisible=false};
        }
        public static Button Button(string title, Action action)
        {
            var button=new Button {Text=title,AutoSize=true,Height=38,Margin=new Padding(5),FlatStyle=FlatStyle.System};
            button.Click+=(s,e)=>Run(action);return button;
        }
        public static FlowLayoutPanel Buttons(params Control[] buttons)
        { var bar=new FlowLayoutPanel {Dock=DockStyle.Bottom,Height=60,Padding=new Padding(12)};bar.Controls.AddRange(buttons);return bar; }
        public static void Run(Action action)
        {
            try {action();}
            catch(Exception ex){MessageBox.Show(ex.Message,"Khong thuc hien duoc",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
        }
        public static void Done(string text) { MessageBox.Show(text,"Thanh cong",MessageBoxButtons.OK,MessageBoxIcon.Information); }
        public static string Cell(DataGridView grid,string field)
        {return grid.CurrentRow==null?"":Convert.ToString(grid.CurrentRow.Cells[field].Value);}
    }
}
