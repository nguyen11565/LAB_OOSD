/* Required SET options for indexed persisted computed columns and filtered indexes. */
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;
GO
/* LAB6 - Quan ly ho so benh an | Microsoft SQL Server 2017+.
   Run in SSMS in order: 01_Schema -> 02_Procedures -> 03_Seed -> Tests/01_TestCases.
   Safe guard: do not overwrite an existing populated database. */
IF DB_ID(N'QuanLyHoSoBenhAn_LAB6') IS NULL
    CREATE DATABASE QuanLyHoSoBenhAn_LAB6;
GO
USE QuanLyHoSoBenhAn_LAB6;
GO
IF OBJECT_ID('dbo.BenhNhan', 'U') IS NOT NULL
    THROW 50001, N'CSDL da co cac bang. Khong chay lai 01_Schema tren du lieu dang su dung.', 1;
GO
CREATE TABLE dbo.BenhNhan (
    MaBenhNhan INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_BenhNhan PRIMARY KEY,
    HoTen NVARCHAR(120) NOT NULL,
    DiaChi NVARCHAR(250) NOT NULL,
    NgaySinh DATE NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    Anh3x4 NVARCHAR(300) NULL,
    NgayTao DATETIME2(0) NOT NULL CONSTRAINT DF_BenhNhan_NgayTao DEFAULT SYSDATETIME(),
    CONSTRAINT CK_BenhNhan_Phone CHECK (LEN(SoDienThoai) BETWEEN 9 AND 15 AND SoDienThoai NOT LIKE '%[^0-9]%'),
    CONSTRAINT UQ_BenhNhan_DanhTinh UNIQUE (HoTen, NgaySinh, SoDienThoai)
);
CREATE TABLE dbo.PhieuDangKy (
    MaPhieuDangKy INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuDangKy PRIMARY KEY,
    MaBenhNhan INT NOT NULL CONSTRAINT UQ_PhieuDangKy_BenhNhan UNIQUE,
    NgayDangKy DATETIME2(0) NOT NULL CONSTRAINT DF_PhieuDangKy_Ngay DEFAULT SYSDATETIME(),
    HoTen NVARCHAR(120) NOT NULL,
    DiaChi NVARCHAR(250) NOT NULL,
    NgaySinh DATE NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    Anh3x4 NVARCHAR(300) NULL,
    TrangThai VARCHAR(20) NOT NULL CONSTRAINT DF_PhieuDangKy_TrangThai DEFAULT 'DA_XAC_NHAN',
    CONSTRAINT FK_PhieuDangKy_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan),
    CONSTRAINT CK_PhieuDangKy_TrangThai CHECK (TrangThai IN ('DA_XAC_NHAN', 'TU_CHOI'))
);
CREATE TABLE dbo.PhieuThuLePhi (
    MaPhieuThu INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuThuLePhi PRIMARY KEY,
    MaBenhNhan INT NOT NULL,
    NgayThu DATETIME2(0) NOT NULL CONSTRAINT DF_PhieuThu_Ngay DEFAULT SYSDATETIME(),
    SoTien DECIMAL(18,2) NOT NULL,
    NgayBatDauHieuLuc DATE NOT NULL,
    NgayHetHan DATE NOT NULL,
    CONSTRAINT FK_PhieuThu_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan),
    CONSTRAINT CK_PhieuThu_SoTien CHECK (SoTien > 0),
    CONSTRAINT CK_PhieuThu_KhoangNgay CHECK (NgayHetHan > NgayBatDauHieuLuc),
    CONSTRAINT UQ_PhieuThu_BenhNhan_Ngay UNIQUE (MaBenhNhan, NgayBatDauHieuLuc)
);
CREATE TABLE dbo.HoSoBenhAn (
    MaHoSo INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_HoSoBenhAn PRIMARY KEY,
    MaBenhNhan INT NOT NULL CONSTRAINT UQ_HoSo_BenhNhan UNIQUE,
    MaPhieuDangKy INT NOT NULL CONSTRAINT UQ_HoSo_PhieuDangKy UNIQUE,
    NgayLap DATETIME2(0) NOT NULL CONSTRAINT DF_HoSo_Ngay DEFAULT SYSDATETIME(),
    GhiChu NVARCHAR(500) NULL,
    CONSTRAINT FK_HoSo_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan),
    CONSTRAINT FK_HoSo_DangKy FOREIGN KEY (MaPhieuDangKy) REFERENCES dbo.PhieuDangKy(MaPhieuDangKy)
);
CREATE TABLE dbo.KhoaDieuTri (
    MaKhoa INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Khoa PRIMARY KEY,
    TenKhoa NVARCHAR(120) NOT NULL CONSTRAINT UQ_Khoa_Ten UNIQUE,
    MoTa NVARCHAR(500) NULL
);
CREATE TABLE dbo.BacSi (
    MaBacSi INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_BacSi PRIMARY KEY,
    MaKhoa INT NOT NULL,
    HoTen NVARCHAR(120) NOT NULL,
    ChuyenMon NVARCHAR(150) NOT NULL,
    SoDienThoai VARCHAR(15) NULL,
    CONSTRAINT FK_BacSi_Khoa FOREIGN KEY (MaKhoa) REFERENCES dbo.KhoaDieuTri(MaKhoa)
);
CREATE TABLE dbo.NhanVienCSKH (
    MaNhanVien INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhanVien PRIMARY KEY,
    HoTen NVARCHAR(120) NOT NULL,
    SoDienThoai VARCHAR(15) NULL
);
CREATE TABLE dbo.LichHen (
    MaLichHen INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_LichHen PRIMARY KEY,
    MaHoSo INT NOT NULL,
    MaBacSi INT NOT NULL,
    MaNhanVien INT NULL,
    NgayLap DATETIME2(0) NOT NULL CONSTRAINT DF_LichHen_NgayLap DEFAULT SYSDATETIME(),
    NgayGioHen DATETIME2(0) NOT NULL,
    LoaiHen VARCHAR(20) NOT NULL,
    NoiDungHen NVARCHAR(300) NULL,
    TrangThai VARCHAR(20) NOT NULL CONSTRAINT DF_LichHen_TrangThai DEFAULT 'CHO_KHAM',
    /* One live quarterly appointment per medical record per calendar quarter. */
    NamQuyHieuLuc AS (CASE WHEN LoaiHen = 'DINH_KY' AND TrangThai <> 'DA_HUY'
                        THEN YEAR(NgayGioHen) * 10 + DATEPART(QUARTER, NgayGioHen) ELSE NULL END) PERSISTED,
    NgayHenHieuLuc AS (CASE WHEN TrangThai <> 'DA_HUY' THEN NgayGioHen ELSE NULL END) PERSISTED,
    CONSTRAINT FK_LichHen_HoSo FOREIGN KEY (MaHoSo) REFERENCES dbo.HoSoBenhAn(MaHoSo),
    CONSTRAINT FK_LichHen_BacSi FOREIGN KEY (MaBacSi) REFERENCES dbo.BacSi(MaBacSi),
    CONSTRAINT FK_LichHen_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES dbo.NhanVienCSKH(MaNhanVien),
    CONSTRAINT CK_LichHen_Loai CHECK (LoaiHen IN ('KHAM_BENH', 'DINH_KY')),
    CONSTRAINT CK_LichHen_TrangThai CHECK (TrangThai IN ('CHO_KHAM', 'DANG_KHAM', 'DA_KHAM', 'DA_HUY'))
);
CREATE UNIQUE INDEX UX_LichHen_HoSo_NamQuy ON dbo.LichHen(MaHoSo, NamQuyHieuLuc)
  WHERE LoaiHen='DINH_KY' AND TrangThai <> 'DA_HUY';
