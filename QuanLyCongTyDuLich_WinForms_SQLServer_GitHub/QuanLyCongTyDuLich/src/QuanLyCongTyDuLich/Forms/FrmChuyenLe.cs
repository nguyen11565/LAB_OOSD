using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Data;
using QuanLyCongTyDuLich.Models;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public sealed class FrmChuyenLe : Form
    {
        private readonly BookingService service=new BookingService();
        private readonly ReportService reports=new ReportService();
        public FrmChuyenLe()
        {
            Text="Khách lẻ | Lập chuyến, mua vé và hủy vé";Width=1150;Height=720;StartPosition=FormStartPosition.CenterScreen;
            var tabs=new TabControl {Dock=DockStyle.Fill};tabs.TabPages.Add(CreateTrip());tabs.TabPages.Add(SellTicket());Controls.Add(tabs);
        }
        private TabPage CreateTrip()
        {
            var tab=new TabPage("Lập chuyến khách lẻ");var panel=Ui.Editor(7);var grid=Ui.Grid();
            var id=Ui.Text("CH003");var tour=Ui.Combo(LookupType.Tours,"MaTour","TenTour");
            var start=Ui.Date();var capacity=Ui.Number(10000);capacity.Value=30;
            Ui.Add(panel,"Mã chuyến",id);Ui.Add(panel,"Tour",tour);Ui.Add(panel,"Ngày đi",start);Ui.Add(panel,"Sức chứa",capacity);
            Action refresh=()=>grid.DataSource=reports.Trips();
            var buttons=Ui.Buttons(
                Ui.Button("Tạo chuyến",()=>{service.CreateTrip(id.Text,Ui.Selected(tour),start.Value,(int)capacity.Value);refresh();Ui.Done("Đã tạo chuyến; ngày về tự động theo số ngày tour.");}),
                Ui.Button("Hoàn thành chuyến",()=>{service.CompleteTrip(Ui.Cell(grid,"MaChuyen"));refresh();Ui.Done("Đã xác nhận chuyến hoàn thành.");}),
                Ui.Button("Tải lại",()=>{Ui.ReloadCombo(tour,LookupType.Tours,"MaTour","TenTour");refresh();}));
            tab.Controls.Add(grid);tab.Controls.Add(panel);tab.Controls.Add(buttons);Ui.Run(refresh);return tab;
        }
        private TabPage SellTicket()
        {
            var tab=new TabPage("Bán vé / hủy vé");var panel=Ui.Editor(9);var grid=Ui.Grid();
            var id=Ui.Text("VE003");
            var trip=Ui.Combo(LookupType.OpenTrips,"MaChuyen","Ten");
            var customer=Ui.Combo(LookupType.Customers,"MaKhachHang","HoTen");
            var office=Ui.Combo(LookupType.Offices,"MaDiemBan","TenDiemBan");
            var people=Ui.Number(11);people.Value=1;people.Minimum=1;var pickup=Ui.Text("Ben Thanh");
            Ui.Add(panel,"Mã vé",id);Ui.Add(panel,"Chuyến",trip);Ui.Add(panel,"Khách hàng",customer);
            Ui.Add(panel,"Điểm bán",office);Ui.Add(panel,"Số người (1-11)",people);Ui.Add(panel,"Điểm đón",pickup);
            Action refresh=()=>grid.DataSource=reports.Tickets();
            var buttons=Ui.Buttons(
                Ui.Button("Bán vé (đã thu tiền)",()=>{
                    decimal total=service.SellTicket(new LeRequest{MaVe=id.Text,MaChuyen=Ui.Selected(trip),MaKhachHang=Ui.Selected(customer),
                        MaDiemBan=Ui.Selected(office),SoNguoi=(int)people.Value,DiemDon=pickup.Text});
                    refresh();Ui.Done("Đã xuất vé và thu "+total.ToString("N0")+" VND.");}),
                Ui.Button("Hủy vé đang chọn",()=>{service.CancelTicket(Ui.Cell(grid,"MaVe"));refresh();Ui.Done("Đã hủy vé, trả lại số ghế. Không tự hoàn tiền.");}),
                Ui.Button("Tải lại",()=>{Ui.ReloadCombo(trip,LookupType.OpenTrips,"MaChuyen","Ten");
                    Ui.ReloadCombo(customer,LookupType.Customers,"MaKhachHang","HoTen");
                    Ui.ReloadCombo(office,LookupType.Offices,"MaDiemBan","TenDiemBan");refresh();}));
            tab.Controls.Add(grid);tab.Controls.Add(panel);tab.Controls.Add(buttons);Ui.Run(refresh);return tab;
        }
    }
}
