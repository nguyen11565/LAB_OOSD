using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Data;
using QuanLyCongTyDuLich.Models;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public sealed class FrmPhieuDoan : Form
    {
        private readonly BookingService service=new BookingService();
        private readonly ReportService reports=new ReportService();
        public FrmPhieuDoan()
        {
            Text="Phiếu đăng ký đoàn | Đặt cọc - bảo hiểm - tất toán";Width=1330;Height=780;StartPosition=FormStartPosition.CenterScreen;
            var grid=Ui.Grid();var panel=Ui.Editor(15);panel.Width=605;
            var id=Ui.Text("PD003");var tour=Ui.Combo(LookupType.Tours,"MaTour","TenTour");
            var customer=Ui.Combo(LookupType.Customers,"MaKhachHang","HoTen");
            var company=Ui.Text("Công ty ví dụ");var address=Ui.Text("TP.HCM");var phone=Ui.Text("0901234567");var representative=Ui.Text("Đại diện đoàn");
            var people=Ui.Number(99999);people.Minimum=12;people.Value=12;var date=Ui.Date();
            var pickup=Ui.Text("Văn phòng đoàn");var deposit=Ui.Number(1000000000);deposit.DecimalPlaces=2;deposit.Value=1000000;
            var insured=new CheckBox{Text="Mua bảo hiểm"};
            var list=new TextBox {Multiline=true,ScrollBars=ScrollBars.Vertical,Height=110};
            Ui.Add(panel,"Mã phiếu đoàn",id);Ui.Add(panel,"Tour",tour);Ui.Add(panel,"Khách đại diện",customer);
            Ui.Add(panel,"Tên cơ quan/gia đình",company);Ui.Add(panel,"Địa chỉ cơ quan",address);Ui.Add(panel,"Điện thoại",phone);
            Ui.Add(panel,"Người đại diện",representative);Ui.Add(panel,"Số người (>=12)",people);Ui.Add(panel,"Ngày đi yêu cầu",date);
            Ui.Add(panel,"Địa điểm xe đón",pickup);Ui.Add(panel,"Tiền đặt cọc",deposit);Ui.Add(panel,"Bảo hiểm?",insured);
            Ui.Add(panel,"Người bảo hiểm, mỗi dòng: Tên|CCCD|dd/MM/yyyy",list);
            // Make insurance multiline editor taller without changing the surrounding transaction rules.
            panel.RowStyles[panel.RowStyles.Count-1].Height=126;
            Action refresh=()=>grid.DataSource=reports.Groups();
            var buttons=Ui.Buttons(
                Ui.Button("Lập phiếu & đặt cọc",()=>{
                    var r=new DoanRequest {MaPhieuDoan=id.Text,MaTour=Ui.Selected(tour),MaKhachHang=Ui.Selected(customer),TenCoQuan=company.Text,
                        DiaChiCoQuan=address.Text,DienThoaiCoQuan=phone.Text,NguoiDaiDien=representative.Text,SoNguoiDi=(int)people.Value,
                        NgayDi=date.Value,DiemDon=pickup.Text,TienCoc=deposit.Value,CoBaoHiem=insured.Checked, DanhSach=ParsePeople(list.Text)};
                    decimal total=service.CreateGroup(r);refresh();Ui.Done("Đặt cọc thành công. Tổng phí: "+total.ToString("N0")+" VND, còn lại: "+(total-r.TienCoc).ToString("N0")+" VND.");}),
                Ui.Button("Hủy phiếu",()=>{service.CancelGroup(Ui.Cell(grid,"MaPhieuDoan"));refresh();Ui.Done("Đã hủy, tiền cọc không hoàn.");}),
                Ui.Button("Hoàn thành tour",()=>{service.CompleteGroup(Ui.Cell(grid,"MaPhieuDoan"));refresh();Ui.Done("Đã hoàn thành tour đoàn.");}),
                Ui.Button("Tất toán sau tour",()=>{decimal due=service.SettleGroup(Ui.Cell(grid,"MaPhieuDoan"));refresh();Ui.Done("Đã thu thêm "+due.ToString("N0")+" VND.");}),
                Ui.Button("Làm mới",()=>{Ui.ReloadCombo(tour,LookupType.Tours,"MaTour","TenTour");
                    Ui.ReloadCombo(customer,LookupType.Customers,"MaKhachHang","HoTen");refresh();}));
            Controls.Add(grid);Controls.Add(panel);Controls.Add(buttons);Ui.Run(refresh);
        }
        private static List<BaoHiemNguoi> ParsePeople(string input)
        {
            var people=new List<BaoHiemNguoi>();
            foreach(string raw in input.Split(new[]{'\r','\n'},StringSplitOptions.RemoveEmptyEntries))
            {
                string[] cols=raw.Split('|');
                if(cols.Length!=3)throw new ArgumentException("Danh sach bao hiem: moi dong can Ten|GiayTo|dd/MM/yyyy.");
                DateTime dob;
                if(!DateTime.TryParseExact(cols[2].Trim(),"dd/MM/yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out dob))
                    throw new ArgumentException("Ngay sinh phai dung dd/MM/yyyy.");
                people.Add(new BaoHiemNguoi {HoTen=cols[0].Trim(),GiayTo=cols[1].Trim(),NgaySinh=dob});
            }
            return people;
        }
    }
}
