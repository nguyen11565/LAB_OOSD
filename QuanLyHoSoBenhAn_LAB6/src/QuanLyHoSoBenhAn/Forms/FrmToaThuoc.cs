using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmToaThuoc : BaseForm
    {
        private readonly ToaThuocService service = new ToaThuocService();
        private readonly NumericUpDown maPhieuKham, maToa, soLuong, soLan, soNgay;
        private readonly ComboBox thuoc;
        private readonly TextBox loiDan, lieuDung, cachDung;
        public FrmToaThuoc() : base("Lập toa thuốc / Chi tiết toa",175)
        {
            maPhieuKham = Number("Mã phiếu khám");
            maToa = Number("Mã toa thuốc");
            loiDan = TextField("Lời dặn",205);
            thuoc = Reference("Thuốc",Catalog.Thuoc(),"TenThuoc","MaThuoc",195);
            soLuong = Number("Số lượng",10,100);
            lieuDung = TextField("Liều dùng",115,"1 viên");
            soLan = Number("Số lần/ngày",2,100);
            cachDung = TextField("Cách dùng",150,"Sau ăn");
            soNgay = Number("Số ngày dùng",5,100);
            ActionButton("Tạo toa thuốc",() =>
            {
                var id=service.TaoToa((int)maPhieuKham.Value,loiDan.Text.Trim());
                maToa.Value=id; RefreshList();Success("Đã lập toa #"+id);
            });
            ActionButton("Thêm thuốc vào toa",() =>
            {
                service.ThemThuoc(new ThuocKeInput {MaToaThuoc=(int)maToa.Value,MaThuoc=SelectedId(thuoc),
                    SoLuong=(int)soLuong.Value,LieuDung=lieuDung.Text.Trim(),
                    SoLanDungTrongNgay=(int)soLan.Value,CachDung=cachDung.Text.Trim(),SoNgayDung=(int)soNgay.Value});
                RefreshList();Success("Đã thêm thuốc vào toa #"+maToa.Value);
            });
            ActionButton("Xem toa",RefreshList);
            ActionButton("Xem trước / In toa",PrintPrescription);
            Grid.CellClick += (s,e) => { if(e.RowIndex>=0) {maToa.Value=RowId(Grid,"MaToaThuoc");maPhieuKham.Value=RowId(Grid,"MaPhieuKham");} };
            Run(RefreshList);
        }
        private void RefreshList() { ShowTable(service.DanhSach((int)maToa.Value)); }
        private void PrintPrescription()
        {
            if (maToa.Value == 0) throw new InvalidOperationException("Nhập mã toa trước khi in.");
            DataTable data = service.DanhSach((int)maToa.Value);
            if (data.Rows.Count == 0) throw new InvalidOperationException("Không có toa thuốc này.");
            var document = new PrintDocument();
            document.DocumentName = "Toa thuoc " + maToa.Value;
            document.PrintPage += (sender, e) =>
            {
                float y=80;
                using (var heading = new Font("Arial", 17, FontStyle.Bold))
                using (var body = new Font("Arial", 11))
                {
                    e.Graphics.DrawString("TOA THUOC - MA #"+maToa.Value,heading,Brushes.Black,75,y); y+=45;
                    e.Graphics.DrawString("Phieu kham: #"+data.Rows[0]["MaPhieuKham"] +" - Ngay lap: "+data.Rows[0]["NgayLap"],body,Brushes.Black,75,y); y+=35;
                    foreach (DataRow row in data.Rows)
                    {
                        if (row["TenThuoc"]==DBNull.Value) continue;
                        string line=row["TenThuoc"]+" "+row["HamLuong"]+" | SL: "+row["SoLuong"];
                        string instruction="Lieu: "+row["LieuDung"]+" | "+row["SoLanDungTrongNgay"]+" lan/ngay | "+row["SoNgayDung"]+" ngay | "+row["CachDung"];
                        e.Graphics.DrawString(line,body,Brushes.Black,75,y);y+=26;
                        e.Graphics.DrawString(instruction,body,Brushes.Black,90,y);y+=35;
                    }
                    e.Graphics.DrawString("Loi dan: "+data.Rows[0]["LoiDan"],body,Brushes.Black,new RectangleF(75,y,650,95));
                }
                e.HasMorePages=false;
            };
            using (var preview = new PrintPreviewDialog { Document=document, Width=960, Height=680 }) preview.ShowDialog(this);
            document.Dispose();
        }
    }
}
