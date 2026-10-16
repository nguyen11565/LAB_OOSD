using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmLichSu : BaseForm
    {
        private readonly HoSoService service = new HoSoService();
        private readonly TextBox tuKhoa;
        private readonly NumericUpDown maHoSo;
        public FrmLichSu() : base("Tra cứu hồ sơ / Lịch sử điều trị",105)
        {
            tuKhoa = TextField("Mã HS / Họ tên / SĐT",230);
            maHoSo = Number("Mã hồ sơ chi tiết",0,155);
            ActionButton("Tìm bệnh nhân",() => ShowTable(service.TimKiem(tuKhoa.Text.Trim())));
            ActionButton("Lịch sử khám",() => ShowTable(service.LichSu((int)maHoSo.Value)));
            ActionButton("Kết quả xét nghiệm",() => ShowTable(service.ChiTietXetNghiem((int)maHoSo.Value)));
            ActionButton("Chi tiết toa thuốc",() => ShowTable(service.ChiTietToa((int)maHoSo.Value)));
            ActionButton("Theo dõi điều trị",() => ShowTable(service.TheoDoi((int)maHoSo.Value)));
            Grid.CellClick += (s,e) =>
            {
                if(e.RowIndex>=0 && Grid.Columns.Contains("MaHoSo"))
                   maHoSo.Value=RowId(Grid,"MaHoSo");
            };
            Run(() => ShowTable(service.TimKiem("")));
        }
    }
}
