/* Du lieu mau; chay mot lan sau 01_Schema.sql. Ngay: nam 2026, có ca lich su cho kiem thu. */
USE QuanLyCongTyDuLich;
GO
SET XACT_ABORT ON;
BEGIN TRAN;
IF EXISTS(SELECT 1 FROM dbo.TourDuLich WHERE MaTour = N'T001')
BEGIN ROLLBACK; THROW 50002, N'Du lieu mau da ton tai, khong chay lai.', 1; END;
INSERT dbo.TourDuLich VALUES
(N'T001', N'Da Lat - Thanh pho ngan hoa', 3, 2, 2500000),
(N'T002', N'Nha Trang - Bien xanh', 4, 3, 3200000),
(N'T003', N'Phan Thiet - Mui Ne', 2, 1, 1800000);
INSERT dbo.DiemThamQuan VALUES
(N'DT001',N'Ho Xuan Huong',N'Da Lat',N'Tham quan ho va canh quan trung tam'),
(N'DT002',N'Thap Ba Ponagar',N'Nha Trang',N'Di tich van hoa Cham'),
(N'DT003',N'Doi cat bay',N'Mui Ne',N'Canh quan doi cat');
INSERT dbo.NoiDungChan VALUES
(N'NC001',N'Da Lat',1,1,1,3),
(N'NC002',N'Nha Trang',1,1,1,4),
(N'NC003',N'Mui Ne',0,1,0,NULL);
INSERT dbo.PhuongTien VALUES
(N'PT001',N'Xe 45 cho',N'O to'),
(N'PT002',N'May bay',N'Hang khong'),
(N'PT003',N'Tau hoa',N'Duong sat');
INSERT dbo.DiemBanVe VALUES
(N'DBV01',N'Quay quan 1',N'123 Nguyen Hue, Quan 1',N'02812345678'),
(N'DBV02',N'Quay Thu Duc',N'45 Vo Van Ngan, Thu Duc',N'02887654321');
INSERT dbo.KhachHang VALUES
(N'KH001',N'Nguyen Van An',N'0901000001',N'TP.HCM',N'an@example.com'),
(N'KH002',N'Tran Thi Binh',N'0901000002',N'TP.HCM',N'binh@example.com'),
(N'KH003',N'Le Van Cuong',N'0901000003',N'TP.HCM',N'cuong@example.com');
INSERT dbo.NhanVienHD VALUES
(N'NV001',N'Pham Huong Duong',N'0911000001',8000000),
(N'NV002',N'Do Hoang Lam',N'0911000002',8500000),
(N'NV003',N'Bui Thanh Tam',N'0911000003',7800000);
INSERT dbo.TourDiemThamQuan VALUES
(N'T001',N'DT001',1),(N'T002',N'DT002',1),(N'T003',N'DT003',1);
INSERT dbo.TourNoiDungChan VALUES
(N'T001',1,N'NC001',N'PT001'),(N'T002',1,N'NC002',N'PT002'),(N'T003',1,N'NC003',N'PT001');
INSERT dbo.ChuyenKhachLe(MaChuyen,MaTour,DonGiaApDung,NgayDi,NgayVe,SucChua,SoLuongHienTai,TrangThai) VALUES
(N'CH001',N'T001',2500000,'2026-12-10','2026-12-12',30,2,N'DangMo'),
(N'CH002',N'T002',3200000,'2026-07-01','2026-07-04',25,1,N'HoanThanh');
INSERT dbo.VeKhachLe VALUES
(N'VE001',N'CH001',N'KH001',N'DBV01','2026-10-01',N'Ben Thanh',2,5000000,N'DaThanhToan'),
(N'VE002',N'CH002',N'KH002',N'DBV02','2026-06-15',N'Nga tu Thu Duc',1,3200000,N'DaThanhToan');
INSERT dbo.PhieuDangKyDoan(MaPhieuDoan,MaTour,MaKhachHang,DonGiaApDung,TenCoQuan,DiaChiCoQuan,DienThoaiCoQuan,NguoiDaiDien,SoNguoiDi,NgayDiYeuCau,DiemDon,TienCoc,CoBaoHiem,TrangThai) VALUES
(N'PD001',N'T003',N'KH003',1800000,N'Cong ty ABC',N'12 Le Loi, TP.HCM',N'02899887766',N'Le Van Cuong',15,'2026-07-10',N'Van phong ABC',5000000,0,N'DaHoanThanh'),
(N'PD002',N'T001',N'KH002',2500000,N'Gia dinh Binh',N'Quan 7, TP.HCM',N'0901000002',N'Tran Thi Binh',12,'2026-12-20',N'Quan 7',6000000,0,N'DaDatCoc');
INSERT dbo.PhanCongHuongDan VALUES
(N'PC001',N'NV001',N'CH001',NULL,900000),
(N'PC002',N'NV002',N'CH002',NULL,1500000),
(N'PC003',N'NV003',NULL,N'PD001',1100000);
INSERT dbo.PhieuKhaoSat VALUES
(N'KS001',N'KH002',N'CH002',NULL,'2026-07-05',N'Tour dep, huong dan nhiet tinh',5),
(N'KS002',N'KH003',NULL,N'PD001','2026-07-13',N'De nghi them thoi gian tham quan',4);
COMMIT;
GO
PRINT N'Da nap du lieu mau: tour, khach, nhan vien, 2 chuyen, 2 doan, phan cong, khao sat.';
