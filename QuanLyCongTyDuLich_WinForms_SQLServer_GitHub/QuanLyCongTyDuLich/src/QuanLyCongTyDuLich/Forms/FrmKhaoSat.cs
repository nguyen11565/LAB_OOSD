using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Data;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public sealed class FrmKhaoSat : Form
    {
        private readonly SurveyService service=new SurveyService();
        private readonly ReportService reports=new ReportService();
        public FrmKhaoSat()
        {
            Text="Khảo sát - Góp ý sau tour";Width=1120;Height=650;StartPosition=FormStartPosition.CenterScreen;
            var panel=Ui.Editor(9);var grid=Ui.Grid();
            var id=Ui.Text("KS003");var customer=Ui.Combo(LookupType.Customers,"MaKhachHang","HoTen");
            var type=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList};type.Items.AddRange(new object[]{"Chuyến khách lẻ","Đoàn"});type.SelectedIndex=0;
            var trip=Ui.Combo(LookupType.ClosedTrips,"MaChuyen","Ten");
            var group=Ui.Combo(LookupType.ClosedGroups,"MaPhieuDoan","Ten");
            var score=Ui.Number(5);score.Minimum=1;score.Value=5;var content=new TextBox{Multiline=true,Height=90,ScrollBars=ScrollBars.Vertical};
            Ui.Add(panel,"Mã khảo sát",id);Ui.Add(panel,"Khách hàng",customer);Ui.Add(panel,"Loại",type);
            Ui.Add(panel,"Chuyến",trip);Ui.Add(panel,"Đoàn",group);Ui.Add(panel,"Điểm 1-5",score);Ui.Add(panel,"Góp ý",content);
            panel.RowStyles[panel.RowStyles.Count-1].Height=100;
            Action set=()=>{trip.Enabled=type.SelectedIndex==0;group.Enabled=type.SelectedIndex==1;};type.SelectedIndexChanged+=(s,e)=>set();set();
            Action refresh=()=>grid.DataSource=reports.Surveys();
            var buttons=Ui.Buttons(
                Ui.Button("Lưu khảo sát",()=>{service.Create(id.Text,Ui.Selected(customer),type.SelectedIndex==0?Ui.Selected(trip):null,
                    type.SelectedIndex==1?Ui.Selected(group):null,(int)score.Value,content.Text);refresh();Ui.Done("Đã ghi nhận góp ý sau tour.");}),
                Ui.Button("Tải lại",()=>{Ui.ReloadCombo(customer,LookupType.Customers,"MaKhachHang","HoTen");
                    Ui.ReloadCombo(trip,LookupType.ClosedTrips,"MaChuyen","Ten");
                    Ui.ReloadCombo(group,LookupType.ClosedGroups,"MaPhieuDoan","Ten");refresh();}));
            Controls.Add(grid);Controls.Add(panel);Controls.Add(buttons);Ui.Run(refresh);
        }
    }
}
