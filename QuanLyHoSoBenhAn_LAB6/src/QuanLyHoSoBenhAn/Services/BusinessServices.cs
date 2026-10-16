using System;
using System.Data;
using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Models;

namespace QuanLyHoSoBenhAn.Services
{
    public sealed class DangKyService
    {
        public DangKyResult DangKyLanDau(DangKyInput input)
        {
            if (input == null) throw new ArgumentNullException("input");
            var bn = Db.Out("@MaBenhNhan"); var hs = Db.Out("@MaHoSo");
            Db.Proc("dbo.usp_DangKyLanDau", Db.P("@HoTen", input.HoTen), Db.P("@DiaChi", input.DiaChi),
                Db.P("@NgaySinh", input.NgaySinh.Date), Db.P("@SoDienThoai", input.SoDienThoai),
                Db.P("@Anh3x4", input.Anh3x4), Db.P("@SoTien", input.LePhiNam), bn, hs);
            return new DangKyResult { MaBenhNhan = Convert.ToInt32(bn.Value), MaHoSo = Convert.ToInt32(hs.Value) };
        }
        public int GiaHanLePhi(int maHoSo, decimal soTien)
        {
            var output = Db.Out("@MaPhieuThu");
            Db.Proc("dbo.usp_GiaHanLePhi", Db.P("@MaHoSo", maHoSo), Db.P("@SoTien", soTien), output);
            return Convert.ToInt32(output.Value);
        }
        public DataTable DanhSach()
        {
            return Db.Query(@"SELECT b.MaBenhNhan,h.MaHoSo,b.HoTen,b.NgaySinh,b.SoDienThoai,b.DiaChi,
                  (SELECT MAX(p.NgayHetHan) FROM dbo.PhieuThuLePhi p WHERE p.MaBenhNhan=b.MaBenhNhan) AS HetHanLePhi
                  FROM dbo.BenhNhan b INNER JOIN dbo.HoSoBenhAn h ON h.MaBenhNhan=b.MaBenhNhan
                  ORDER BY h.MaHoSo DESC");
        }
    }

    public sealed class LichHenService
    {
        public int DatLich(int maHoSo, int maBacSi, int maNhanVien, DateTime ngayGioHen, string loaiHen, string noiDung)
        {
            var output = Db.Out("@MaLichHen");
            Db.Proc("dbo.usp_DatLichHen", Db.P("@MaHoSo", maHoSo), Db.P("@MaBacSi", maBacSi),
                Db.P("@MaNhanVien", maNhanVien > 0 ? (object)maNhanVien : DBNull.Value),
                Db.P("@NgayGioHen", ngayGioHen), Db.P("@LoaiHen", loaiHen),
                Db.P("@NoiDungHen", noiDung), output);
            return Convert.ToInt32(output.Value);
        }
        public void HuyLich(int maLichHen) { Db.Proc("dbo.usp_HuyLichHen", Db.P("@MaLichHen", maLichHen)); }
        public DataTable DanhSach()
        {
            return Db.Query(@"SELECT l.MaLichHen,l.MaHoSo,b.HoTen AS BenhNhan,l.MaBacSi,bs.HoTen AS BacSi,
                  l.NgayGioHen,l.LoaiHen,l.TrangThai,l.NoiDungHen
                  FROM dbo.LichHen l JOIN dbo.HoSoBenhAn h ON h.MaHoSo=l.MaHoSo
                  JOIN dbo.BenhNhan b ON b.MaBenhNhan=h.MaBenhNhan
                  JOIN dbo.BacSi bs ON bs.MaBacSi=l.MaBacSi ORDER BY l.NgayGioHen DESC");
        }
    }

