using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Forms
{
    public sealed class FrmMain : Form
    {
        public FrmMain()
        {
            Text="VAN HOA VIET | Quan ly cong ty du lich"; Width=930;Height=650;StartPosition=FormStartPosition.CenterScreen;
            BackColor=Color.FromArgb(248,250,253); Font=new Font("Segoe UI",11);
            var banner=new Label {Dock=DockStyle.Top,Height=105,Text="CÔNG TY DU LỊCH VĂN HÓA VIỆT\nHệ thống quản lý tour - vé - đoàn - nhân sự",TextAlign=ContentAlignment.MiddleCenter,
                Font=new Font("Segoe UI",16,FontStyle.Bold),ForeColor=Color.White,BackColor=Color.FromArgb(25,66,105)};
            Controls.Add(banner);
            var layout=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=2,RowCount=4,Padding=new Padding(36),BackColor=BackColor};
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
            for(int i=0;i<4;i++) layout.RowStyles.Add(new RowStyle(SizeType.Percent,25));
            Add(layout,0,0,"1. Danh mục cơ bản",()=>new FrmDanhMuc());
            Add(layout,1,0,"2. Tuyến và điểm tham quan",()=>new FrmTuyenDiem());
            Add(layout,0,1,"3. Chuyến & vé khách lẻ",()=>new FrmChuyenLe());
            Add(layout,1,1,"4. Đăng ký đoàn",()=>new FrmPhieuDoan());
            Add(layout,0,2,"5. Phân công hướng dẫn viên",()=>new FrmPhanCong());
            Add(layout,1,2,"6. Khảo sát sau tour",()=>new FrmKhaoSat());
            Add(layout,0,3,"7. Báo cáo và tính lương",()=>new FrmBaoCao());
            Add(layout,1,3,"8. Kiểm tra kết nối SQL",()=>{Ui.Done("Đã kết nối SQL Server: "+new ReportService().DatabaseName());return null;});
            Controls.Add(layout);layout.BringToFront();banner.BringToFront();
        }
        private static void Add(TableLayoutPanel panel,int column,int row,string name,Func<Form> open)
        {
            var button=new Button {Text=name,Dock=DockStyle.Fill,Margin=new Padding(12),BackColor=Color.White,FlatStyle=FlatStyle.Flat,Font=new Font("Segoe UI",12,FontStyle.Bold)};
            button.FlatAppearance.BorderColor=Color.FromArgb(181,196,216);
            button.Click+=(s,e)=>Ui.Run(()=>{var form=open();if(form!=null)form.ShowDialog();});
            panel.Controls.Add(button,column,row);
        }
    }
}
