using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public sealed class FrmBaoCao : Form
    {
        private readonly ReportService service=new ReportService();
        public FrmBaoCao()
        {
            Text="Báo cáo tour, vé, đoàn và tiền lương";Width=1200;Height=710;StartPosition=FormStartPosition.CenterScreen;
            var top=new FlowLayoutPanel{Dock=DockStyle.Top,Height=70,Padding=new Padding(8)};
            var selector=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList,Width=310};
            selector.Items.AddRange(new object[]{"Danh sách chuyến", "Vé khách lẻ", "Phiếu đoàn và công nợ", "Khảo sát góp ý", "Bảng lương theo tháng"});selector.SelectedIndex=0;
            var month=Ui.Number(12);month.Minimum=1;month.Value=DateTime.Today.Month;var year=Ui.Number(2100);year.Minimum=2000;year.Value=DateTime.Today.Year;
            top.Controls.Add(new Label{Text="Báo cáo",AutoSize=true});top.Controls.Add(selector);
            top.Controls.Add(new Label{Text="Tháng",AutoSize=true});top.Controls.Add(month);
            top.Controls.Add(new Label{Text="Năm",AutoSize=true});top.Controls.Add(year);
            var grid=Ui.Grid();Action refresh=()=>{
                DataTable data;
                switch(selector.SelectedIndex)
                {
                    case 0:data=service.Trips();break;
                    case 1:data=service.Tickets();break;
                    case 2:data=service.Groups();break;
                    case 3:data=service.Surveys();break;
                    default:data=service.Payroll((int)year.Value,(int)month.Value);break;
                }
                grid.DataSource=data;
            };
            top.Controls.Add(Ui.Button("Xem / Làm mới",refresh));
            selector.SelectedIndexChanged+=(s,e)=>Ui.Run(refresh);
            Controls.Add(grid);Controls.Add(top);Ui.Run(refresh);
        }
    }
}
