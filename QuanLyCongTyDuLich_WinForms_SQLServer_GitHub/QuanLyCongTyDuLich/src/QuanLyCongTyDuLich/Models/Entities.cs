using System;
namespace QuanLyCongTyDuLich.Models
{
    // 12 main classes follow the source UML. Their IDs match SQL primary keys.
    // DECIMAL is used instead of UML Double for precise money calculations.
    public sealed class TourDuLich
    { public string MaTour {get;set;} public string TenTour {get;set;} public int SoNgay {get;set;} public int SoDem {get;set;} public decimal DonGia {get;set;} }
    public sealed class DiemThamQuan
    { public string MaDiemThamQuan {get;set;} public string TenDiemThamQuan {get;set;} public string DiaDiem {get;set;} public string NoiDungYNghia {get;set;} }
    public sealed class NoiDungChan
    { public string MaNoiDungChan {get;set;} public string TenNoiDungChan {get;set;} public bool DoiPhuongTien {get;set;} public bool CoNoiAn {get;set;} public bool CoKhachSan {get;set;} public int? LoaiKhachSan {get;set;} }
    public sealed class PhuongTien
    { public string MaPhuongTien {get;set;} public string TenPhuongTien {get;set;} public string LoaiPhuongTien {get;set;} }
    public sealed class ChuyenKhachLe
    { public string MaChuyen {get;set;} public string MaTour {get;set;} public DateTime NgayDi {get;set;} public DateTime NgayVe {get;set;} public int SoLuongHienTai {get;set;} public int SucChua {get;set;} public decimal DonGiaApDung {get;set;} public string TrangThai {get;set;} }
    public sealed class VeKhachLe
    { public string MaVe {get;set;} public string MaChuyen {get;set;} public string MaKhachHang {get;set;} public string MaDiemBan {get;set;} public DateTime NgayDangKy {get;set;} public string DiemDon {get;set;} public int SoNguoi {get;set;} public decimal TongTienThanhToan {get;set;} public string TrangThai {get;set;} }
    public sealed class DiemBanVe
    { public string MaDiemBan {get;set;} public string TenDiemBan {get;set;} public string DiaChi {get;set;} public string SoDienThoai {get;set;} }
    public sealed class KhachHang
    { public string MaKhachHang {get;set;} public string HoTen {get;set;} public string SoDienThoai {get;set;} public string DiaChi {get;set;} public string Email {get;set;} }
    public sealed class PhieuDangKyDoan
    { public string MaPhieuDoan {get;set;} public string MaTour {get;set;} public string MaKhachHang {get;set;} public string TenCoQuan {get;set;} public string DiaChiCoQuan {get;set;} public string DienThoaiCoQuan {get;set;} public string NguoiDaiDien {get;set;} public int SoNguoiDi {get;set;} public DateTime NgayDiYeuCau {get;set;} public string DiemDon {get;set;} public decimal TienCoc {get;set;} public bool CoBaoHiem {get;set;} public decimal DonGiaApDung {get;set;} public string TrangThai {get;set;} }
    public sealed class DanhSachBaoHiem
    { public string MaSoBaoHiem {get;set;} public string MaPhieuDoan {get;set;} public string HoTen {get;set;} public string CMNDPassport {get;set;} public DateTime NgaySinh {get;set;} }
    public sealed class NhanVienHD
    { public string MaNhanVien {get;set;} public string HoTen {get;set;} public string SoDienThoai {get;set;} public decimal LuongCoBan {get;set;} }
    public sealed class PhieuKhaoSat
    { public string MaKhaoSat {get;set;} public string MaKhachHang {get;set;} public string MaChuyen {get;set;} public string MaPhieuDoan {get;set;} public DateTime NgayKhaoSat {get;set;} public string NoiDungGopY {get;set;} public int DiemDanhGia {get;set;} }

    // 4 association / transaction classes added to implement relational cardinalities.
    public sealed class TourDiemThamQuan
    { public string MaTour {get;set;} public string MaDiemThamQuan {get;set;} public int ThuTu {get;set;} }
    public sealed class TourNoiDungChan
    { public string MaTour {get;set;} public int ThuTu {get;set;} public string MaNoiDungChan {get;set;} public string MaPhuongTien {get;set;} }
    public sealed class ThanhToanDoan
    { public string MaThanhToan {get;set;} public string MaPhieuDoan {get;set;} public DateTime NgayThanhToan {get;set;} public decimal SoTien {get;set;} }
    public sealed class PhanCongHuongDan
    { public string MaPhanCong {get;set;} public string MaNhanVien {get;set;} public string MaChuyen {get;set;} public string MaPhieuDoan {get;set;} public decimal TienCong {get;set;} }
}
