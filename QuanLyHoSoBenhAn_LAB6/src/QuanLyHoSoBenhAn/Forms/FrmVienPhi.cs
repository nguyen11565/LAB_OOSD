using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmVienPhi : BaseForm
    {
        private readonly VienPhiService service = new VienPhiService();
        private readonly KhamService kham = new KhamService();
        private readonly NumericUpDown maPhieuKham, maHoaDon, soTienNhan;
        private readonly ComboBox nhanVien;
        private readonly TextBox dienBien, phanUng, hieuQua;
        public FrmVienPhi() : base("Viện phí / Hoàn tất / Chăm sóc sau khám",185)
        {
            maPhieuKham = Number("Mã phiếu khám");
            maHoaDon = Number("Mã hóa đơn");
            soTienNhan = Number("Số tiền nhận (VNĐ)",0,170);
            nhanVien = Reference("Nhân viên CSKH",Catalog.NhanVien(),"HoTen","MaNhanVien");
            dienBien = TextField("Diễn biến bệnh",170);
            phanUng = TextField("Phản ứng phụ",170);
            hieuQua = TextField("Hiệu quả điều trị",170);
            ActionButton("Lập hóa đơn",() =>
            {
                int id=service.LapHoaDon((int)maPhieuKham.Value);
                maHoaDon.Value=id; RefreshList();
                Success("Đã lập hóa đơn #"+id+". Số tiền xem ở bảng (khám định kỳ = 0đ tiền khám).");
            });
            ActionButton("Xác nhận thanh toán",() =>
            {
                service.ThanhToan((int)maHoaDon.Value,soTienNhan.Value);
                RefreshList();Success("Đã thanh toán hóa đơn #"+maHoaDon.Value);
            });
            ActionButton("Hoàn tất phiếu khám",() =>
            {
                kham.HoanTat((int)maPhieuKham.Value);
                RefreshList();Success("Đã hoàn tất phiếu khám #"+maPhieuKham.Value);
            });
            ActionButton("Ghi theo dõi sau khám",() =>
            {
                service.GhiTheoDoi((int)maPhieuKham.Value,SelectedId(nhanVien),
                  dienBien.Text.Trim(),phanUng.Text.Trim(),hieuQua.Text.Trim(),"");
                Success("Đã ghi nhận diễn biến và phản ứng phụ sau điều trị.");
            });
            ActionButton("Làm mới",RefreshList);
            Grid.CellClick += (s,e) =>
            {
                if(e.RowIndex>=0)
                {
                    maHoaDon.Value=RowId(Grid,"MaHoaDon");
                    maPhieuKham.Value=RowId(Grid,"MaPhieuKham");
                    if(Grid.CurrentRow!=null && Grid.CurrentRow.Cells["TongTien"].Value!=DBNull.Value)
                        soTienNhan.Value=Convert.ToDecimal(Grid.CurrentRow.Cells["TongTien"].Value);
                }
            };
            Run(RefreshList);
        }
        private void RefreshList() { ShowTable(service.DanhSach()); }
    }
}