CREATE UNIQUE INDEX UX_LichHen_BacSi_Gio ON dbo.LichHen(MaBacSi, NgayGioHen)
  WHERE TrangThai <> 'DA_HUY';
CREATE TABLE dbo.PhieuKhamBenh (
    MaPhieuKham INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuKham PRIMARY KEY,
    MaHoSo INT NOT NULL,
    MaBacSi INT NOT NULL,
    MaKhoa INT NOT NULL,
    MaLichHen INT NULL,
    NgayGioKham DATETIME2(0) NOT NULL CONSTRAINT DF_PhieuKham_Ngay DEFAULT SYSDATETIME(),
    LoaiKham VARCHAR(20) NOT NULL,
    TrieuChung NVARCHAR(1000) NULL,
    ThanNhiet DECIMAL(4,1) NULL,
    HuyetAp NVARCHAR(30) NULL,
    ChanDoan NVARCHAR(1000) NULL,
    KetLuan NVARCHAR(1000) NULL,
    TrangThai VARCHAR(20) NOT NULL CONSTRAINT DF_PhieuKham_TrangThai DEFAULT 'DANG_KHAM',
    NgayHoanTat DATETIME2(0) NULL,
    NamQuyKham AS (CASE WHEN LoaiKham = 'DINH_KY'
                      THEN YEAR(NgayGioKham) * 10 + DATEPART(QUARTER, NgayGioKham) ELSE NULL END) PERSISTED,
    CONSTRAINT FK_PhieuKham_HoSo FOREIGN KEY (MaHoSo) REFERENCES dbo.HoSoBenhAn(MaHoSo),
    CONSTRAINT FK_PhieuKham_BacSi FOREIGN KEY (MaBacSi) REFERENCES dbo.BacSi(MaBacSi),
    CONSTRAINT FK_PhieuKham_Khoa FOREIGN KEY (MaKhoa) REFERENCES dbo.KhoaDieuTri(MaKhoa),
    CONSTRAINT FK_PhieuKham_LichHen FOREIGN KEY (MaLichHen) REFERENCES dbo.LichHen(MaLichHen),
    CONSTRAINT CK_PhieuKham_Loai CHECK (LoaiKham IN ('KHAM_BENH', 'DINH_KY')),
    CONSTRAINT CK_PhieuKham_TrangThai CHECK (TrangThai IN ('DANG_KHAM', 'HOAN_TAT')),
    CONSTRAINT CK_PhieuKham_Nhiet CHECK (ThanNhiet IS NULL OR ThanNhiet BETWEEN 30 AND 45),
    CONSTRAINT CK_PhieuKham_HoanTat CHECK (
        (TrangThai='DANG_KHAM' AND NgayHoanTat IS NULL) OR
        (TrangThai='HOAN_TAT' AND NgayHoanTat IS NOT NULL))
);
CREATE UNIQUE INDEX UX_PhieuKham_HoSo_Quy ON dbo.PhieuKhamBenh(MaHoSo, NamQuyKham)
  WHERE LoaiKham='DINH_KY';
