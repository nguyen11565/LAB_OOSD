/* BAI 6 - QUAN LY CONG TY DU LICH. Chay voi SQL Server (SSMS, windows authentication).
   Chay script nay truoc 02_Seed.sql. KHONG xoa du lieu neu DB da ton tai. */
IF DB_ID(N'QuanLyCongTyDuLich') IS NULL CREATE DATABASE QuanLyCongTyDuLich;
GO
USE QuanLyCongTyDuLich;
GO
IF OBJECT_ID('dbo.TourDuLich', 'U') IS NOT NULL
    THROW 50001, N'Database da co bang; dung database moi de chay schema mot lan.', 1;
GO
CREATE TABLE dbo.TourDuLich (
 MaTour NVARCHAR(15) NOT NULL PRIMARY KEY,
 TenTour NVARCHAR(150) NOT NULL,
 SoNgay INT NOT NULL CHECK (SoNgay >= 1),
 SoDem INT NOT NULL CHECK (SoDem >= 0 AND SoDem <= SoNgay),
 DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia > 0)
);
CREATE TABLE dbo.DiemThamQuan (
 MaDiemThamQuan NVARCHAR(15) NOT NULL PRIMARY KEY,
 TenDiemThamQuan NVARCHAR(150) NOT NULL,
 DiaDiem NVARCHAR(200) NOT NULL,
 NoiDungYNghia NVARCHAR(1000) NOT NULL
);
CREATE TABLE dbo.NoiDungChan (
 MaNoiDungChan NVARCHAR(15) NOT NULL PRIMARY KEY,
 TenNoiDungChan NVARCHAR(150) NOT NULL,
 DoiPhuongTien BIT NOT NULL,
 CoNoiAn BIT NOT NULL,
 CoKhachSan BIT NOT NULL,
 LoaiKhachSan INT NULL,
 CONSTRAINT CK_NoiDungChan_KhachSan CHECK (
   (CoKhachSan = 0 AND LoaiKhachSan IS NULL) OR
   (CoKhachSan = 1 AND LoaiKhachSan BETWEEN 2 AND 5))
);
CREATE TABLE dbo.PhuongTien (
 MaPhuongTien NVARCHAR(15) NOT NULL PRIMARY KEY,
 TenPhuongTien NVARCHAR(100) NOT NULL,
 LoaiPhuongTien NVARCHAR(80) NOT NULL
);
CREATE TABLE dbo.DiemBanVe (
 MaDiemBan NVARCHAR(15) NOT NULL PRIMARY KEY,
 TenDiemBan NVARCHAR(120) NOT NULL,
 DiaChi NVARCHAR(200) NOT NULL,
 SoDienThoai NVARCHAR(25) NOT NULL
);
CREATE TABLE dbo.KhachHang (
 MaKhachHang NVARCHAR(15) NOT NULL PRIMARY KEY,
 HoTen NVARCHAR(120) NOT NULL,
 SoDienThoai NVARCHAR(25) NOT NULL,
 DiaChi NVARCHAR(200) NOT NULL,
 Email NVARCHAR(150) NULL
);
CREATE TABLE dbo.NhanVienHD (
 MaNhanVien NVARCHAR(15) NOT NULL PRIMARY KEY,
 HoTen NVARCHAR(120) NOT NULL,
 SoDienThoai NVARCHAR(25) NOT NULL,
 LuongCoBan DECIMAL(18,2) NOT NULL CHECK (LuongCoBan >= 0)
);
CREATE TABLE dbo.TourDiemThamQuan (
 MaTour NVARCHAR(15) NOT NULL REFERENCES dbo.TourDuLich(MaTour),
 MaDiemThamQuan NVARCHAR(15) NOT NULL REFERENCES dbo.DiemThamQuan(MaDiemThamQuan),
 ThuTu INT NOT NULL CHECK (ThuTu > 0),
 CONSTRAINT PK_TourDiemThamQuan PRIMARY KEY (MaTour, MaDiemThamQuan),
 CONSTRAINT UQ_TourDiemThamQuan_ThuTu UNIQUE (MaTour, ThuTu)
);
CREATE TABLE dbo.TourNoiDungChan (
 MaTour NVARCHAR(15) NOT NULL REFERENCES dbo.TourDuLich(MaTour),
 ThuTu INT NOT NULL CHECK (ThuTu > 0),
 MaNoiDungChan NVARCHAR(15) NOT NULL REFERENCES dbo.NoiDungChan(MaNoiDungChan),
 MaPhuongTien NVARCHAR(15) NOT NULL REFERENCES dbo.PhuongTien(MaPhuongTien),
 CONSTRAINT PK_TourNoiDungChan PRIMARY KEY (MaTour, ThuTu)
);
CREATE TABLE dbo.ChuyenKhachLe (
 MaChuyen NVARCHAR(15) NOT NULL PRIMARY KEY,
 MaTour NVARCHAR(15) NOT NULL REFERENCES dbo.TourDuLich(MaTour),
 DonGiaApDung DECIMAL(18,2) NOT NULL CHECK (DonGiaApDung > 0),
 NgayDi DATE NOT NULL,
 NgayVe DATE NOT NULL,
 SucChua INT NOT NULL CHECK (SucChua >= 1),
 SoLuongHienTai INT NOT NULL CONSTRAINT DF_ChuyenKhachLe_SoLuong DEFAULT (0),
 TrangThai NVARCHAR(20) NOT NULL CONSTRAINT DF_ChuyenKhachLe_TrangThai DEFAULT N'DangMo',
 CONSTRAINT CK_ChuyenKhachLe_Ngay CHECK (NgayVe >= NgayDi),
 CONSTRAINT CK_ChuyenKhachLe_SoLuong CHECK (SoLuongHienTai BETWEEN 0 AND SucChua),
 CONSTRAINT CK_ChuyenKhachLe_TrangThai CHECK (TrangThai IN (N'DangMo', N'HoanThanh'))
);
CREATE TABLE dbo.VeKhachLe (
 MaVe NVARCHAR(15) NOT NULL PRIMARY KEY,
 MaChuyen NVARCHAR(15) NOT NULL REFERENCES dbo.ChuyenKhachLe(MaChuyen),
 MaKhachHang NVARCHAR(15) NOT NULL REFERENCES dbo.KhachHang(MaKhachHang),
 MaDiemBan NVARCHAR(15) NOT NULL REFERENCES dbo.DiemBanVe(MaDiemBan),
 NgayDangKy DATE NOT NULL,
 DiemDon NVARCHAR(200) NOT NULL,
 SoNguoi INT NOT NULL CHECK (SoNguoi BETWEEN 1 AND 11),
 TongTienThanhToan DECIMAL(18,2) NOT NULL CHECK (TongTienThanhToan > 0),
 TrangThai NVARCHAR(20) NOT NULL CONSTRAINT DF_VeKhachLe_TrangThai DEFAULT N'DaThanhToan',
 CONSTRAINT CK_VeKhachLe_TrangThai CHECK (TrangThai IN (N'DaThanhToan', N'DaHuy'))
);
CREATE TABLE dbo.PhieuDangKyDoan (
 MaPhieuDoan NVARCHAR(15) NOT NULL PRIMARY KEY,
 MaTour NVARCHAR(15) NOT NULL REFERENCES dbo.TourDuLich(MaTour),
 MaKhachHang NVARCHAR(15) NOT NULL REFERENCES dbo.KhachHang(MaKhachHang),
 DonGiaApDung DECIMAL(18,2) NOT NULL CHECK (DonGiaApDung > 0),
 TenCoQuan NVARCHAR(150) NOT NULL,
 DiaChiCoQuan NVARCHAR(200) NOT NULL,
 DienThoaiCoQuan NVARCHAR(25) NOT NULL,
 NguoiDaiDien NVARCHAR(120) NOT NULL,
 SoNguoiDi INT NOT NULL CHECK (SoNguoiDi >= 12),
 NgayDiYeuCau DATE NOT NULL,
 DiemDon NVARCHAR(200) NOT NULL,
 TienCoc DECIMAL(18,2) NOT NULL CHECK (TienCoc > 0),
 CoBaoHiem BIT NOT NULL,
 TrangThai NVARCHAR(20) NOT NULL CONSTRAINT DF_PhieuDangKyDoan_TrangThai DEFAULT N'DaDatCoc',
 CONSTRAINT CK_PhieuDangKyDoan_TrangThai CHECK (TrangThai IN (N'DaDatCoc', N'DaHoanThanh', N'DaThanhToan', N'DaHuy'))
);
CREATE TABLE dbo.DanhSachBaoHiem (
 MaSoBaoHiem NVARCHAR(36) NOT NULL PRIMARY KEY,
 MaPhieuDoan NVARCHAR(15) NOT NULL REFERENCES dbo.PhieuDangKyDoan(MaPhieuDoan),
 HoTen NVARCHAR(120) NOT NULL,
 CMNDPassport NVARCHAR(30) NOT NULL,
 NgaySinh DATE NOT NULL,
 CONSTRAINT UQ_DanhSachBaoHiem_Passport UNIQUE(MaPhieuDoan, CMNDPassport)
);
CREATE TABLE dbo.ThanhToanDoan (
 MaThanhToan NVARCHAR(36) NOT NULL PRIMARY KEY,
 MaPhieuDoan NVARCHAR(15) NOT NULL UNIQUE REFERENCES dbo.PhieuDangKyDoan(MaPhieuDoan),
 NgayThanhToan DATE NOT NULL,
 SoTien DECIMAL(18,2) NOT NULL CHECK (SoTien > 0)
);
CREATE TABLE dbo.PhanCongHuongDan (
 MaPhanCong NVARCHAR(36) NOT NULL PRIMARY KEY,
 MaNhanVien NVARCHAR(15) NOT NULL REFERENCES dbo.NhanVienHD(MaNhanVien),
 MaChuyen NVARCHAR(15) NULL REFERENCES dbo.ChuyenKhachLe(MaChuyen),
 MaPhieuDoan NVARCHAR(15) NULL REFERENCES dbo.PhieuDangKyDoan(MaPhieuDoan),
 TienCong DECIMAL(18,2) NOT NULL CHECK (TienCong > 0),
 CONSTRAINT CK_PhanCong_MotDich CHECK ((MaChuyen IS NULL AND MaPhieuDoan IS NOT NULL) OR (MaChuyen IS NOT NULL AND MaPhieuDoan IS NULL)),
 CONSTRAINT UQ_PhanCong_NhanVien_Chuyen UNIQUE(MaNhanVien, MaChuyen),
 CONSTRAINT UQ_PhanCong_NhanVien_Doan UNIQUE(MaNhanVien, MaPhieuDoan)
);
CREATE UNIQUE INDEX UX_PhanCong_MotNhanVienMoiChuyen ON dbo.PhanCongHuongDan(MaChuyen) WHERE MaChuyen IS NOT NULL;
CREATE TABLE dbo.PhieuKhaoSat (
 MaKhaoSat NVARCHAR(15) NOT NULL PRIMARY KEY,
 MaKhachHang NVARCHAR(15) NOT NULL REFERENCES dbo.KhachHang(MaKhachHang),
 MaChuyen NVARCHAR(15) NULL REFERENCES dbo.ChuyenKhachLe(MaChuyen),
 MaPhieuDoan NVARCHAR(15) NULL REFERENCES dbo.PhieuDangKyDoan(MaPhieuDoan),
 NgayKhaoSat DATE NOT NULL,
 NoiDungGopY NVARCHAR(2000) NOT NULL,
 DiemDanhGia INT NOT NULL CHECK (DiemDanhGia BETWEEN 1 AND 5),
 CONSTRAINT CK_PhieuKhaoSat_MotDich CHECK ((MaChuyen IS NOT NULL AND MaPhieuDoan IS NULL) OR (MaChuyen IS NULL AND MaPhieuDoan IS NOT NULL))
);
CREATE INDEX IX_VeKhachLe_MaChuyen ON dbo.VeKhachLe(MaChuyen, TrangThai);
CREATE INDEX IX_PhanCong_NhanVien ON dbo.PhanCongHuongDan(MaNhanVien);
CREATE INDEX IX_PhieuDangKyDoan_NgayDi ON dbo.PhieuDangKyDoan(NgayDiYeuCau);
GO
PRINT N'Da tao CSDL QuanLyCongTyDuLich va 16 bang.';
