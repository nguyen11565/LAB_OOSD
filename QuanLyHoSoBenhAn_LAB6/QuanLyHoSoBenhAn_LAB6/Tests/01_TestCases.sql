/* LAB6 automated integration checks (execute in SSMS AFTER 01, 02, 03).
   TEST DATABASE ONLY. This script intentionally WRITES sample patients and clinical data.
   It does not delete them; each execution uses a unique telephone number.
   PASS = an assertion ran without throwing; FAIL = execution stops with an exception.
*/
USE QuanLyHoSoBenhAn_LAB6;
GO
SET NOCOUNT ON;
DECLARE @phone VARCHAR(15)='09'+RIGHT('00000000'+CONVERT(VARCHAR(8),ABS(CHECKSUM(NEWID()))%100000000),8);
DECLARE @phone2 VARCHAR(15)='08'+RIGHT('00000000'+CONVERT(VARCHAR(8),ABS(CHECKSUM(NEWID()))%100000000),8);
DECLARE @bn INT,@hs INT,@bn2 INT,@hs2 INT,@fee INT,@lh INT,@lh2 INT;
DECLARE @visitFree INT,@visitPaid INT,@lab INT,@rx INT,@bill INT,@zeroBill INT,@book INT;
DECLARE @bs INT=(SELECT TOP(1) MaBacSi FROM dbo.BacSi ORDER BY MaBacSi);
DECLARE @khoa INT=(SELECT MaKhoa FROM dbo.BacSi WHERE MaBacSi=@bs);
DECLARE @bsOther INT=(SELECT TOP(1) MaBacSi FROM dbo.BacSi WHERE MaBacSi<>@bs ORDER BY MaBacSi);
DECLARE @khoaOther INT=(SELECT MaKhoa FROM dbo.BacSi WHERE MaBacSi=@bsOther);
DECLARE @loai INT=(SELECT TOP(1) MaLoaiXetNghiem FROM dbo.LoaiXetNghiem ORDER BY MaLoaiXetNghiem);
DECLARE @thuoc INT=(SELECT TOP(1) MaThuoc FROM dbo.Thuoc ORDER BY MaThuoc);
DECLARE @nv INT=(SELECT TOP(1) MaNhanVien FROM dbo.NhanVienCSKH ORDER BY MaNhanVien);
DECLARE @ngay DATE=CAST(SYSDATETIME() AS DATE), @date DATETIME2(0)=DATEADD(DAY,3,SYSDATETIME());
DECLARE @blocked BIT, @oldExpiry DATE,@newStart DATE,@total DECIMAL(18,2),@expected DECIMAL(18,2);
IF @bs IS NULL OR @bsOther IS NULL OR @loai IS NULL OR @thuoc IS NULL OR @nv IS NULL
 THROW 52000,N'Chưa chạy Database/03_Seed.sql.',1;
PRINT N'== LAB6 TESTS - begin (data is persisted in TEST database) ==';

-- TC01: First-time registration + annual fee + dossier in a single transaction.
EXEC dbo.usp_DangKyLanDau @HoTen=N'Nguyễn Văn Kiểm Thử',@DiaChi=N'123 Đường Kiểm Thử',
 @NgaySinh='1998-05-22',@SoDienThoai=@phone,@Anh3x4=N'anhtest.jpg',
 @SoTien=500000,@MaBenhNhan=@bn OUTPUT,@MaHoSo=@hs OUTPUT;
IF NOT EXISTS(SELECT 1 FROM dbo.PhieuDangKy WHERE MaBenhNhan=@bn)
 OR NOT EXISTS(SELECT 1 FROM dbo.HoSoBenhAn WHERE MaHoSo=@hs AND MaBenhNhan=@bn)
 OR NOT EXISTS(SELECT 1 FROM dbo.PhieuThuLePhi WHERE MaBenhNhan=@bn AND SoTien=500000 AND NgayBatDauHieuLuc=@ngay
   AND NgayHetHan=DATEADD(YEAR,1,@ngay))
 THROW 52001,N'TC01 FAIL: đăng ký / thu phí / cấp hồ sơ.',1;
PRINT N'TC01 PASS - Đăng ký lần đầu + lệ phí 500,000 VND + cấp mã hồ sơ';

-- TC02: Duplicated registration rejected.
SET @blocked=0;
BEGIN TRY
 EXEC dbo.usp_DangKyLanDau @HoTen=N'Nguyễn Văn Kiểm Thử',@DiaChi=N'123 Đường Kiểm Thử',
   @NgaySinh='1998-05-22',@SoDienThoai=@phone,@SoTien=500000,
   @MaBenhNhan=@bn2 OUTPUT,@MaHoSo=@hs2 OUTPUT;
