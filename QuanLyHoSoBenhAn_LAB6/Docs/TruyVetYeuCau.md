# BẢNG TRUY VẾT: YÊU CẦU → UML → FORM/SERVICE → SQL → TEST CASE

| Mã yêu cầu | Quy tắc cần kiểm thử | UML | Form (UI) | Service | Bảng CSDL | Stored procedure / constraint | Test case |
|---|---|---|---|---|---|---|---|
| RQ01 | Đăng ký lần đầu và kiểm tra trùng | `02_UseCase_TongQuat`, `04_Activity_TiepNhan`, `01_ClassDiagram` | `FrmDangKy` | `DangKyService.DangKyLanDau` | `BenhNhan`, `PhieuDangKy` | `usp_DangKyLanDau`, `UQ_BenhNhan_DanhTinh` | TC01, TC02 |
| RQ02 | Thu lệ phí năm và cấp hồ sơ trong cùng transaction | `01_ClassDiagram`, `04_Activity_TiepNhan` | `FrmDangKy` | `DangKyService.DangKyLanDau` | `PhieuThuLePhi`, `HoSoBenhAn` | `usp_DangKyLanDau` + `BEGIN TRAN/COMMIT` | TC01 |
| RQ03 | Gia hạn lệ phí năm không mất ngày hiệu lực cũ | `01_ClassDiagram` | `FrmDangKy` | `DangKyService.GiaHanLePhi` | `PhieuThuLePhi` | `usp_GiaHanLePhi`, `CK_PhieuThu_KhoangNgay` | TC09 |
| RQ04 | Tra cứu mã, lịch sử khám, kết quả và toa | `02_UseCase_TongQuat`, `01_ClassDiagram` | `FrmLichSu` | `HoSoService` | `HoSoBenhAn`, `PhieuKhamBenh`, `PhieuXetNghiem`, `ToaThuoc`, `TheoDoiDieuTri` | `SELECT` theo `MaHoSo` và FK | TC10 |
| RQ05 | Đặt lịch hẹn theo quý, không trùng, cho hủy/đặt lại | `01_ClassDiagram`, `02_UseCase_TongQuat` | `FrmLichHen` | `LichHenService` | `LichHen`, `HoSoBenhAn`, `PhieuThuLePhi` | `usp_DatLichHen`, `usp_HuyLichHen`, `UX_LichHen_HoSo_NamQuy` | TC03 |
| RQ06 | Khám định kỳ miễn tiền khám, còn phí năm, không trùng quý | `05_Activity_KhamToaVienPhi` | `FrmKhamBenh`, `FrmVienPhi` | `KhamService`, `VienPhiService` | `PhieuKhamBenh`, `PhieuThuLePhi`, `HoaDonVienPhi` | `usp_MoPhieuKham`, `UX_PhieuKham_HoSo_Quy`, `usp_LapHoaDon` | TC04, TC05, TC11 |
| RQ07 | Phiếu khám có bác sĩ đúng khoa và triệu chứng/chẩn đoán | `01_ClassDiagram`, `03_UseCase_PhanRaTiepNhan` | `FrmKhamBenh` | `KhamService` | `PhieuKhamBenh`, `BacSi`, `KhoaDieuTri` | `usp_MoPhieuKham`, `usp_CapNhatKham` | TC04, TC06, TC12 |
| RQ08 | Xét nghiệm phải thuộc phiếu khám, có kết quả | `01_ClassDiagram`, `05_Activity_KhamToaVienPhi` | `FrmXetNghiem` | `XetNghiemService` | `PhieuXetNghiem`, `LoaiXetNghiem`, `PhieuKhamBenh` | `usp_ChiDinhXetNghiem`, `usp_GhiKetQuaXetNghiem`, FK | TC06, TC07 |
| RQ09 | Toa thuốc và chi tiết thuốc phải thuộc phiếu khám | `01_ClassDiagram`, `06_Sequence_LapToaThuoc` | `FrmToaThuoc` | `ToaThuocService` | `ToaThuoc`, `ChiTietToaThuoc`, `Thuoc`, `PhieuKhamBenh` | `usp_TaoToaThuoc`, `usp_ThemChiTietToa`, FK, CHECK | TC06 |
| RQ10 | Hóa đơn đúng tiền khám / xét nghiệm; chặn khi kết quả chưa có | `05_Activity_KhamToaVienPhi` | `FrmVienPhi` | `VienPhiService.LapHoaDon` | `HoaDonVienPhi`, `PhieuXetNghiem` | `usp_LapHoaDon` | TC04, TC07 |
| RQ11 | Thanh toán đủ tiền mới cho hoàn tất, 0đ phải xác nhận | `05_Activity_KhamToaVienPhi` | `FrmVienPhi`, `FrmKhamBenh` | `VienPhiService.ThanhToan`, `KhamService.HoanTat` | `HoaDonVienPhi`, `PhieuKhamBenh` | `usp_XacNhanThanhToan`, `usp_HoanTatKham` | TC04, TC08 |
| RQ12 | Ghi nhận diễn biến, phản ứng phụ sau hoàn tất | `01_ClassDiagram`, `05_Activity_KhamToaVienPhi` | `FrmVienPhi` | `VienPhiService.GhiTheoDoi` | `TheoDoiDieuTri`, `NhanVienCSKH` | `usp_GhiNhanTheoDoi` | TC08 |

## Danh sách 16 lớp gốc được ánh xạ sang 16 bảng

`BenhNhan` → `BenhNhan`, `PhieuDangKy` → `PhieuDangKy`, `PhieuThuLePhi` → `PhieuThuLePhi`, `HoSoBenhAn` → `HoSoBenhAn`, `PhieuKhamBenh` → `PhieuKhamBenh`, `BacSi` → `BacSi`, `KhoaDieuTri` → `KhoaDieuTri`, `ToaThuoc` → `ToaThuoc`, `ChiTietToaThuoc` → `ChiTietToaThuoc`, `Thuoc` → `Thuoc`, `LoaiXetNghiem` → `LoaiXetNghiem`, `PhieuXetNghiem` → `PhieuXetNghiem`, `LichHen` → `LichHen`, `NhanVienCSKH` → `NhanVienCSKH`, `HoaDonVienPhi` → `HoaDonVienPhi`, `TheoDoiDieuTri` → `TheoDoiDieuTri`.

`Models/Entities.cs` có **16 lớp C#** (entity) ánh xạ từ Class Diagram sang bảng CSDL. `Models/Contracts.cs` định nghĩa DTO khi truyền dữ liệu giữa UI và Service. Các màn hình danh sách của bản demo sử dụng `DataTable` từ ADO.NET; những phương thức nghiệp vụ trong sơ đồ UML được hiện thực ở lớp Service và stored procedures thay vì đặt trực tiếp trong entity.

## Kiến trúc luồng ví dụ

```text
FrmToaThuoc (UI; nhận số lượng, liều dùng, chọn thuốc)
  └─ ToaThuocService.ThemThuoc (Service; điều phối nghiệp vụ)
       └─ Db.Proc("dbo.usp_ThemChiTietToa", @... ) (Data; ADO.NET)
            └─ SQL Server (stored procedure xác thực phiếu khám + INSERT ChiTietToaThuoc)
```

Tất cả tham số dữ liệu đi vào SQL đều qua `SqlParameter`, không ghép trực tiếp `TextBox.Text` vào SQL động.
