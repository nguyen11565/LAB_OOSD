using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Data;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public sealed class FrmPhanCong : Form
    {
        private readonly SchedulingService service=new SchedulingService();
        public FrmPhanCong()
        {
            Text="Phân công hướng dẫn viên | Kiểm tra trùng lịch";Width=1050;Height=650;StartPosition=FormStartPosition.CenterScreen;
            var panel=Ui.Editor(8);var grid=Ui.Grid();
            var employee=Ui.Combo(LookupType.Employees,"MaNhanVien","HoTen");
            var type=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList};type.Items.AddRange(new object[]{"Chuyen khach le","Doan"});type.SelectedIndex=0;
            var trip=Ui.Combo(LookupType.OpenTrips,"MaChuyen","Ten");
            var group=Ui.Combo(LookupType.OpenGroups,"MaPhieuDoan","Ten");
            var salary=Ui.Number(1000000000);salary.Value=1000000;
            Ui.Add(panel,"Hướng dẫn viên",employee);Ui.Add(panel,"Loại phân công",type);
            Ui.Add(panel,"Chuyến",trip);Ui.Add(panel,"Đoàn",group);Ui.Add(panel,"Tiền công / tour",salary);
            Action switchType=()=>{trip.Enabled=type.SelectedIndex==0;group.Enabled=type.SelectedIndex==1;};type.SelectedIndexChanged+=(s,e)=>switchType();switchType();
            Action refresh=()=>grid.DataSource=service.List();
            var buttons=Ui.Buttons(
                Ui.Button("Phân công",()=>{service.Assign(Ui.Selected(employee),type.SelectedIndex==0?Ui.Selected(trip):null,type.SelectedIndex==1?Ui.Selected(group):null,salary.Value);refresh();Ui.Done("Đã phân công, không trùng lịch.");}),
                Ui.Button("Tải lại",()=>{Ui.ReloadCombo(employee,LookupType.Employees,"MaNhanVien","HoTen");
                    Ui.ReloadCombo(trip,LookupType.OpenTrips,"MaChuyen","Ten");
                    Ui.ReloadCombo(group,LookupType.OpenGroups,"MaPhieuDoan","Ten");refresh();}));
            Controls.Add(grid);Controls.Add(panel);Controls.Add(buttons);Ui.Run(refresh);
        }
    }
}