    public sealed class KhamService
    {
        public int MoPhieuKham(KhamInput input)
        {
            var output = Db.Out("@MaPhieuKham");
            Db.Proc("dbo.usp_MoPhieuKham", Db.P("@MaHoSo", input.MaHoSo), Db.P("@MaBacSi", input.MaBacSi),
                Db.P("@MaKhoa", input.MaKhoa), Db.P("@LoaiKham", input.LoaiKham),
                Db.P("@MaLichHen", input.MaLichHen > 0 ? (object)input.MaLichHen : DBNull.Value), output);
            return Convert.ToInt32(output.Value);
        }
        public void CapNhat(CapNhatKhamInput input)
        {
            Db.Proc("dbo.usp_CapNhatKham", Db.P("@MaPhieuKham", input.MaPhieuKham),
              Db.P("@TrieuChung", input.TrieuChung), Db.P("@ThanNhiet", input.ThanNhiet.HasValue ? (object)input.ThanNhiet.Value : DBNull.Value),
              Db.P("@HuyetAp", input.HuyetAp), Db.P("@ChanDoan", input.ChanDoan), Db.P("@KetLuan", input.KetLuan));
        }
        public void HoanTat(int maPhieuKham) { Db.Proc("dbo.usp_HoanTatKham", Db.P("@MaPhieuKham", maPhieuKham)); }
        public DataTable DanhSach()
        {
            return Db.Query(@"SELECT k.MaPhieuKham,k.MaHoSo,b.HoTen AS BenhNhan,k.MaBacSi,
              bs.HoTen AS BacSi,k.MaKhoa,k.MaLichHen,k.NgayGioKham,k.LoaiKham,
              k.TrieuChung,k.ThanNhiet,k.HuyetAp,k.ChanDoan,k.KetLuan,k.TrangThai,k.NgayHoanTat
              FROM dbo.PhieuKhamBenh k JOIN dbo.HoSoBenhAn h ON h.MaHoSo=k.MaHoSo
              JOIN dbo.BenhNhan b ON b.MaBenhNhan=h.MaBenhNhan
              JOIN dbo.BacSi bs ON bs.MaBacSi=k.MaBacSi ORDER BY k.MaPhieuKham DESC");
        }
    }

    public sealed class XetNghiemService
    {
        public int ChiDinh(int maPhieuKham, int maLoai)
        {
            var output=Db.Out("@MaPhieuXetNghiem");
            Db.Proc("dbo.usp_ChiDinhXetNghiem",Db.P("@MaPhieuKham",maPhieuKham),Db.P("@MaLoaiXetNghiem",maLoai),output);
            return Convert.ToInt32(output.Value);
        }
        public void GhiKetQua(int maPhieuXN,string ketQua,string ketLuan)
        {
            Db.Proc("dbo.usp_GhiKetQuaXetNghiem",Db.P("@MaPhieuXetNghiem",maPhieuXN),Db.P("@KetQua",ketQua),Db.P("@KetLuan",ketLuan));
        }
        public DataTable DanhSach()
        {
            return Db.Query(@"SELECT x.MaPhieuXetNghiem,x.MaPhieuKham,x.MaLoaiXetNghiem,l.TenXetNghiem,
               x.DonGiaApDung,x.NgayChiDinh,x.NgayThucHien,x.KetQua,x.KetLuan,x.TrangThai
               FROM dbo.PhieuXetNghiem x JOIN dbo.LoaiXetNghiem l ON l.MaLoaiXetNghiem=x.MaLoaiXetNghiem
               ORDER BY x.MaPhieuXetNghiem DESC");
        }
    }

    public sealed class ToaThuocService
    {
        public int TaoToa(int maPhieuKham,string loiDan)
        {
            var output=Db.Out("@MaToaThuoc");
            Db.Proc("dbo.usp_TaoToaThuoc",Db.P("@MaPhieuKham",maPhieuKham),Db.P("@LoiDan",loiDan),output);
            return Convert.ToInt32(output.Value);
        }
        public void ThemThuoc(ThuocKeInput input)
        {
            Db.Proc("dbo.usp_ThemChiTietToa",Db.P("@MaToaThuoc",input.MaToaThuoc),Db.P("@MaThuoc",input.MaThuoc),
                Db.P("@SoLuong",input.SoLuong),Db.P("@LieuDung",input.LieuDung),
                Db.P("@SoLanDungTrongNgay",input.SoLanDungTrongNgay),Db.P("@CachDung",input.CachDung),Db.P("@SoNgayDung",input.SoNgayDung));
        }
        public DataTable DanhSach(int maToa)
        {
            return Db.Query(@"SELECT t.MaToaThuoc,t.MaPhieuKham,t.NgayLap,t.LoiDan,c.MaChiTietToa,
                th.TenThuoc,th.HamLuong,c.SoLuong,c.LieuDung,c.SoLanDungTrongNgay,c.CachDung,c.SoNgayDung
                FROM dbo.ToaThuoc t LEFT JOIN dbo.ChiTietToaThuoc c ON c.MaToaThuoc=t.MaToaThuoc
                LEFT JOIN dbo.Thuoc th ON th.MaThuoc=c.MaThuoc
                WHERE (@MaToaThuoc=0 OR t.MaToaThuoc=@MaToaThuoc) ORDER BY t.MaToaThuoc DESC",
                Db.P("@MaToaThuoc",maToa));
        }
    }

