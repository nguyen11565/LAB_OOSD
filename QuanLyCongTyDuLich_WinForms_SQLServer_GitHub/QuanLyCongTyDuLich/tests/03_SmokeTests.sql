/* Automated DB checks: run AFTER 01_Schema.sql + 02_Seed.sql; scripts never persist modifications.
   SQL Server Management Studio: Ctrl + E. A THROW marks failure. */
USE QuanLyCongTyDuLich;
GO
SET NOCOUNT ON;
IF (SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID(N'dbo')) <> 16
 THROW 51000, N'TC-SQL01 FAILED: Expected 16 tables.', 1;
PRINT N'TC-SQL01 PASS: 16 tables';
IF (SELECT COUNT(*) FROM dbo.TourDuLich)<>3 OR (SELECT COUNT(*) FROM dbo.KhachHang)<>3
 THROW 51001, N'TC-SQL02 FAILED: Missing seed records.', 1;
PRINT N'TC-SQL02 PASS: seed records';
IF EXISTS (SELECT 1 FROM dbo.ChuyenKhachLe c OUTER APPLY (
 SELECT COALESCE(SUM(SoNguoi),0) AS People FROM dbo.VeKhachLe v
 WHERE v.MaChuyen=c.MaChuyen AND v.TrangThai=N'DaThanhToan') v
 WHERE c.SoLuongHienTai <> v.People)
 THROW 51002, N'TC-SQL03 FAILED: Occupancy inconsistent with active tickets.', 1;
PRINT N'TC-SQL03 PASS: occupancy equals active ticket passengers';
IF (SELECT TongTienThanhToan FROM dbo.VeKhachLe WHERE MaVe=N'VE001')<>5000000
 THROW 51003, N'TC-SQL04 FAILED: Ticket amount.', 1;
IF (SELECT SoNguoiDi*DonGiaApDung-TienCoc FROM dbo.PhieuDangKyDoan WHERE MaPhieuDoan=N'PD001')<>22000000
 THROW 51004, N'TC-SQL04 FAILED: Group deposit/balance.', 1;
PRINT N'TC-SQL04 PASS: price snapshots and deposits';
;WITH S AS (
 SELECT p.MaPhanCong,p.MaNhanVien,
   COALESCE(c.NgayDi,d.NgayDiYeuCau) AS StartDate,
   COALESCE(c.NgayVe,DATEADD(day,t.SoNgay-1,d.NgayDiYeuCau)) AS EndDate
 FROM dbo.PhanCongHuongDan p
 LEFT JOIN dbo.ChuyenKhachLe c ON c.MaChuyen=p.MaChuyen
 LEFT JOIN dbo.PhieuDangKyDoan d ON d.MaPhieuDoan=p.MaPhieuDoan
 LEFT JOIN dbo.TourDuLich t ON t.MaTour=d.MaTour
)
SELECT * INTO #Schedules FROM S;
IF EXISTS(SELECT 1 FROM #Schedules a JOIN #Schedules b ON a.MaNhanVien=b.MaNhanVien AND a.MaPhanCong<b.MaPhanCong
 WHERE a.StartDate<=b.EndDate AND a.EndDate>=b.StartDate)
 THROW 51005, N'TC-SQL05 FAILED: Employee overlapping assignments.', 1;
DROP TABLE #Schedules;
PRINT N'TC-SQL05 PASS: no overlapping guide shifts';
IF (SELECT n.LuongCoBan+COALESCE(SUM(CASE WHEN
 (c.TrangThai=N'HoanThanh' AND c.NgayDi>='20260701' AND c.NgayDi<'20260801') OR
 (d.TrangThai IN(N'DaHoanThanh',N'DaThanhToan') AND d.NgayDiYeuCau>='20260701' AND d.NgayDiYeuCau<'20260801')
 THEN p.TienCong ELSE 0 END),0)
 FROM dbo.NhanVienHD n LEFT JOIN dbo.PhanCongHuongDan p ON p.MaNhanVien=n.MaNhanVien
 LEFT JOIN dbo.ChuyenKhachLe c ON c.MaChuyen=p.MaChuyen
 LEFT JOIN dbo.PhieuDangKyDoan d ON d.MaPhieuDoan=p.MaPhieuDoan
 WHERE n.MaNhanVien=N'NV002' GROUP BY n.LuongCoBan) <> 10000000
 THROW 51006, N'TC-SQL06 FAILED: NV002 July payroll.', 1;
PRINT N'TC-SQL06 PASS: July payroll NV002 = 10,000,000';
IF EXISTS(SELECT 1 FROM dbo.PhieuDangKyDoan d WHERE d.CoBaoHiem=1 AND
 (SELECT COUNT(*) FROM dbo.DanhSachBaoHiem bh WHERE bh.MaPhieuDoan=d.MaPhieuDoan)<>d.SoNguoiDi)
 THROW 51007, N'TC-SQL07 FAILED: Group insurance headcount mismatch.', 1;
PRINT N'TC-SQL07 PASS: insurance completeness';
-- Negative validation: deliberately attempt to breach capacity. Constraint must reject this.
DECLARE @rejected BIT=0;
BEGIN TRY
 BEGIN TRAN;
 UPDATE dbo.ChuyenKhachLe SET SoLuongHienTai=SucChua+1 WHERE MaChuyen=N'CH001';
 ROLLBACK;
END TRY
BEGIN CATCH
 SET @rejected=1;
 IF XACT_STATE()<>0 ROLLBACK;
END CATCH;
IF @rejected=0 THROW 51008, N'TC-SQL08 FAILED: Overselling must be rejected.', 1;
PRINT N'TC-SQL08 PASS: DB rejects capacity overflow';
PRINT N'ALL SQL SMOKE TESTS PASSED.';
GO
