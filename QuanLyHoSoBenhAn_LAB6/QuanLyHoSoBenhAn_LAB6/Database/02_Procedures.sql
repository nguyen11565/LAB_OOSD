/* Required SET options for indexed persisted computed columns and filtered indexes. */
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;
GO
/* Business rules centralized at SQL transaction boundaries. */
USE QuanLyHoSoBenhAn_LAB6;
GO
CREATE OR ALTER PROCEDURE dbo.usp_DangKyLanDau
 @HoTen NVARCHAR(120), @DiaChi NVARCHAR(250), @NgaySinh DATE,
 @SoDienThoai VARCHAR(15), @Anh3x4 NVARCHAR(300) = NULL,
 @SoTien DECIMAL(18,2), @MaBenhNhan INT OUTPUT, @MaHoSo INT OUTPUT
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF NULLIF(LTRIM(RTRIM(@HoTen)),N'') IS NULL OR NULLIF(LTRIM(RTRIM(@DiaChi)),N'') IS NULL
    THROW 51001, N'Họ tên và địa chỉ không được để trống.', 1;
 IF @NgaySinh >= CAST(SYSDATETIME() AS DATE) THROW 51002, N'Ngày sinh không hợp lệ.', 1;
 IF LEN(ISNULL(@SoDienThoai,'')) NOT BETWEEN 9 AND 15 OR @SoDienThoai LIKE '%[^0-9]%'
    THROW 51003, N'Số điện thoại phải có 9-15 chữ số.', 1;
 IF @SoTien IS NULL OR @SoTien <= 0 THROW 51004, N'Lệ phí năm phải lớn hơn 0.', 1;
 BEGIN TRY
   BEGIN TRAN;
   IF EXISTS (SELECT 1 FROM dbo.BenhNhan WITH (UPDLOCK, HOLDLOCK)
              WHERE HoTen=@HoTen AND NgaySinh=@NgaySinh AND SoDienThoai=@SoDienThoai)
      THROW 51005, N'Bệnh nhân đã có hồ sơ theo họ tên, ngày sinh và số điện thoại.', 1;
   INSERT dbo.BenhNhan(HoTen,DiaChi,NgaySinh,SoDienThoai,Anh3x4)
      VALUES (@HoTen,@DiaChi,@NgaySinh,@SoDienThoai,@Anh3x4);
   SET @MaBenhNhan = CONVERT(INT,SCOPE_IDENTITY());
   INSERT dbo.PhieuDangKy(MaBenhNhan,HoTen,DiaChi,NgaySinh,SoDienThoai,Anh3x4)
      VALUES (@MaBenhNhan,@HoTen,@DiaChi,@NgaySinh,@SoDienThoai,@Anh3x4);
   DECLARE @MaPhieuDangKy INT=CONVERT(INT,SCOPE_IDENTITY()), @NgayBatDau DATE=CAST(SYSDATETIME() AS DATE);
   INSERT dbo.PhieuThuLePhi(MaBenhNhan,SoTien,NgayBatDauHieuLuc,NgayHetHan)
      VALUES (@MaBenhNhan,@SoTien,@NgayBatDau,DATEADD(YEAR,1,@NgayBatDau));
   INSERT dbo.HoSoBenhAn(MaBenhNhan,MaPhieuDangKy)
      VALUES (@MaBenhNhan,@MaPhieuDangKy);
   SET @MaHoSo=CONVERT(INT,SCOPE_IDENTITY());
   COMMIT;
 END TRY BEGIN CATCH
   IF @@TRANCOUNT>0 ROLLBACK;
   THROW;
 END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GiaHanLePhi
 @MaHoSo INT, @SoTien DECIMAL(18,2), @MaPhieuThu INT OUTPUT
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF @SoTien IS NULL OR @SoTien<=0 THROW 51006, N'Lệ phí năm phải lớn hơn 0.',1;
 BEGIN TRY
  BEGIN TRAN;
  DECLARE @MaBenhNhan INT, @BatDau DATE, @NgayCuoi DATE;
  SELECT @MaBenhNhan=MaBenhNhan FROM dbo.HoSoBenhAn WITH (UPDLOCK,HOLDLOCK) WHERE MaHoSo=@MaHoSo;
  IF @MaBenhNhan IS NULL THROW 51007, N'Không tìm thấy mã hồ sơ.',1;
  SELECT @NgayCuoi=MAX(NgayHetHan) FROM dbo.PhieuThuLePhi WITH (UPDLOCK,HOLDLOCK) WHERE MaBenhNhan=@MaBenhNhan;
  SET @BatDau=CASE WHEN @NgayCuoi>CAST(SYSDATETIME() AS DATE) THEN @NgayCuoi ELSE CAST(SYSDATETIME() AS DATE) END;
  INSERT dbo.PhieuThuLePhi(MaBenhNhan,SoTien,NgayBatDauHieuLuc,NgayHetHan)
     VALUES(@MaBenhNhan,@SoTien,@BatDau,DATEADD(YEAR,1,@BatDau));
  SET @MaPhieuThu=CONVERT(INT,SCOPE_IDENTITY());
  COMMIT;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_DatLichHen
 @MaHoSo INT, @MaBacSi INT, @MaNhanVien INT=NULL,
 @NgayGioHen DATETIME2(0), @LoaiHen VARCHAR(20), @NoiDungHen NVARCHAR(300)=NULL,
 @MaLichHen INT OUTPUT
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF @LoaiHen NOT IN ('KHAM_BENH','DINH_KY') THROW 51008, N'Loại hẹn không hợp lệ.',1;
 IF @NgayGioHen<=SYSDATETIME() THROW 51009, N'Lịch hẹn phải ở tương lai.',1;
 BEGIN TRY
  SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
  BEGIN TRAN;
  DECLARE @MaBenhNhan INT;
  SELECT @MaBenhNhan=MaBenhNhan FROM dbo.HoSoBenhAn WHERE MaHoSo=@MaHoSo;
  IF @MaBenhNhan IS NULL THROW 51010, N'Hồ sơ không tồn tại.',1;
  IF NOT EXISTS (SELECT 1 FROM dbo.BacSi WHERE MaBacSi=@MaBacSi) THROW 51011, N'Bác sĩ không tồn tại.',1;
  IF @LoaiHen='DINH_KY'
  BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.PhieuThuLePhi WHERE MaBenhNhan=@MaBenhNhan
           AND NgayBatDauHieuLuc<=CAST(@NgayGioHen AS DATE) AND NgayHetHan>CAST(@NgayGioHen AS DATE))
       THROW 51012, N'Lệ phí năm chưa có hiệu lực tại ngày khám định kỳ.',1;
    IF EXISTS (SELECT 1 FROM dbo.PhieuKhamBenh WHERE MaHoSo=@MaHoSo AND LoaiKham='DINH_KY'
          AND YEAR(NgayGioKham)=YEAR(@NgayGioHen)
          AND DATEPART(QUARTER,NgayGioKham)=DATEPART(QUARTER,@NgayGioHen))
       THROW 51013, N'Quý này đã được khám định kỳ.',1;
  END;
  INSERT dbo.LichHen(MaHoSo,MaBacSi,MaNhanVien,NgayGioHen,LoaiHen,NoiDungHen)
    VALUES(@MaHoSo,@MaBacSi,@MaNhanVien,@NgayGioHen,@LoaiHen,@NoiDungHen);
  SET @MaLichHen=CONVERT(INT,SCOPE_IDENTITY());
  COMMIT;
  SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
 END TRY BEGIN CATCH
  IF @@TRANCOUNT>0 ROLLBACK;
  SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
  THROW;
 END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_HuyLichHen @MaLichHen INT
