# Đối chiếu Class Diagram ↔ lớp C# ↔ bảng SQL Server

Tài liệu này gắn các Class trong đề Bài 6 với các POCO trong `Models/Entities.cs`, khóa chính và quan hệ. Kiểu tiền trong đề mô tả `Double` nhưng đổi sang `decimal/DECIMAL(18,2)` để tránh sai số tiền.

| Class (C#) | Khóa định danh (ID) | Bảng CSDL | Thuộc tính bổ sung để hiện thực |
|---|---|---|---|
| `TourDuLich` | `MaTour` | `TourDuLich` | Không |
| `DiemThamQuan` | `MaDiemThamQuan` | `DiemThamQuan` | Không |
| `NoiDungChan` | `MaNoiDungChan` | `NoiDungChan` | `LoaiKhachSan` cho phép NULL |
| `PhuongTien` | `MaPhuongTien` | `PhuongTien` | Không |
| `ChuyenKhachLe` | `MaChuyen` | `ChuyenKhachLe` | `MaTour`, `SucChua`, `DonGiaApDung`, `TrangThai` |
| `VeKhachLe` | `MaVe` | `VeKhachLe` | `MaChuyen`, `MaKhachHang`, `MaDiemBan`, `SoNguoi`, `TrangThai` |
| `DiemBanVe` | `MaDiemBan` | `DiemBanVe` | Không |
| `KhachHang` | `MaKhachHang` | `KhachHang` | Không |
| `PhieuDangKyDoan` | `MaPhieuDoan` | `PhieuDangKyDoan` | `MaTour`, `MaKhachHang`, `DiemDon`, `DonGiaApDung`, `TrangThai` |
| `DanhSachBaoHiem` | `MaSoBaoHiem` | `DanhSachBaoHiem` | `MaPhieuDoan` |
| `NhanVienHD` | `MaNhanVien` | `NhanVienHD` | Không |
| `PhieuKhaoSat` | `MaKhaoSat` | `PhieuKhaoSat` | `MaKhachHang`, `MaChuyen` hoặc `MaPhieuDoan` |
| `TourDiemThamQuan` | `(MaTour, MaDiemThamQuan)` | `TourDiemThamQuan` | `ThuTu` |
| `TourNoiDungChan` | `(MaTour, ThuTu)` | `TourNoiDungChan` | `MaNoiDungChan`, `MaPhuongTien` |
| `ThanhToanDoan` | `MaThanhToan` | `ThanhToanDoan` | `MaPhieuDoan`, `SoTien`, `NgayThanhToan` |
| `PhanCongHuongDan` | `MaPhanCong` | `PhanCongHuongDan` | `MaNhanVien`, `MaChuyen` XOR `MaPhieuDoan`, `TienCong` |

## Mối liên kết chính

- `TourDuLich (1) — (0..*) ChuyenKhachLe`, và `TourDuLich (1) — (0..*) PhieuDangKyDoan`.
- `TourDuLich (1) — (0..*) TourDiemThamQuan`, `DiemThamQuan (1) — (0..*) TourDiemThamQuan`: giải quyết quan hệ **N–N**.
- `TourDuLich (1) — (0..*) TourNoiDungChan`, `NoiDungChan (1) — (0..*) TourNoiDungChan` và `PhuongTien (1) — (0..*) TourNoiDungChan`.
- `ChuyenKhachLe (1) — (0..*) VeKhachLe`; `KhachHang (1) — (0..*) VeKhachLe`; `DiemBanVe (1) — (0..*) VeKhachLe`.
- `PhieuDangKyDoan (1) — (0..*) DanhSachBaoHiem`; nếu chọn bảo hiểm, Service yêu cầu số bản ghi **bằng** số thành viên.
- `PhieuDangKyDoan (1) — (0..1) ThanhToanDoan`, phiếu hủy không được tất toán, mỗi phiếu có tối đa một khoản thanh toán còn lại.
- `NhanVienHD (1) — (0..*) PhanCongHuongDan`; một phân công gắn **duy nhất một** chuyến lẻ **hoặc** một phiếu đoàn. Chuyến lẻ tối đa một phân công; đoàn nhiều phân công.
- `KhachHang (1) — (0..*) PhieuKhaoSat`; khảo sát gắn vào chuyến hoặc đoàn (XOR), chỉ cho phép sau khi tour kết thúc và đã tham gia.

Các ràng buộc được thiết kế 2 tầng: CHECK/FK/UNIQUE trong SQL Server và kiểm tra nghiệp vụ trước khi ghi trong Service. Đối với yêu cầu không thể biểu đạt đơn giản bằng CHECK (lịch HDV trùng nhau; số người bảo hiểm phải đầy đủ), Service sử dụng transaction để bảo toàn tính nhất quán.