END TRY BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52002,N'TC02 FAIL: trùng bệnh nhân không bị chặn.',1;
PRINT N'TC02 PASS - Chống đăng ký trùng bệnh nhân';

-- TC03: Book quarterly, prevent duplicate, cancel, reschedule.
EXEC dbo.usp_DatLichHen @MaHoSo=@hs,@MaBacSi=@bs,@MaNhanVien=@nv,
 @NgayGioHen=@date,@LoaiHen='DINH_KY',@NoiDungHen=N'Khám định kỳ',@MaLichHen=@lh OUTPUT;
SET @blocked=0;
BEGIN TRY
 EXEC dbo.usp_DatLichHen @MaHoSo=@hs,@MaBacSi=@bsOther,@MaNhanVien=@nv,
 @NgayGioHen=@date,@LoaiHen='DINH_KY',@MaLichHen=@lh2 OUTPUT;
END TRY BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52003,N'TC03 FAIL: cho đặt 2 lịch định kỳ cùng quý.',1;
EXEC dbo.usp_HuyLichHen @MaLichHen=@lh;
EXEC dbo.usp_DatLichHen @MaHoSo=@hs,@MaBacSi=@bs,@MaNhanVien=@nv,
 @NgayGioHen=@date,@LoaiHen='DINH_KY',@MaLichHen=@lh2 OUTPUT;
IF NOT EXISTS(SELECT 1 FROM dbo.LichHen WHERE MaLichHen=@lh2 AND TrangThai='CHO_KHAM')
 THROW 52004,N'TC03 FAIL: không thể đặt lại sau hủy.',1;
PRINT N'TC03 PASS - 1 lịch định kỳ/quý; hủy cho phép đặt lại';

-- TC04: Quarter visit has no examination fee; zero invoice must be settled before close.
EXEC dbo.usp_MoPhieuKham @MaHoSo=@hs,@MaBacSi=@bs,@MaKhoa=@khoa,
 @LoaiKham='DINH_KY',@MaPhieuKham=@visitFree OUTPUT;
EXEC dbo.usp_CapNhatKham @MaPhieuKham=@visitFree,@TrieuChung=N'Khám sức khỏe định kỳ',
 @ThanNhiet=36.8,@HuyetAp=N'120/80',@ChanDoan=N'Ổn định',@KetLuan=N'Theo dõi quý sau';
EXEC dbo.usp_LapHoaDon @MaPhieuKham=@visitFree,@MaHoaDon=@zeroBill OUTPUT;
IF NOT EXISTS(SELECT 1 FROM dbo.HoaDonVienPhi WHERE MaHoaDon=@zeroBill AND TienKham=0 AND TongTien=0)
 THROW 52005,N'TC04 FAIL: phí khám định kỳ không phải 0 đồng.',1;
SET @blocked=0;
BEGIN TRY EXEC dbo.usp_HoanTatKham @MaPhieuKham=@visitFree; END TRY
BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52006,N'TC04 FAIL: cho hoàn tất trước xác nhận 0 đồng.',1;
EXEC dbo.usp_XacNhanThanhToan @MaHoaDon=@zeroBill,@SoTienNhan=0;
EXEC dbo.usp_HoanTatKham @MaPhieuKham=@visitFree;
IF NOT EXISTS(SELECT 1 FROM dbo.PhieuKhamBenh WHERE MaPhieuKham=@visitFree AND TrangThai='HOAN_TAT')
 THROW 52007,N'TC04 FAIL: không hoàn tất phiếu 0 đồng đã xác nhận.',1;
PRINT N'TC04 PASS - Miễn tiền khám định kỳ, hóa đơn 0 đồng, yêu cầu xác nhận trước hoàn tất';

-- TC05: A second quarterly visit in same quarter must fail.
SET @blocked=0;
BEGIN TRY
 EXEC dbo.usp_MoPhieuKham @MaHoSo=@hs,@MaBacSi=@bs,@MaKhoa=@khoa,
 @LoaiKham='DINH_KY',@MaPhieuKham=@book OUTPUT;
END TRY BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52008,N'TC05 FAIL: khám định kỳ 2 lần cùng quý.',1;
PRINT N'TC05 PASS - Không mở 2 phiếu khám định kỳ trong cùng quý';

-- TC06: normal examination and medicine / lab must share the visit foreign key.
EXEC dbo.usp_MoPhieuKham @MaHoSo=@hs,@MaBacSi=@bs,@MaKhoa=@khoa,
 @LoaiKham='KHAM_BENH',@MaPhieuKham=@visitPaid OUTPUT;
