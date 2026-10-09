using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public sealed class FrmTuyenDiem : Form
    {
        private readonly ItineraryService service=new ItineraryService();
        public FrmTuyenDiem()
        {
            Text="Thiết kế hành trình | Điểm tham quan và nơi dừng chân";Width=1000;Height=650;StartPosition=FormStartPosition.CenterScreen;
            var tabs=new TabControl{Dock=DockStyle.Fill};
            tabs.TabPages.Add(BuildVisit());tabs.TabPages.Add(BuildStop());Controls.Add(tabs);
        }
        private TabPage BuildVisit()
        {
            var tab=new TabPage("Điểm tham quan của tour");
            var editor=Ui.Editor(6);
            var tour=Ui.Combo(LookupType.Tours,"MaTour","TenTour");
            var point=Ui.Combo(LookupType.Attractions,"MaDiemThamQuan","TenDiemThamQuan");
            var order=Ui.Number(10000);order.Value=1;
            Ui.Add(editor,"Tour",tour);Ui.Add(editor,"Điểm tham quan",point);Ui.Add(editor,"Thứ tự",order);
            var grid=Ui.Grid();
            Action refresh=()=>grid.DataSource=service.Attractions();
            var buttons=Ui.Buttons(
                Ui.Button("Thêm",()=>{service.AddAttraction(Ui.Selected(tour),Ui.Selected(point),(int)order.Value);refresh();}),
                Ui.Button("Xóa dòng chọn",()=>{if(grid.CurrentRow==null)return;service.RemoveAttraction(Ui.Cell(grid,"MaTour"),Ui.Cell(grid,"MaDiemThamQuan"));refresh();}),
                Ui.Button("Làm mới",()=>{Ui.ReloadCombo(tour,LookupType.Tours,"MaTour","TenTour");Ui.ReloadCombo(point,LookupType.Attractions,"MaDiemThamQuan","TenDiemThamQuan");refresh();}));
            tab.Controls.Add(grid);tab.Controls.Add(editor);tab.Controls.Add(buttons);Ui.Run(refresh);return tab;
        }
        private TabPage BuildStop()
        {
            var tab=new TabPage("Nơi dừng và phương tiện");var editor=Ui.Editor(7);
            var tour=Ui.Combo(LookupType.Tours,"MaTour","TenTour");
            var stop=Ui.Combo(LookupType.Stops,"MaNoiDungChan","TenNoiDungChan");
            var vehicle=Ui.Combo(LookupType.Vehicles,"MaPhuongTien","TenPhuongTien");
            var order=Ui.Number(10000);order.Value=1;
            Ui.Add(editor,"Tour",tour);Ui.Add(editor,"Thứ tự",order);Ui.Add(editor,"Nơi dừng",stop);Ui.Add(editor,"Phương tiện",vehicle);
            var grid=Ui.Grid();
            Action refresh=()=>grid.DataSource=service.Stops();
            var buttons=Ui.Buttons(
                Ui.Button("Thêm",()=>{service.AddStop(Ui.Selected(tour),(int)order.Value,Ui.Selected(stop),Ui.Selected(vehicle));refresh();}),
                Ui.Button("Xóa dòng chọn",()=>{if(grid.CurrentRow==null)return;service.RemoveStop(Ui.Cell(grid,"MaTour"),Convert.ToInt32(Ui.Cell(grid,"ThuTu")));refresh();}),
                Ui.Button("Làm mới",()=>{Ui.ReloadCombo(tour,LookupType.Tours,"MaTour","TenTour");Ui.ReloadCombo(stop,LookupType.Stops,"MaNoiDungChan","TenNoiDungChan");Ui.ReloadCombo(vehicle,LookupType.Vehicles,"MaPhuongTien","TenPhuongTien");refresh();}));
            tab.Controls.Add(grid);tab.Controls.Add(editor);tab.Controls.Add(buttons);Ui.Run(refresh);return tab;
        }
    }
}
