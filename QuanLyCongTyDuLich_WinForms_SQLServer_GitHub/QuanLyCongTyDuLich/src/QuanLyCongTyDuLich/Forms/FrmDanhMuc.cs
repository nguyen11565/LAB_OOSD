using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public sealed class FrmDanhMuc : Form
    {
        private readonly CatalogService service=new CatalogService();
        private readonly ComboBox entities=new ComboBox {DropDownStyle=ComboBoxStyle.DropDownList,Dock=DockStyle.Top,Height=40};
        private readonly DataGridView grid=Ui.Grid();
        private readonly TableLayoutPanel panel=Ui.Editor(10);
        private readonly Dictionary<string, Control> controls=new Dictionary<string,Control>();
        private CatalogSpec current;
        public FrmDanhMuc()
        {
            Text="Danh mục | Tour - Điểm - Khách - Nhân viên";Width=1150;Height=720;StartPosition=FormStartPosition.CenterScreen;
            var bar=Ui.Buttons(Ui.Button("Thêm mới",()=>Save(false)),Ui.Button("Cập nhật",()=>Save(true)),
                Ui.Button("Xóa bản ghi",Delete),Ui.Button("Làm mới",LoadRows),Ui.Button("Xóa ô nhập",Clear));
            Controls.Add(grid);Controls.Add(panel);Controls.Add(bar);Controls.Add(entities);
            grid.SelectionChanged+=(s,e)=>FillSelected();
            entities.DisplayMember="Title";entities.DataSource=CatalogService.Entities;
            entities.SelectedIndexChanged+=(s,e)=>Rebuild();
            Rebuild();
        }
        private void Rebuild()
        {
            current=entities.SelectedItem as CatalogSpec;if(current==null)return;
            panel.Controls.Clear();panel.RowStyles.Clear();controls.Clear();
            foreach(var field in current.Fields)
            {
                Control editor;
                if(field.Kind==typeof(bool))editor=new CheckBox();
                else editor=Ui.Text();
                controls.Add(field.Name,editor);Ui.Add(panel,field.Caption,editor);
            }
            Ui.Run(LoadRows);
        }
        private void LoadRows(){ if(current!=null) grid.DataSource=service.List(current); }
        private void FillSelected()
        {
            if(current==null||grid.CurrentRow==null||grid.DataSource==null)return;
            foreach(var field in current.Fields)
            {
                if(!grid.Columns.Contains(field.Name))continue;
                object val=grid.CurrentRow.Cells[field.Name].Value;
                Control ctrl;
                if(!controls.TryGetValue(field.Name,out ctrl))continue;
                if(ctrl is CheckBox)((CheckBox)ctrl).Checked=val!=DBNull.Value && Convert.ToBoolean(val);
                else ctrl.Text=val==DBNull.Value?"":Convert.ToString(val,CultureInfo.CurrentCulture);
            }
        }
        private void Clear()
        {
            foreach(var c in controls.Values)
                if(c is CheckBox)((CheckBox)c).Checked=false;
                else c.Text="";
            if(grid.CurrentRow!=null)grid.ClearSelection();
        }
        private void Save(bool update)
        {
            var values=new Dictionary<string,object>();
            foreach(var field in current.Fields)
            {
                Control c=controls[field.Name];
                if(field.Kind==typeof(bool))values[field.Name]=((CheckBox)c).Checked;
                else if(string.IsNullOrWhiteSpace(c.Text))values[field.Name]=field.Nullable?(object)DBNull.Value:"";
                else if(field.Kind==typeof(int))values[field.Name]=int.Parse(c.Text.Trim(),NumberStyles.Integer,CultureInfo.CurrentCulture);
                else if(field.Kind==typeof(decimal))values[field.Name]=decimal.Parse(c.Text.Trim(),NumberStyles.Number,CultureInfo.CurrentCulture);
                else values[field.Name]=c.Text.Trim();
            }
            service.Save(current,values,update);LoadRows();Ui.Done(update?"Đã cập nhật.":"Đã thêm bản ghi.");
        }
        private void Delete()
        {
            string key=controls[current.Fields[0].Name].Text;
            if(MessageBox.Show("Xóa mã "+key+"? Chỉ xóa được khi chưa được sử dụng.","Xác nhận",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;
            service.Delete(current,key);LoadRows();Ui.Done("Đã xóa.");
        }
    }
}