AS
BEGIN
 SET NOCOUNT ON;
 UPDATE dbo.LichHen SET TrangThai='DA_HUY' WHERE MaLichHen=@MaLichHen AND TrangThai='CHO_KHAM';
 IF @@ROWCOUNT<>1 THROW 51014, N'Chỉ hủy được lịch đang chờ khám.',1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_MoPhieuKham
 @MaHoSo INT, @MaBacSi INT, @MaKhoa INT,
 @LoaiKham VARCHAR(20), @MaLichHen INT=NULL, @MaPhieuKham INT OUTPUT
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF @LoaiKham NOT IN ('KHAM_BENH','DINH_KY') THROW 51015, N'Loại khám không hợp lệ.',1;
 BEGIN TRY
  BEGIN TRAN;
  DECLARE @MaBenhNhan INT, @LoaiHen VARCHAR(20), @TrangThaiHen VARCHAR(20),
          @NgayHen DATETIME2(0), @MaHoSoHen INT, @MaBacSiHen INT;
  SELECT @MaBenhNhan=MaBenhNhan FROM dbo.HoSoBenhAn WITH (UPDLOCK,HOLDLOCK) WHERE MaHoSo=@MaHoSo;
  IF @MaBenhNhan IS NULL THROW 51016, N'Hồ sơ không tồn tại.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.BacSi WHERE MaBacSi=@MaBacSi AND MaKhoa=@MaKhoa)
      THROW 51017, N'Bác sĩ không thuộc khoa được chọn.',1;
  IF @MaLichHen IS NOT NULL
  BEGIN
   SELECT @MaHoSoHen=MaHoSo,@MaBacSiHen=MaBacSi,@LoaiHen=LoaiHen,
          @TrangThaiHen=TrangThai,@NgayHen=NgayGioHen
    FROM dbo.LichHen WITH (UPDLOCK,HOLDLOCK) WHERE MaLichHen=@MaLichHen;
   IF @MaHoSoHen IS NULL OR @MaHoSoHen<>@MaHoSo OR @MaBacSiHen<>@MaBacSi
       OR @LoaiHen<>@LoaiKham OR @TrangThaiHen<>'CHO_KHAM'
      THROW 51018, N'Lịch hẹn không khớp hồ sơ, bác sĩ, loại khám hoặc trạng thái.',1;
   IF CAST(@NgayHen AS DATE)<>CAST(SYSDATETIME() AS DATE)
      THROW 51019, N'Chỉ tiếp nhận lịch hẹn đúng ngày đã đặt.',1;
  END;
  IF @LoaiKham='DINH_KY'
  BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.PhieuThuLePhi WHERE MaBenhNhan=@MaBenhNhan
         AND NgayBatDauHieuLuc<=CAST(SYSDATETIME() AS DATE) AND NgayHetHan>CAST(SYSDATETIME() AS DATE))
      THROW 51020, N'Lệ phí năm đã hết hạn; không được khám định kỳ miễn phí.',1;
  END;
  INSERT dbo.PhieuKhamBenh(MaHoSo,MaBacSi,MaKhoa,MaLichHen,LoaiKham)
     VALUES(@MaHoSo,@MaBacSi,@MaKhoa,@MaLichHen,@LoaiKham);
  SET @MaPhieuKham=CONVERT(INT,SCOPE_IDENTITY());
  IF @MaLichHen IS NOT NULL
     UPDATE dbo.LichHen SET TrangThai='DANG_KHAM' WHERE MaLichHen=@MaLichHen;
  COMMIT;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_CapNhatKham
 @MaPhieuKham INT, @TrieuChung NVARCHAR(1000)=NULL, @ThanNhiet DECIMAL(4,1)=NULL,
 @HuyetAp NVARCHAR(30)=NULL, @ChanDoan NVARCHAR(1000)=NULL, @KetLuan NVARCHAR(1000)=NULL
