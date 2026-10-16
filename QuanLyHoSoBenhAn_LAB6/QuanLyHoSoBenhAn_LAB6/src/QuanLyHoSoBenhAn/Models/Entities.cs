using System;

namespace QuanLyHoSoBenhAn.Models
{
    // 16 domain entities correspond 1:1 to the 16 SQL tables and UML classes.
    // In this demo ADO.NET reads DataTable for the data grid; business methods live in Services.
    public sealed class BenhNhan
    {
        public int MaBenhNhan { get; set; }  // PK
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Anh3x4 { get; set; }
        public DateTime NgayTao { get; set; }
    }
    public sealed class PhieuDangKy
    {
        public int MaPhieuDangKy { get; set; } // PK
        public int MaBenhNhan { get; set; } // FK UNIQUE
        public DateTime NgayDangKy { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Anh3x4 { get; set; }
        public string TrangThai { get; set; }
    }
    public sealed class PhieuThuLePhi
    {
        public int MaPhieuThu { get; set; } // PK
        public int MaBenhNhan { get; set; } // FK
        public DateTime NgayThu { get; set; }
        public decimal SoTien { get; set; }
        public DateTime NgayBatDauHieuLuc { get; set; }
        public DateTime NgayHetHan { get; set; }
    }
    public sealed class HoSoBenhAn
    {
        public int MaHoSo { get; set; } // PK
        public int MaBenhNhan { get; set; } // FK UNIQUE
        public int MaPhieuDangKy { get; set; } // FK UNIQUE
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
    }
    public sealed class PhieuKhamBenh
    {
        public int MaPhieuKham { get; set; } // PK
        public int MaHoSo { get; set; } // FK
        public int MaBacSi { get; set; } // FK
        public int MaKhoa { get; set; } // FK
        public int? MaLichHen { get; set; } // FK, optional, unique when not null
        public DateTime NgayGioKham { get; set; }
        public string LoaiKham { get; set; }
        public string TrieuChung { get; set; }
        public decimal? ThanNhiet { get; set; }
        public string HuyetAp { get; set; }
        public string ChanDoan { get; set; }
        public string KetLuan { get; set; }
        public string TrangThai { get; set; }
        public DateTime? NgayHoanTat { get; set; }
    }
    public sealed class BacSi
    {
        public int MaBacSi { get; set; }
        public int MaKhoa { get; set; }
        public string HoTen { get; set; }
        public string ChuyenMon { get; set; }
        public string SoDienThoai { get; set; }
    }
    public sealed class KhoaDieuTri
    {
        public int MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string MoTa { get; set; }
    }
    public sealed class ToaThuoc
    {
        public int MaToaThuoc { get; set; }
        public int MaPhieuKham { get; set; }
        public DateTime NgayLap { get; set; }
        public string LoiDan { get; set; }
    }
    public sealed class ChiTietToaThuoc
    {
        public int MaChiTietToa { get; set; }
        public int MaToaThuoc { get; set; }
        public int MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public string LieuDung { get; set; }
        public int SoLanDungTrongNgay { get; set; }
        public string CachDung { get; set; }
        public int SoNgayDung { get; set; }
    }
    public sealed class Thuoc
    {
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string HamLuong { get; set; }
        public string DonViTinh { get; set; }
        public string MoTa { get; set; }
    }
    public sealed class LoaiXetNghiem
    {
        public int MaLoaiXetNghiem { get; set; }
        public string TenXetNghiem { get; set; }
        public string MoTa { get; set; }
        public decimal DonGia { get; set; }
    }
    public sealed class PhieuXetNghiem
    {
        public int MaPhieuXetNghiem { get; set; }
        public int MaPhieuKham { get; set; }
        public int MaLoaiXetNghiem { get; set; }
        public DateTime NgayChiDinh { get; set; }
        public DateTime? NgayThucHien { get; set; }
        public string KetQua { get; set; }
        public string KetLuan { get; set; }
        public decimal DonGiaApDung { get; set; }
        public string TrangThai { get; set; }
    }
    public sealed class LichHen
    {
        public int MaLichHen { get; set; }
        public int MaHoSo { get; set; }
        public int MaBacSi { get; set; }
        public int? MaNhanVien { get; set; }
        public DateTime NgayLap { get; set; }
        public DateTime NgayGioHen { get; set; }
        public string LoaiHen { get; set; }
        public string NoiDungHen { get; set; }
        public string TrangThai { get; set; }
    }
    public sealed class NhanVienCSKH
    {
        public int MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
    }
    public sealed class HoaDonVienPhi
    {
        public int MaHoaDon { get; set; }
        public int MaPhieuKham { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TienKham { get; set; }
        public decimal TienXetNghiem { get; set; }
        public decimal TongTien { get; set; }
        public decimal? SoTienNhan { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public string TrangThaiThanhToan { get; set; }
        public string GhiChu { get; set; }
    }
    public sealed class TheoDoiDieuTri
    {
        public int MaTheoDoi { get; set; }
        public int MaPhieuKham { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayGhiNhan { get; set; }
        public string DienBienBenh { get; set; }
        public string PhanUngPhu { get; set; }
        public string HieuQuaDieuTri { get; set; }
        public string GhiChu { get; set; }
    }
}
