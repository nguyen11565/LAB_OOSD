USE QuanLyHoSoBenhAn_LAB6;
GO
/* Reference-only seed: does NOT pre-create patients or invoices; try first registration in UI. */
IF NOT EXISTS(SELECT 1 FROM dbo.KhoaDieuTri)
BEGIN
 INSERT dbo.KhoaDieuTri(TenKhoa,MoTa) VALUES
 (N'Nội tổng quát',N'Khám bệnh nội khoa tổng quát'),
 (N'Ngoại',N'Khám và điều trị ngoại khoa'),
 (N'Nhi',N'Khám và điều trị trẻ em');
END;
IF NOT EXISTS(SELECT 1 FROM dbo.BacSi)
BEGIN
 INSERT dbo.BacSi(MaKhoa,HoTen,ChuyenMon,SoDienThoai) VALUES
 ((SELECT MaKhoa FROM dbo.KhoaDieuTri WHERE TenKhoa=N'Nội tổng quát'),N'BS. Nguyễn An',N'Nội khoa','0900000001'),
 ((SELECT MaKhoa FROM dbo.KhoaDieuTri WHERE TenKhoa=N'Ngoại'),N'BS. Trần Bình',N'Ngoại khoa','0900000002'),
 ((SELECT MaKhoa FROM dbo.KhoaDieuTri WHERE TenKhoa=N'Nhi'),N'BS. Lê Chi',N'Nhi khoa','0900000003');
END;
IF NOT EXISTS(SELECT 1 FROM dbo.NhanVienCSKH)
 INSERT dbo.NhanVienCSKH(HoTen,SoDienThoai) VALUES(N'Nguyễn Minh CSKH','0910000001');
IF NOT EXISTS(SELECT 1 FROM dbo.Thuoc)
 INSERT dbo.Thuoc(TenThuoc,HamLuong,DonViTinh,MoTa) VALUES
 (N'Paracetamol',N'500mg',N'viên',N'Hạ sốt, giảm đau'),
 (N'Vitamin C',N'500mg',N'viên',N'Bổ sung vitamin'),
 (N'Oresol',N'Gói 4.1g',N'gói',N'Bù nước');
IF NOT EXISTS(SELECT 1 FROM dbo.LoaiXetNghiem)
 INSERT dbo.LoaiXetNghiem(TenXetNghiem,MoTa,DonGia) VALUES
 (N'Công thức máu',N'Huyết học',120000),
 (N'Đường huyết',N'Đường máu',60000),
 (N'Tổng phân tích nước tiểu',N'Sàng lọc',80000);
PRINT N'03_Seed: đã thêm khoa, bác sĩ, CSKH, thuốc và loại xét nghiệm.';
GO
