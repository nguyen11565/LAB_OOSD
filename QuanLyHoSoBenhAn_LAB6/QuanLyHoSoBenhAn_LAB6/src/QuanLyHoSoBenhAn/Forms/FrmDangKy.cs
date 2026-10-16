using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmDangKy : BaseForm
    {
        private readonly DangKyService service = new DangKyService();
        private readonly TextBox ten, diaChi, phone, anh;
        private readonly DateTimePicker ngaySinh;
        private readonly NumericUpDown phi, maHoSo;
        public FrmDangKy() : base("Đăng ký lần đầu / Lệ phí năm", 155)
        {
            ten = TextField("Họ tên", 170);
            ngaySinh = DateField("Ngày sinh", new DateTime(2000, 1, 1));
            diaChi = TextField("Địa chỉ", 200);
            phone = TextField("Điện thoại", 130);
            anh = TextField("Đường dẫn ảnh 3x4", 165);
            phi = Number("Lệ phí năm (VNĐ)", 500000, 150);
            maHoSo = Number("Mã hồ sơ (gia hạn)", 0, 125);
            ActionButton("Đăng ký + thu phí + cấp hồ sơ", () =>
            {
                var result = service.DangKyLanDau(new DangKyInput { HoTen = ten.Text.Trim(),
                    DiaChi = diaChi.Text.Trim(), SoDienThoai = phone.Text.Trim(),
                    Anh3x4 = anh.Text.Trim(), NgaySinh = ngaySinh.Value.Date, LePhiNam = phi.Value });
                maHoSo.Value = result.MaHoSo;
                RefreshList();
                Success("Đã tạo bệnh nhân #" + result.MaBenhNhan + ", cấp hồ sơ #" + result.MaHoSo + " và thu lệ phí năm.");
            });
            ActionButton("Gia hạn phí 1 năm", () =>
            {
                var receipt = service.GiaHanLePhi((int)maHoSo.Value, phi.Value);
                RefreshList();
                Success("Đã thu phí gia hạn, mã phiếu thu #" + receipt);
            });
            ActionButton("Làm mới", RefreshList);
            Grid.CellClick += (s, e) => { if (e.RowIndex >= 0) maHoSo.Value = RowId(Grid, "MaHoSo"); };
            Run(RefreshList);
        }
        private void RefreshList() { ShowTable(service.DanhSach()); }
    }
}
