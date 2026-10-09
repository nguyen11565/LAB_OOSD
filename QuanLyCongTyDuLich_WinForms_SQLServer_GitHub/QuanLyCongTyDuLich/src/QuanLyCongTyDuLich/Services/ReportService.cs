using System;
using System.Data;
using QuanLyCongTyDuLich.Data;
namespace QuanLyCongTyDuLich.Services
{
    public sealed class ReportService
    {
        public string DatabaseName(){return Convert.ToString(Db.Scalar("SELECT DB_NAME()"));}
        public DataTable Trips() { return Db.Query(@"SELECT c.MaChuyen,t.TenTour,c.NgayDi,c.NgayVe,c.SoLuongHienTai,c.SucChua,c.TrangThai,
 n.HoTen AS HuongDanVien FROM dbo.ChuyenKhachLe c JOIN dbo.TourDuLich t ON t.MaTour=c.MaTour
 LEFT JOIN dbo.PhanCongHuongDan pc ON pc.MaChuyen=c.MaChuyen LEFT JOIN dbo.NhanVienHD n ON n.MaNhanVien=pc.MaNhanVien ORDER BY c.NgayDi DESC"); }
        public DataTable Tickets() { return Db.Query(@"SELECT v.MaVe,c.MaChuyen,k.HoTen,v.SoNguoi,v.DiemDon,v.TongTienThanhToan,v.TrangThai
 FROM dbo.VeKhachLe v JOIN dbo.ChuyenKhachLe c ON c.MaChuyen=v.MaChuyen
 JOIN dbo.KhachHang k ON k.MaKhachHang=v.MaKhachHang ORDER BY v.MaVe DESC"); }
        public DataTable Groups() { return Db.Query(@"SELECT d.MaPhieuDoan,t.TenTour,k.HoTen,d.SoNguoiDi,d.NgayDiYeuCau,
 CAST(d.SoNguoiDi*d.DonGiaApDung AS DECIMAL(18,2)) AS TongChiPhi,d.TienCoc,
 CAST(CASE WHEN d.TrangThai=N'DaThanhToan' THEN 0 ELSE d.SoNguoiDi*d.DonGiaApDung-d.TienCoc END AS DECIMAL(18,2)) AS ConLai,
 d.CoBaoHiem,d.TrangThai FROM dbo.PhieuDangKyDoan d JOIN dbo.TourDuLich t ON t.MaTour=d.MaTour
 JOIN dbo.KhachHang k ON k.MaKhachHang=d.MaKhachHang ORDER BY d.NgayDiYeuCau DESC"); }
        public DataTable Surveys() { return Db.Query(@"SELECT s.MaKhaoSat,k.HoTen,s.MaChuyen,s.MaPhieuDoan,s.NgayKhaoSat,s.DiemDanhGia,s.NoiDungGopY
 FROM dbo.PhieuKhaoSat s JOIN dbo.KhachHang k ON k.MaKhachHang=s.MaKhachHang ORDER BY s.NgayKhaoSat DESC"); }
        public DataTable Payroll(int year, int month)
        {
            if(year<2000||year>2100||month<1||month>12)throw new ArgumentException("Thang nam khong hop le.");
            DateTime first=new DateTime(year,month,1),next=first.AddMonths(1);
            return Db.Query(@"SELECT n.MaNhanVien,n.HoTen,n.LuongCoBan,
 COALESCE(SUM(CASE WHEN (c.MaChuyen IS NOT NULL AND c.TrangThai=N'HoanThanh'
 AND c.NgayDi>=@from AND c.NgayDi<@to) OR (d.MaPhieuDoan IS NOT NULL AND d.TrangThai IN(N'DaHoanThanh',N'DaThanhToan')
 AND d.NgayDiYeuCau>=@from AND d.NgayDiYeuCau<@to) THEN p.TienCong ELSE 0 END),0) AS LuongTheoTour,
 n.LuongCoBan+COALESCE(SUM(CASE WHEN (c.MaChuyen IS NOT NULL AND c.TrangThai=N'HoanThanh'
 AND c.NgayDi>=@from AND c.NgayDi<@to) OR (d.MaPhieuDoan IS NOT NULL AND d.TrangThai IN(N'DaHoanThanh',N'DaThanhToan')
 AND d.NgayDiYeuCau>=@from AND d.NgayDiYeuCau<@to) THEN p.TienCong ELSE 0 END),0) AS TongLuong
 FROM dbo.NhanVienHD n LEFT JOIN dbo.PhanCongHuongDan p ON p.MaNhanVien=n.MaNhanVien
 LEFT JOIN dbo.ChuyenKhachLe c ON c.MaChuyen=p.MaChuyen LEFT JOIN dbo.PhieuDangKyDoan d ON d.MaPhieuDoan=p.MaPhieuDoan
 GROUP BY n.MaNhanVien,n.HoTen,n.LuongCoBan ORDER BY n.MaNhanVien",Db.P("@from",first),Db.P("@to",next));
        }
    }
}