    public sealed class VienPhiService
    {
        public int LapHoaDon(int maPhieuKham)
        {
            var output=Db.Out("@MaHoaDon");
            Db.Proc("dbo.usp_LapHoaDon",Db.P("@MaPhieuKham",maPhieuKham),output);
            return Convert.ToInt32(output.Value);
        }
        public void ThanhToan(int maHoaDon,decimal soTienNhan)
        {
            Db.Proc("dbo.usp_XacNhanThanhToan",Db.P("@MaHoaDon",maHoaDon),Db.P("@SoTienNhan",soTienNhan));
        }
        public void GhiTheoDoi(int maPhieuKham,int maNhanVien,string dienBien,string phanUng,string hieuQua,string ghiChu)
        {
            Db.Proc("dbo.usp_GhiNhanTheoDoi",Db.P("@MaPhieuKham",maPhieuKham),Db.P("@MaNhanVien",maNhanVien),
                Db.P("@DienBienBenh",dienBien),Db.P("@PhanUngPhu",phanUng),Db.P("@HieuQuaDieuTri",hieuQua),Db.P("@GhiChu",ghiChu));
        }
        public DataTable DanhSach()
        {
            return Db.Query(@"SELECT v.MaHoaDon,v.MaPhieuKham,k.MaHoSo,k.LoaiKham,
                 v.TienKham,v.TienXetNghiem,v.TongTien,v.SoTienNhan,v.TrangThaiThanhToan,
                 v.NgayLap,v.NgayThanhToan,v.GhiChu
                 FROM dbo.HoaDonVienPhi v JOIN dbo.PhieuKhamBenh k ON k.MaPhieuKham=v.MaPhieuKham
                 ORDER BY v.MaHoaDon DESC");
        }
    }