EXEC dbo.usp_CapNhatKham @MaPhieuKham=@visitPaid,@TrieuChung=N'Đau đầu, sốt',
 @ThanNhiet=38.5,@HuyetAp=N'125/85',@ChanDoan=N'Nhiễm siêu vi',@KetLuan=N'Uống thuốc, tái khám';
EXEC dbo.usp_ChiDinhXetNghiem @MaPhieuKham=@visitPaid,@MaLoaiXetNghiem=@loai,@MaPhieuXetNghiem=@lab OUTPUT;
EXEC dbo.usp_TaoToaThuoc @MaPhieuKham=@visitPaid,@LoiDan=N'Uống đủ nước',@MaToaThuoc=@rx OUTPUT;
SET @blocked=0;
BEGIN TRY
 EXEC dbo.usp_ThemChiTietToa @MaToaThuoc=@rx,@MaThuoc=@thuoc,@SoLuong=0,
  @LieuDung=N'1 viên',@SoLanDungTrongNgay=2,@CachDung=N'Sau ăn',@SoNgayDung=5;
END TRY BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52009,N'TC06 FAIL: toa thuốc liều lượng sai được chấp nhận.',1;
EXEC dbo.usp_ThemChiTietToa @MaToaThuoc=@rx,@MaThuoc=@thuoc,@SoLuong=10,
 @LieuDung=N'1 viên',@SoLanDungTrongNgay=2,@CachDung=N'Sau ăn',@SoNgayDung=5;
IF NOT EXISTS(SELECT 1 FROM dbo.PhieuXetNghiem WHERE MaPhieuXetNghiem=@lab AND MaPhieuKham=@visitPaid)
 OR NOT EXISTS(SELECT 1 FROM dbo.ToaThuoc t JOIN dbo.ChiTietToaThuoc c ON c.MaToaThuoc=t.MaToaThuoc
    WHERE t.MaToaThuoc=@rx AND t.MaPhieuKham=@visitPaid)
 THROW 52010,N'TC06 FAIL: liên kết FK phiếu khám/lab/toa sai.',1;
PRINT N'TC06 PASS - Phiếu khám liên kết 1-N xét nghiệm, 1-1 toa và chi tiết thuốc hợp lệ';

-- TC07: can't issue final invoice with pending lab result.
SET @blocked=0;
BEGIN TRY EXEC dbo.usp_LapHoaDon @MaPhieuKham=@visitPaid,@MaHoaDon=@bill OUTPUT; END TRY
BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52011,N'TC07 FAIL: cho lập hóa đơn khi chưa có kết quả xét nghiệm.',1;
EXEC dbo.usp_GhiKetQuaXetNghiem @MaPhieuXetNghiem=@lab,@KetQua=N'Chỉ số trong giới hạn',@KetLuan=N'Theo dõi tại nhà';
EXEC dbo.usp_LapHoaDon @MaPhieuKham=@visitPaid,@MaHoaDon=@bill OUTPUT;
SELECT @total=TongTien FROM dbo.HoaDonVienPhi WHERE MaHoaDon=@bill;
SELECT @expected=150000+DonGia FROM dbo.LoaiXetNghiem WHERE MaLoaiXetNghiem=@loai;
IF @total<>@expected THROW 52012,N'TC07 FAIL: tổng viện phí sai.',1;
PRINT N'TC07 PASS - Chỉ lập hóa đơn sau xét nghiệm, tính 150,000 + đơn giá xét nghiệm';

-- TC08: underpay rejected, can't close before payment, success after payment.
SET @blocked=0;
BEGIN TRY EXEC dbo.usp_XacNhanThanhToan @MaHoaDon=@bill,@SoTienNhan=1000; END TRY
BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52013,N'TC08 FAIL: cho thanh toán thiếu.',1;
SET @blocked=0;
BEGIN TRY EXEC dbo.usp_HoanTatKham @MaPhieuKham=@visitPaid; END TRY
BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52014,N'TC08 FAIL: cho hoàn tất trước thanh toán.',1;
EXEC dbo.usp_XacNhanThanhToan @MaHoaDon=@bill,@SoTienNhan=@total;
EXEC dbo.usp_HoanTatKham @MaPhieuKham=@visitPaid;
EXEC dbo.usp_GhiNhanTheoDoi @MaPhieuKham=@visitPaid,@MaNhanVien=@nv,
 @DienBienBenh=N'Hết sốt sau 2 ngày',@PhanUngPhu=N'Không có',@HieuQuaDieuTri=N'Tốt';
IF NOT EXISTS(SELECT 1 FROM dbo.PhieuKhamBenh WHERE MaPhieuKham=@visitPaid AND TrangThai='HOAN_TAT')
 OR NOT EXISTS(SELECT 1 FROM dbo.TheoDoiDieuTri WHERE MaPhieuKham=@visitPaid)
 THROW 52015,N'TC08 FAIL: hoàn tất / theo dõi sau khám.',1;
