using System;

namespace QuanLyHoSoBenhAn.Models
{
    // DTOs cross the UI/Service boundary. Foreign keys and validations are held in SQL.
    public class DangKyInput
    {
        public string HoTen, DiaChi, SoDienThoai, Anh3x4;
        public DateTime NgaySinh;
        public decimal LePhiNam;
    }
    public class DangKyResult
    {
        public int MaBenhNhan, MaHoSo;
    }
    public class KhamInput
    {
        public int MaHoSo, MaBacSi, MaKhoa, MaLichHen;
        public string LoaiKham;
    }
    public class CapNhatKhamInput
    {
        public int MaPhieuKham;
        public string TrieuChung, HuyetAp, ChanDoan, KetLuan;
        public decimal? ThanNhiet;
    }
    public class ThuocKeInput
    {
        public int MaToaThuoc, MaThuoc, SoLuong, SoLanDungTrongNgay, SoNgayDung;
        public string LieuDung, CachDung;
    }
}
