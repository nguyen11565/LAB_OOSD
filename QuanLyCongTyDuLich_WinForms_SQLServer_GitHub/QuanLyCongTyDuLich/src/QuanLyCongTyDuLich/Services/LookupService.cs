using System;
using System.Data;
using QuanLyCongTyDuLich.Data;
namespace QuanLyCongTyDuLich.Services
{
    public enum LookupType { Tours, Attractions, Stops, Vehicles, Customers, Offices, Employees, OpenTrips, OpenGroups, ClosedTrips, ClosedGroups }
    public sealed class LookupService
    {
        public DataTable List(LookupType type)
        {
            switch(type)
            {
                case LookupType.Tours: return Db.Query("SELECT MaTour,TenTour FROM dbo.TourDuLich ORDER BY MaTour");
                case LookupType.Attractions: return Db.Query("SELECT MaDiemThamQuan,TenDiemThamQuan FROM dbo.DiemThamQuan ORDER BY MaDiemThamQuan");
                case LookupType.Stops: return Db.Query("SELECT MaNoiDungChan,TenNoiDungChan FROM dbo.NoiDungChan ORDER BY MaNoiDungChan");
                case LookupType.Vehicles: return Db.Query("SELECT MaPhuongTien,TenPhuongTien FROM dbo.PhuongTien ORDER BY MaPhuongTien");
                case LookupType.Customers: return Db.Query("SELECT MaKhachHang,HoTen FROM dbo.KhachHang ORDER BY HoTen");
                case LookupType.Offices: return Db.Query("SELECT MaDiemBan,TenDiemBan FROM dbo.DiemBanVe ORDER BY MaDiemBan");
                case LookupType.Employees: return Db.Query("SELECT MaNhanVien,HoTen FROM dbo.NhanVienHD ORDER BY HoTen");
                case LookupType.OpenTrips: return Db.Query("SELECT MaChuyen,MaChuyen+' - '+MaTour AS Ten FROM dbo.ChuyenKhachLe WHERE TrangThai=N'DangMo' ORDER BY NgayDi");
                case LookupType.OpenGroups: return Db.Query("SELECT MaPhieuDoan,MaPhieuDoan+' - '+TenCoQuan AS Ten FROM dbo.PhieuDangKyDoan WHERE TrangThai=N'DaDatCoc' ORDER BY NgayDiYeuCau");
                case LookupType.ClosedTrips: return Db.Query("SELECT MaChuyen,MaChuyen AS Ten FROM dbo.ChuyenKhachLe WHERE TrangThai=N'HoanThanh' ORDER BY MaChuyen");
                case LookupType.ClosedGroups: return Db.Query("SELECT MaPhieuDoan,MaPhieuDoan AS Ten FROM dbo.PhieuDangKyDoan WHERE TrangThai IN(N'DaHoanThanh',N'DaThanhToan') ORDER BY MaPhieuDoan");
                default: throw new ArgumentOutOfRangeException("type");
            }
        }
    }
}