PRINT N'TC08 PASS - Không trả thiếu, không hoàn tất trước thanh toán, ghi nhận điều trị sau khi xong';

-- TC09: annual fee renewal starts from current expiry, no loss of unused paid period.
SELECT @oldExpiry=MAX(NgayHetHan) FROM dbo.PhieuThuLePhi WHERE MaBenhNhan=@bn;
EXEC dbo.usp_GiaHanLePhi @MaHoSo=@hs,@SoTien=500000,@MaPhieuThu=@fee OUTPUT;
SELECT @newStart=NgayBatDauHieuLuc FROM dbo.PhieuThuLePhi WHERE MaPhieuThu=@fee;
IF @newStart<>@oldExpiry THROW 52016,N'TC09 FAIL: gia hạn làm mất ngày hiệu lực.',1;
PRINT N'TC09 PASS - Gia hạn cộng nối một năm từ ngày hết hạn cũ';

-- TC10: lookup history reflects two visits, lab, prescriptions and cleared invoices.
IF (SELECT COUNT(*) FROM dbo.PhieuKhamBenh WHERE MaHoSo=@hs)<2
 OR NOT EXISTS(SELECT 1 FROM dbo.HoaDonVienPhi WHERE MaPhieuKham=@visitPaid AND TrangThaiThanhToan='DA_THANH_TOAN')
 OR NOT EXISTS(SELECT 1 FROM dbo.PhieuXetNghiem WHERE MaPhieuKham=@visitPaid AND TrangThai='DA_CO_KET_QUA')
 THROW 52017,N'TC10 FAIL: không tra cứu được lịch sử thống nhất.',1;
PRINT N'TC10 PASS - Truy vết lịch sử khám + xét nghiệm + toa + hóa đơn';

-- TC11: expired annual coverage rejects quarterly examination; renewal restores right.
EXEC dbo.usp_DangKyLanDau @HoTen=N'Bệnh nhân hết hạn giả lập',@DiaChi=N'456 Đường Test',
 @NgaySinh='1988-08-03',@SoDienThoai=@phone2,@SoTien=500000,
 @MaBenhNhan=@bn2 OUTPUT,@MaHoSo=@hs2 OUTPUT;
/* Simulate a long-past registration only in TEST database. */
UPDATE dbo.PhieuThuLePhi SET NgayBatDauHieuLuc=DATEADD(YEAR,-2,@ngay),
  NgayHetHan=DATEADD(YEAR,-1,@ngay) WHERE MaBenhNhan=@bn2;
SET @blocked=0;
BEGIN TRY
 EXEC dbo.usp_MoPhieuKham @MaHoSo=@hs2,@MaBacSi=@bs,@MaKhoa=@khoa,
  @LoaiKham='DINH_KY',@MaPhieuKham=@book OUTPUT;
END TRY BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52018,N'TC11 FAIL: phí hết hạn vẫn khám định kỳ.',1;
EXEC dbo.usp_GiaHanLePhi @MaHoSo=@hs2,@SoTien=500000,@MaPhieuThu=@fee OUTPUT;
EXEC dbo.usp_MoPhieuKham @MaHoSo=@hs2,@MaBacSi=@bs,@MaKhoa=@khoa,
 @LoaiKham='DINH_KY',@MaPhieuKham=@book OUTPUT;
IF @book IS NULL THROW 52019,N'TC11 FAIL: sau gia hạn vẫn không khám định kỳ.',1;
PRINT N'TC11 PASS - Phí hết hạn chặn khám định kỳ, gia hạn cho phép khám';

-- TC12: wrong physician/department foreign-key business mapping rejected.
SET @blocked=0;
BEGIN TRY
 EXEC dbo.usp_MoPhieuKham @MaHoSo=@hs2,@MaBacSi=@bs,@MaKhoa=@khoaOther,
 @LoaiKham='KHAM_BENH',@MaPhieuKham=@book OUTPUT;
END TRY BEGIN CATCH SET @blocked=1; END CATCH;
IF @blocked=0 THROW 52020,N'TC12 FAIL: bác sĩ sai khoa được tiếp nhận.',1;
PRINT N'TC12 PASS - Bác sĩ phải thuộc đúng khoa điều trị';

PRINT N'== RESULT: 12/12 ASSERTION GROUPS PASS ==';
SELECT @hs AS MaHoSoMau, @visitFree AS PhieuKhamDinhKy, @visitPaid AS PhieuKhamDieuTri,
 @rx AS MaToaThuoc, @lab AS MaPhieuXetNghiem, @bill AS MaHoaDonDaThanhToan;
GO