AS
BEGIN
 SET NOCOUNT ON;
 UPDATE dbo.PhieuKhamBenh
 SET TrieuChung=@TrieuChung,ThanNhiet=@ThanNhiet,HuyetAp=@HuyetAp,
     ChanDoan=@ChanDoan,KetLuan=@KetLuan
 WHERE MaPhieuKham=@MaPhieuKham AND TrangThai='DANG_KHAM'
   AND NOT EXISTS(SELECT 1 FROM dbo.HoaDonVienPhi WHERE MaPhieuKham=@MaPhieuKham);
 IF @@ROWCOUNT<>1 THROW 51021, N'Không thể cập nhật phiếu đã lập hóa đơn/hoàn tất.',1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ChiDinhXetNghiem
 @MaPhieuKham INT, @MaLoaiXetNghiem INT, @MaPhieuXetNghiem INT OUTPUT
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 BEGIN TRY
  BEGIN TRAN;
  IF NOT EXISTS (SELECT 1 FROM dbo.PhieuKhamBenh WITH (UPDLOCK,HOLDLOCK)
      WHERE MaPhieuKham=@MaPhieuKham AND TrangThai='DANG_KHAM')
     THROW 51022, N'Phiếu khám không tồn tại hoặc đã hoàn tất.',1;
  IF EXISTS(SELECT 1 FROM dbo.HoaDonVienPhi WHERE MaPhieuKham=@MaPhieuKham)
     THROW 51023, N'Không được chỉ định thêm sau khi lập hóa đơn.',1;
  DECLARE @DonGia DECIMAL(18,2);
  SELECT @DonGia=DonGia FROM dbo.LoaiXetNghiem WHERE MaLoaiXetNghiem=@MaLoaiXetNghiem;
  IF @DonGia IS NULL THROW 51024, N'Loại xét nghiệm không tồn tại.',1;
  INSERT dbo.PhieuXetNghiem(MaPhieuKham,MaLoaiXetNghiem,DonGiaApDung)
     VALUES(@MaPhieuKham,@MaLoaiXetNghiem,@DonGia);
  SET @MaPhieuXetNghiem=CONVERT(INT,SCOPE_IDENTITY());
  COMMIT;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GhiKetQuaXetNghiem
 @MaPhieuXetNghiem INT, @KetQua NVARCHAR(2000), @KetLuan NVARCHAR(1000)=NULL
