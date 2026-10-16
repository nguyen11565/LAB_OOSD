using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmKhamBenh : BaseForm
    {
        private readonly KhamService service = new KhamService();
        private readonly NumericUpDown maHoSo, maPhieuKham, maLichHen, nhiet;
        private readonly ComboBox bacSi, khoa, loaiKham;
        private readonly TextBox trieuChung, huyetAp, chanDoan, ketLuan;
        public FrmKhamBenh() : base("Tiếp nhận / Khám bệnh", 235)
        {
            maHoSo = Number("Mã hồ sơ");
            bacSi = Reference("Bác sĩ",Catalog.BacSi(),"HoTen","MaBacSi",180);
            khoa = Reference("Khoa điều trị",Catalog.Khoa(),"TenKhoa","MaKhoa",160);
            loaiKham = Choices("Loại khám", "KHAM_BENH", "DINH_KY");
            maLichHen = Number("Mã lịch hẹn (0 = khám trực tiếp)",0,205);
            maPhieuKham = Number("Mã phiếu khám",0,125);
            trieuChung = TextField("Triệu chứng",190);
            nhiet = DecimalField("Thân nhiệt (0 = không ghi)",37.0m,170);
            huyetAp = TextField("Huyết áp",105);
            chanDoan = TextField("Chẩn đoán",185);
            ketLuan = TextField("Kết luận",185);
            ActionButton("Mở phiếu khám", () =>
            {
                var id=service.MoPhieuKham(new KhamInput { MaHoSo=(int)maHoSo.Value,
                  MaBacSi=SelectedId(bacSi),MaKhoa=SelectedId(khoa),
                  MaLichHen=(int)maLichHen.Value,LoaiKham=(string)loaiKham.SelectedItem });
                maPhieuKham.Value=id;
                RefreshList();Success("Đã mở phiếu khám #"+id+". Hãy nhập triệu chứng / chẩn đoán.");
            });
            ActionButton("Lưu nội dung khám", () =>
            {
                service.CapNhat(new CapNhatKhamInput { MaPhieuKham=(int)maPhieuKham.Value,
                  TrieuChung=trieuChung.Text.Trim(),ThanNhiet=nhiet.Value==0 ? (decimal?)null : nhiet.Value,
                  HuyetAp=huyetAp.Text.Trim(),ChanDoan=chanDoan.Text.Trim(),KetLuan=ketLuan.Text.Trim() });
                RefreshList();Success("Đã lưu thông tin phiếu khám.");
            });
            ActionButton("Hoàn tất (sau thanh toán)", () =>
            {
                service.HoanTat((int)maPhieuKham.Value);
                RefreshList();Success("Đã hoàn tất phiếu khám #"+maPhieuKham.Value);
            });
            ActionButton("Làm mới",RefreshList);
            Grid.CellClick += (s,e) => { if(e.RowIndex>=0) {maPhieuKham.Value=RowId(Grid,"MaPhieuKham");maHoSo.Value=RowId(Grid,"MaHoSo");} };
            Run(RefreshList);
        }
        private void RefreshList() { ShowTable(service.DanhSach()); }
    }
}
