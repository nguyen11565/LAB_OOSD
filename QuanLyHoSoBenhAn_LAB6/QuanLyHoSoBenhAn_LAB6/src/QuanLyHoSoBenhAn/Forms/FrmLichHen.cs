using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmLichHen : BaseForm
    {
        private readonly LichHenService service = new LichHenService();
        private readonly NumericUpDown maHoSo, maLichHen;
        private readonly ComboBox bacSi, nhanVien, loaiHen;
        private readonly DateTimePicker ngay;
        private readonly TextBox noiDung;
        public FrmLichHen() : base("Đặt lịch khám / lịch định kỳ quý", 145)
        {
            maHoSo = Number("Mã hồ sơ");
            bacSi = Reference("Bác sĩ", Catalog.BacSi(), "HoTen", "MaBacSi");
            nhanVien = Reference("Nhân viên CSKH", Catalog.NhanVien(), "HoTen", "MaNhanVien");
            ngay = DateField("Ngày giờ hẹn", DateTime.Now.AddDays(1), true, 165);
            loaiHen = Choices("Loại hẹn", "KHAM_BENH", "DINH_KY");
            noiDung = TextField("Nội dung", 160);
            maLichHen = Number("Mã hẹn (để hủy)", 0, 115);
            ActionButton("Đặt lịch", () =>
            {
                var id=service.DatLich((int)maHoSo.Value,SelectedId(bacSi),SelectedId(nhanVien),
                                      ngay.Value,(string)loaiHen.SelectedItem,noiDung.Text.Trim());
                maLichHen.Value = id;
                RefreshList(); Success("Đã đặt lịch #" + id);
            });
            ActionButton("Hủy lịch", () => { service.HuyLich((int)maLichHen.Value); RefreshList(); Success("Đã hủy lịch."); });
            ActionButton("Làm mới", RefreshList);
            Grid.CellClick += (s,e) => { if(e.RowIndex>=0) {maLichHen.Value=RowId(Grid,"MaLichHen");maHoSo.Value=RowId(Grid,"MaHoSo");} };
            Run(RefreshList);
        }
        private void RefreshList() { ShowTable(service.DanhSach()); }
    }
}