AS
BEGIN
 SET NOCOUNT ON;
 IF NULLIF(LTRIM(RTRIM(@KetQua)),N'') IS NULL THROW 51025, N'Kết quả không được trống.',1;
 UPDATE x SET KetQua=@KetQua,KetLuan=@KetLuan,NgayThucHien=SYSDATETIME(),TrangThai='DA_CO_KET_QUA'
 FROM dbo.PhieuXetNghiem x INNER JOIN dbo.PhieuKhamBenh k ON k.MaPhieuKham=x.MaPhieuKham
 WHERE x.MaPhieuXetNghiem=@MaPhieuXetNghiem AND x.TrangThai='CHO_KET_QUA' AND k.TrangThai='DANG_KHAM'
 AND NOT EXISTS(SELECT 1 FROM dbo.HoaDonVienPhi WHERE MaPhieuKham=x.MaPhieuKham);
 IF @@ROWCOUNT<>1 THROW 51026, N'Phiếu xét nghiệm không hợp lệ hoặc đã khóa.',1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_TaoToaThuoc
 @MaPhieuKham INT, @LoiDan NVARCHAR(1000)=NULL, @MaToaThuoc INT OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 IF NOT EXISTS (SELECT 1 FROM dbo.PhieuKhamBenh WHERE MaPhieuKham=@MaPhieuKham AND TrangThai='DANG_KHAM')
  THROW 51027, N'Phiếu khám không hợp lệ.',1;
 IF EXISTS (SELECT 1 FROM dbo.HoaDonVienPhi WHERE MaPhieuKham=@MaPhieuKham)
  THROW 51028, N'Phiếu khám đã lập hóa đơn.',1;
 INSERT dbo.ToaThuoc(MaPhieuKham,LoiDan) VALUES(@MaPhieuKham,@LoiDan);
 SET @MaToaThuoc=CONVERT(INT,SCOPE_IDENTITY());
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ThemChiTietToa
 @MaToaThuoc INT, @MaThuoc INT, @SoLuong INT,
 @LieuDung NVARCHAR(120), @SoLanDungTrongNgay INT, @CachDung NVARCHAR(200), @SoNgayDung INT