    public sealed class HoSoService
    {
        public DataTable TimKiem(string tuKhoa)
        {
            return Db.Query(@"SELECT h.MaHoSo,b.MaBenhNhan,b.HoTen,b.NgaySinh,b.SoDienThoai,b.DiaChi,
               h.NgayLap,(SELECT MAX(p.NgayHetHan) FROM dbo.PhieuThuLePhi p WHERE p.MaBenhNhan=b.MaBenhNhan) AS NgayHetHanLePhi
               FROM dbo.HoSoBenhAn h JOIN dbo.BenhNhan b ON b.MaBenhNhan=h.MaBenhNhan
               WHERE @TuKhoa='' OR CONVERT(VARCHAR(20),h.MaHoSo)=@TuKhoa
                  OR b.HoTen LIKE N'%' + @TuKhoa + N'%' OR b.SoDienThoai LIKE '%' + @TuKhoa + '%'
               ORDER BY h.MaHoSo DESC",Db.P("@TuKhoa",tuKhoa??""));
        }
        public DataTable LichSu(int maHoSo)
        {
            return Db.Query(@"SELECT k.MaPhieuKham,k.NgayGioKham,k.LoaiKham,bs.HoTen AS BacSi,
                kh.TenKhoa,k.TrieuChung,k.ChanDoan,k.KetLuan,k.TrangThai,k.NgayHoanTat,
                v.TongTien,v.TrangThaiThanhToan
                FROM dbo.PhieuKhamBenh k JOIN dbo.BacSi bs ON bs.MaBacSi=k.MaBacSi
                JOIN dbo.KhoaDieuTri kh ON kh.MaKhoa=k.MaKhoa
                LEFT JOIN dbo.HoaDonVienPhi v ON v.MaPhieuKham=k.MaPhieuKham
                WHERE k.MaHoSo=@MaHoSo ORDER BY k.NgayGioKham DESC",Db.P("@MaHoSo",maHoSo));
        }
        public DataTable ChiTietXetNghiem(int maHoSo)
        {
            return Db.Query(@"SELECT k.MaPhieuKham,x.MaPhieuXetNghiem,l.TenXetNghiem,x.KetQua,x.KetLuan,x.TrangThai
                FROM dbo.PhieuKhamBenh k JOIN dbo.PhieuXetNghiem x ON x.MaPhieuKham=k.MaPhieuKham
                JOIN dbo.LoaiXetNghiem l ON l.MaLoaiXetNghiem=x.MaLoaiXetNghiem
                WHERE k.MaHoSo=@MaHoSo ORDER BY k.MaPhieuKham DESC",Db.P("@MaHoSo",maHoSo));
        }
        public DataTable ChiTietToa(int maHoSo)
        {
            return Db.Query(@"SELECT k.MaPhieuKham,t.MaToaThuoc,th.TenThuoc,th.HamLuong,
                c.SoLuong,c.LieuDung,c.SoLanDungTrongNgay,c.CachDung,c.SoNgayDung,t.LoiDan
                FROM dbo.PhieuKhamBenh k JOIN dbo.ToaThuoc t ON t.MaPhieuKham=k.MaPhieuKham
                JOIN dbo.ChiTietToaThuoc c ON c.MaToaThuoc=t.MaToaThuoc
                JOIN dbo.Thuoc th ON th.MaThuoc=c.MaThuoc WHERE k.MaHoSo=@MaHoSo
                ORDER BY k.MaPhieuKham DESC",Db.P("@MaHoSo",maHoSo));
        }
        public DataTable TheoDoi(int maHoSo)
        {
            return Db.Query(@"SELECT k.MaPhieuKham,d.NgayGhiNhan,n.HoTen AS NhanVienCSKH,
                d.DienBienBenh,d.PhanUngPhu,d.HieuQuaDieuTri,d.GhiChu
                FROM dbo.PhieuKhamBenh k JOIN dbo.TheoDoiDieuTri d ON d.MaPhieuKham=k.MaPhieuKham
                JOIN dbo.NhanVienCSKH n ON n.MaNhanVien=d.MaNhanVien
                WHERE k.MaHoSo=@MaHoSo ORDER BY d.NgayGhiNhan DESC",Db.P("@MaHoSo",maHoSo));
        }
    }

    public sealed class DanhMucService
    {
        public DataTable Khoa() { return Db.Query("SELECT MaKhoa,TenKhoa,MoTa FROM dbo.KhoaDieuTri ORDER BY MaKhoa"); }
        public DataTable BacSi() { return Db.Query("SELECT MaBacSi,HoTen,MaKhoa,ChuyenMon,SoDienThoai FROM dbo.BacSi ORDER BY MaBacSi"); }
        public DataTable NhanVien() { return Db.Query("SELECT MaNhanVien,HoTen,SoDienThoai FROM dbo.NhanVienCSKH ORDER BY MaNhanVien"); }
        public DataTable Thuoc() { return Db.Query("SELECT MaThuoc,TenThuoc,HamLuong,DonViTinh,MoTa FROM dbo.Thuoc ORDER BY MaThuoc"); }
        public DataTable LoaiXN() { return Db.Query("SELECT MaLoaiXetNghiem,TenXetNghiem,DonGia,MoTa FROM dbo.LoaiXetNghiem ORDER BY MaLoaiXetNghiem"); }
        public void ThemKhoa(string ten,string moTa) { Db.Execute("INSERT dbo.KhoaDieuTri(TenKhoa,MoTa) VALUES(@Ten,@MoTa)",Db.P("@Ten",ten),Db.P("@MoTa",moTa)); }
        public void ThemBacSi(int maKhoa,string ten,string chuyenMon,string phone) { Db.Execute("INSERT dbo.BacSi(MaKhoa,HoTen,ChuyenMon,SoDienThoai) VALUES(@Khoa,@Ten,@ChuyenMon,@Phone)",Db.P("@Khoa",maKhoa),Db.P("@Ten",ten),Db.P("@ChuyenMon",chuyenMon),Db.P("@Phone",phone)); }
        public void ThemNhanVien(string ten,string phone) { Db.Execute("INSERT dbo.NhanVienCSKH(HoTen,SoDienThoai) VALUES(@Ten,@Phone)",Db.P("@Ten",ten),Db.P("@Phone",phone)); }
        public void ThemThuoc(string ten,string hamLuong,string dvt,string moTa) { Db.Execute("INSERT dbo.Thuoc(TenThuoc,HamLuong,DonViTinh,MoTa) VALUES(@Ten,@HamLuong,@DVT,@MoTa)",Db.P("@Ten",ten),Db.P("@HamLuong",hamLuong),Db.P("@DVT",dvt),Db.P("@MoTa",moTa)); }
        public void ThemLoaiXN(string ten,decimal gia,string moTa) { Db.Execute("INSERT dbo.LoaiXetNghiem(TenXetNghiem,DonGia,MoTa) VALUES(@Ten,@Gia,@MoTa)",Db.P("@Ten",ten),Db.P("@Gia",gia),Db.P("@MoTa",moTa)); }
    }
}
