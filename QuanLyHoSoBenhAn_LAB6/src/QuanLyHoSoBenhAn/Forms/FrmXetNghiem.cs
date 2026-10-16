using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmXetNghiem : BaseForm
    {
        private readonly XetNghiemService service = new XetNghiemService();
        private readonly NumericUpDown maPhieuKham, maPhieuXN;
        private readonly ComboBox loaiXN;
        private readonly TextBox ketQua, ketLuan;
        public FrmXetNghiem() : base("Chỉ định / Kết quả xét nghiệm",140)
        {
            maPhieuKham = Number("Mã phiếu khám");
            loaiXN = Reference("Loại xét nghiệm",Catalog.LoaiXN(),"TenXetNghiem","MaLoaiXetNghiem",205);
            maPhieuXN = Number("Mã phiếu xét nghiệm",0,150);
            ketQua = TextField("Kết quả",200);
            ketLuan = TextField("Kết luận xét nghiệm",190);
            ActionButton("Chỉ định xét nghiệm",() =>
            {
                var id=service.ChiDinh((int)maPhieuKham.Value,SelectedId(loaiXN));
                maPhieuXN.Value=id; RefreshList();Success("Đã chỉ định phiếu xét nghiệm #"+id);
            });
            ActionButton("Ghi kết quả",() =>
            {
                service.GhiKetQua((int)maPhieuXN.Value,ketQua.Text.Trim(),ketLuan.Text.Trim());
                RefreshList();Success("Đã ghi nhận kết quả xét nghiệm.");
            });
            ActionButton("Làm mới",RefreshList);
            Grid.CellClick += (s,e) => { if(e.RowIndex>=0) {maPhieuXN.Value=RowId(Grid,"MaPhieuXetNghiem");maPhieuKham.Value=RowId(Grid,"MaPhieuKham");} };
            Run(RefreshList);
        }
        private void RefreshList() { ShowTable(service.DanhSach()); }
    }
}