AS
BEGIN
 SET NOCOUNT ON;
 IF @SoLuong<=0 OR @SoLanDungTrongNgay<=0 OR @SoNgayDung<=0
     THROW 51029, N'Số lượng, số lần dùng, số ngày dùng phải > 0.',1;
 IF NULLIF(LTRIM(RTRIM(@LieuDung)),N'') IS NULL OR NULLIF(LTRIM(RTRIM(@CachDung)),N'') IS NULL
     THROW 51030, N'Liều dùng và cách dùng không được trống.',1;
 IF NOT EXISTS (SELECT 1 FROM dbo.ToaThuoc t JOIN dbo.PhieuKhamBenh k ON k.MaPhieuKham=t.MaPhieuKham
    WHERE t.MaToaThuoc=@MaToaThuoc AND k.TrangThai='DANG_KHAM'
    AND NOT EXISTS(SELECT 1 FROM dbo.HoaDonVienPhi WHERE MaPhieuKham=k.MaPhieuKham))
    THROW 51031, N'Toa không tồn tại hoặc đã khóa.',1;
 INSERT dbo.ChiTietToaThuoc(MaToaThuoc,MaThuoc,SoLuong,LieuDung,SoLanDungTrongNgay,CachDung,SoNgayDung)
  VALUES(@MaToaThuoc,@MaThuoc,@SoLuong,@LieuDung,@SoLanDungTrongNgay,@CachDung,@SoNgayDung);
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_LapHoaDon
 @MaPhieuKham INT, @MaHoaDon INT OUTPUT
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 BEGIN TRY
  BEGIN TRAN;
  DECLARE @LoaiKham VARCHAR(20), @TienKham DECIMAL(18,2), @TienXetNghiem DECIMAL(18,2);
  SELECT @LoaiKham=LoaiKham FROM dbo.PhieuKhamBenh WITH (UPDLOCK,HOLDLOCK)
    WHERE MaPhieuKham=@MaPhieuKham AND TrangThai='DANG_KHAM';
  IF @LoaiKham IS NULL THROW 51032, N'Phiếu khám không tồn tại hoặc đã hoàn tất.',1;
  IF EXISTS(SELECT 1 FROM dbo.PhieuXetNghiem WHERE MaPhieuKham=@MaPhieuKham AND TrangThai<>'DA_CO_KET_QUA')
    THROW 51033, N'Phải có đầy đủ kết quả xét nghiệm trước khi lập hóa đơn.',1;
  IF EXISTS(SELECT 1 FROM dbo.ToaThuoc t WHERE t.MaPhieuKham=@MaPhieuKham
    AND NOT EXISTS(SELECT 1 FROM dbo.ChiTietToaThuoc c WHERE c.MaToaThuoc=t.MaToaThuoc))
    THROW 51034, N'Toa thuốc chưa có thuốc.',1;
  SET @TienKham=CASE WHEN @LoaiKham='DINH_KY' THEN 0 ELSE 150000 END;
  SELECT @TienXetNghiem=ISNULL(SUM(DonGiaApDung),0)
     FROM dbo.PhieuXetNghiem WHERE MaPhieuKham=@MaPhieuKham;
  INSERT dbo.HoaDonVienPhi(MaPhieuKham,TienKham,TienXetNghiem,TongTien,GhiChu)
     VALUES(@MaPhieuKham,@TienKham,@TienXetNghiem,@TienKham+@TienXetNghiem,
            CASE WHEN @LoaiKham='DINH_KY' THEN N'Miễn phí tiền khám định kỳ theo quý; xét nghiệm (nếu có) thu riêng.' ELSE NULL END);
  SET @MaHoaDon=CONVERT(INT,SCOPE_IDENTITY());
  COMMIT;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_XacNhanThanhToan
 @MaHoaDon INT, @SoTienNhan DECIMAL(18,2)