/* SQL Server UNIQUE on a nullable column permits only one NULL without a filter. */
CREATE UNIQUE INDEX UX_PhieuKham_LichHen ON dbo.PhieuKhamBenh(MaLichHen)
  WHERE MaLichHen IS NOT NULL;
CREATE TABLE dbo.Thuoc (
    MaThuoc INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Thuoc PRIMARY KEY,
    TenThuoc NVARCHAR(160) NOT NULL,
    HamLuong NVARCHAR(80) NOT NULL,
    DonViTinh NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(500) NULL,
    CONSTRAINT UQ_Thuoc_Ten_HamLuong UNIQUE (TenThuoc, HamLuong)
);
CREATE TABLE dbo.ToaThuoc (
    MaToaThuoc INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ToaThuoc PRIMARY KEY,
    MaPhieuKham INT NOT NULL CONSTRAINT UQ_Toa_PhieuKham UNIQUE,
    NgayLap DATETIME2(0) NOT NULL CONSTRAINT DF_Toa_Ngay DEFAULT SYSDATETIME(),
    LoiDan NVARCHAR(1000) NULL,
    CONSTRAINT FK_Toa_PhieuKham FOREIGN KEY (MaPhieuKham) REFERENCES dbo.PhieuKhamBenh(MaPhieuKham)
);
CREATE TABLE dbo.ChiTietToaThuoc (
    MaChiTietToa INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ChiTietToa PRIMARY KEY,
    MaToaThuoc INT NOT NULL,
    MaThuoc INT NOT NULL,
    SoLuong INT NOT NULL,
    LieuDung NVARCHAR(120) NOT NULL,
    SoLanDungTrongNgay INT NOT NULL,
    CachDung NVARCHAR(200) NOT NULL,
    SoNgayDung INT NOT NULL,
    CONSTRAINT FK_ChiTiet_Toa FOREIGN KEY (MaToaThuoc) REFERENCES dbo.ToaThuoc(MaToaThuoc),
    CONSTRAINT FK_ChiTiet_Thuoc FOREIGN KEY (MaThuoc) REFERENCES dbo.Thuoc(MaThuoc),
    CONSTRAINT UQ_ChiTiet_Toa_Thuoc UNIQUE (MaToaThuoc, MaThuoc),
    CONSTRAINT CK_ChiTiet_SoLieu CHECK (SoLuong > 0 AND SoLanDungTrongNgay > 0 AND SoNgayDung > 0)
);
CREATE TABLE dbo.LoaiXetNghiem (
    MaLoaiXetNghiem INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_LoaiXN PRIMARY KEY,
    TenXetNghiem NVARCHAR(160) NOT NULL CONSTRAINT UQ_LoaiXN_Ten UNIQUE,
    MoTa NVARCHAR(500) NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    CONSTRAINT CK_LoaiXN_DonGia CHECK (DonGia >= 0)
);
CREATE TABLE dbo.PhieuXetNghiem (
    MaPhieuXetNghiem INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuXN PRIMARY KEY,
    MaPhieuKham INT NOT NULL,
    MaLoaiXetNghiem INT NOT NULL,
    NgayChiDinh DATETIME2(0) NOT NULL CONSTRAINT DF_PhieuXN_Ngay DEFAULT SYSDATETIME(),
    NgayThucHien DATETIME2(0) NULL,
    KetQua NVARCHAR(2000) NULL,
    KetLuan NVARCHAR(1000) NULL,
    DonGiaApDung DECIMAL(18,2) NOT NULL,
    TrangThai VARCHAR(20) NOT NULL CONSTRAINT DF_PhieuXN_TrangThai DEFAULT 'CHO_KET_QUA',
    CONSTRAINT FK_PhieuXN_PhieuKham FOREIGN KEY (MaPhieuKham) REFERENCES dbo.PhieuKhamBenh(MaPhieuKham),
    CONSTRAINT FK_PhieuXN_Loai FOREIGN KEY (MaLoaiXetNghiem) REFERENCES dbo.LoaiXetNghiem(MaLoaiXetNghiem),
    CONSTRAINT UQ_PhieuXN_Phieu_Loai UNIQUE (MaPhieuKham, MaLoaiXetNghiem),
    CONSTRAINT CK_PhieuXN_DonGia CHECK (DonGiaApDung >= 0),
    CONSTRAINT CK_PhieuXN_TrangThai CHECK (TrangThai IN ('CHO_KET_QUA', 'DA_CO_KET_QUA')),
    CONSTRAINT CK_PhieuXN_KetQua CHECK (
        (TrangThai='CHO_KET_QUA' AND NgayThucHien IS NULL) OR
        (TrangThai='DA_CO_KET_QUA' AND NgayThucHien IS NOT NULL AND KetQua IS NOT NULL))
);
CREATE TABLE dbo.HoaDonVienPhi (
    MaHoaDon INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_HoaDon PRIMARY KEY,
    MaPhieuKham INT NOT NULL CONSTRAINT UQ_HoaDon_PhieuKham UNIQUE,
    NgayLap DATETIME2(0) NOT NULL CONSTRAINT DF_HoaDon_Ngay DEFAULT SYSDATETIME(),
    TienKham DECIMAL(18,2) NOT NULL,
    TienXetNghiem DECIMAL(18,2) NOT NULL,
    TongTien DECIMAL(18,2) NOT NULL,
    SoTienNhan DECIMAL(18,2) NULL,
    NgayThanhToan DATETIME2(0) NULL,
    TrangThaiThanhToan VARCHAR(25) NOT NULL CONSTRAINT DF_HoaDon_TrangThai DEFAULT 'CHUA_THANH_TOAN',
    GhiChu NVARCHAR(300) NULL,
    CONSTRAINT FK_HoaDon_PhieuKham FOREIGN KEY (MaPhieuKham) REFERENCES dbo.PhieuKhamBenh(MaPhieuKham),
    CONSTRAINT CK_HoaDon_SoTien CHECK (TienKham >= 0 AND TienXetNghiem >= 0 AND TongTien = TienKham + TienXetNghiem AND (SoTienNhan IS NULL OR SoTienNhan >= 0)),
    CONSTRAINT CK_HoaDon_TrangThai CHECK (
      (TrangThaiThanhToan='CHUA_THANH_TOAN' AND NgayThanhToan IS NULL AND SoTienNhan IS NULL) OR
      (TrangThaiThanhToan='DA_THANH_TOAN' AND NgayThanhToan IS NOT NULL AND SoTienNhan IS NOT NULL AND SoTienNhan >= TongTien))
);
CREATE TABLE dbo.TheoDoiDieuTri (
    MaTheoDoi INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TheoDoi PRIMARY KEY,
    MaPhieuKham INT NOT NULL,
    MaNhanVien INT NOT NULL,
    NgayGhiNhan DATETIME2(0) NOT NULL CONSTRAINT DF_TheoDoi_Ngay DEFAULT SYSDATETIME(),
    DienBienBenh NVARCHAR(1000) NOT NULL,
    PhanUngPhu NVARCHAR(1000) NULL,
    HieuQuaDieuTri NVARCHAR(1000) NULL,
    GhiChu NVARCHAR(1000) NULL,
    CONSTRAINT FK_TheoDoi_PhieuKham FOREIGN KEY (MaPhieuKham) REFERENCES dbo.PhieuKhamBenh(MaPhieuKham),
    CONSTRAINT FK_TheoDoi_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES dbo.NhanVienCSKH(MaNhanVien)
);
GO
CREATE INDEX IX_PhieuKham_HoSo_Ngay ON dbo.PhieuKhamBenh(MaHoSo, NgayGioKham DESC);
CREATE INDEX IX_PhieuThu_HieuLuc ON dbo.PhieuThuLePhi(MaBenhNhan, NgayBatDauHieuLuc, NgayHetHan);
CREATE INDEX IX_PhieuXN_PhieuKham ON dbo.PhieuXetNghiem(MaPhieuKham);
CREATE INDEX IX_TheoDoi_PhieuKham ON dbo.TheoDoiDieuTri(MaPhieuKham);
GO
PRINT N'01_Schema: da tao 16 bang, khoa PK/FK, CHECK, UNIQUE, chi muc.';