AS
BEGIN
 SET NOCOUNT ON;
 DECLARE @TongTien DECIMAL(18,2);
 SELECT @TongTien=TongTien FROM dbo.HoaDonVienPhi WHERE MaHoaDon=@MaHoaDon AND TrangThaiThanhToan='CHUA_THANH_TOAN';
 IF @TongTien IS NULL THROW 51035, N'Hóa đơn không tồn tại hoặc đã được thanh toán.',1;
 IF @SoTienNhan IS NULL OR @SoTienNhan<@TongTien
    THROW 51036, N'Số tiền nhận nhỏ hơn tổng viện phí.',1;
 UPDATE dbo.HoaDonVienPhi SET SoTienNhan=@SoTienNhan,NgayThanhToan=SYSDATETIME(),TrangThaiThanhToan='DA_THANH_TOAN'
  WHERE MaHoaDon=@MaHoaDon AND TrangThaiThanhToan='CHUA_THANH_TOAN';
 IF @@ROWCOUNT<>1 THROW 51037, N'Hóa đơn vừa được thanh toán từ phiên khác.',1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_HoanTatKham @MaPhieuKham INT
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 BEGIN TRY
   BEGIN TRAN;
   IF NOT EXISTS(SELECT 1 FROM dbo.HoaDonVienPhi WITH (UPDLOCK,HOLDLOCK)
     WHERE MaPhieuKham=@MaPhieuKham AND TrangThaiThanhToan='DA_THANH_TOAN')
      THROW 51038, N'Chưa thanh toán viện phí (hóa đơn 0 đồng cũng phải xác nhận).',1;
   IF EXISTS(SELECT 1 FROM dbo.PhieuXetNghiem
      WHERE MaPhieuKham=@MaPhieuKham AND TrangThai<>'DA_CO_KET_QUA')
      THROW 51039, N'Chưa có đầy đủ kết quả xét nghiệm.',1;
   UPDATE dbo.PhieuKhamBenh SET TrangThai='HOAN_TAT',NgayHoanTat=SYSDATETIME()
      WHERE MaPhieuKham=@MaPhieuKham AND TrangThai='DANG_KHAM';
   IF @@ROWCOUNT<>1 THROW 51040, N'Phiếu khám không tồn tại hoặc đã hoàn tất.',1;
   UPDATE h SET h.TrangThai='DA_KHAM' FROM dbo.LichHen h
      JOIN dbo.PhieuKhamBenh k ON k.MaLichHen=h.MaLichHen WHERE k.MaPhieuKham=@MaPhieuKham;
   COMMIT;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GhiNhanTheoDoi
 @MaPhieuKham INT, @MaNhanVien INT, @DienBienBenh NVARCHAR(1000),
 @PhanUngPhu NVARCHAR(1000)=NULL, @HieuQuaDieuTri NVARCHAR(1000)=NULL, @GhiChu NVARCHAR(1000)=NULL
AS
BEGIN
 SET NOCOUNT ON;
 IF NULLIF(LTRIM(RTRIM(@DienBienBenh)),N'') IS NULL THROW 51041, N'Diễn biến không được trống.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.PhieuKhamBenh WHERE MaPhieuKham=@MaPhieuKham AND TrangThai='HOAN_TAT')
   THROW 51042, N'Chỉ ghi nhận theo dõi sau khi hoàn tất và thanh toán.',1;
 INSERT dbo.TheoDoiDieuTri(MaPhieuKham,MaNhanVien,DienBienBenh,PhanUngPhu,HieuQuaDieuTri,GhiChu)
 VALUES(@MaPhieuKham,@MaNhanVien,@DienBienBenh,@PhanUngPhu,@HieuQuaDieuTri,@GhiChu);
END;
GO
PRINT N'02_Procedures: đã tạo 14 stored procedures nghiệp vụ.';
