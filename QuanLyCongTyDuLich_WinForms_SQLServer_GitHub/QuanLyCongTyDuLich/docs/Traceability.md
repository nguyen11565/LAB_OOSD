# Ma trận truy vết: Yêu cầu → UML → WinForms/Service → CSDL → Kiểm thử

**Nguồn yêu cầu:** `LAB5_1250080120(5).docx`, Bài 6: Quản lý công ty du lịch Văn Hóa Việt. Danh sách 12 lớp lấy ở trang 3–4; nghiệp vụ từ trang 1–2; sơ đồ do bài làm mô tả ở trang 5–10.

| Mã | Yêu cầu nghiệp vụ | UML | Form (UI) | Service (xử lý) | Bảng SQL Server | Test case |
|---|---|---|---|---|---|---|
| R01 | Quản lý tour, đơn giá, số ngày đêm | `class.puml` / `usecase.puml` | `FrmDanhMuc` | `CatalogService` | `TourDuLich` | TC01, TC02 |
| R02 | Quản lý điểm tham quan, dừng chân, khách sạn, phương tiện theo thứ tự | `class.puml` | `FrmDanhMuc`, `FrmTuyenDiem` | `CatalogService`, `ItineraryService` | `DiemThamQuan`, `NoiDungChan`, `PhuongTien`, `TourDiemThamQuan`, `TourNoiDungChan` | TC03 |
| R03 | Quản lý khách hàng, điểm bán vé, nhân viên | `class.puml` | `FrmDanhMuc` | `CatalogService` | `KhachHang`, `DiemBanVe`, `NhanVienHD` | TC01 |
| R04 | Lập chuyến khách lẻ theo lịch, tự tính ngày về | `activity_khach_le.puml` | `FrmChuyenLe` | `BookingService.CreateTrip` | `ChuyenKhachLe`, `TourDuLich` | TC04 |
| R05 | Bán vé khách lẻ 1–11 người, kiểm tra chỗ, thanh toán ngay | `sequence_khach_le.puml` / `activity_khach_le.puml` | `FrmChuyenLe` | `BookingService.SellTicket` | `VeKhachLe`, `ChuyenKhachLe` | TC05, TC06, TC07, TC-SQL03, TC-SQL08 |
| R06 | Hủy vé trước khi khởi hành, cập nhật số khách | `activity_khach_le.puml` | `FrmChuyenLe` | `BookingService.CancelTicket` | `VeKhachLe`, `ChuyenKhachLe` | TC08 |
| R07 | Đăng ký đoàn từ 12 người, chọn ngày đi/điểm đón, đặt cọc | `sequence_doan.puml` / `activity_doan.puml` | `FrmPhieuDoan` | `BookingService.CreateGroup` | `PhieuDangKyDoan`, `TourDuLich`, `KhachHang` | TC09, TC10, TC-SQL04 |
| R08 | Đoàn mua bảo hiểm phải có danh sách đủ số người | `sequence_doan.puml` | `FrmPhieuDoan` | `BookingService.CreateGroup` | `DanhSachBaoHiem` | TC11, TC-SQL07 |
| R09 | Hủy đoàn mất cọc; sau tour thu phần còn lại | `activity_doan.puml` | `FrmPhieuDoan` | `BookingService.CancelGroup/CompleteGroup/SettleGroup` | `PhieuDangKyDoan`, `ThanhToanDoan` | TC12, TC13 |
| R10 | Phân công HDV: một người cho chuyến lẻ; có thể nhiều người cho đoàn; tránh trùng lịch | `class.puml`, `usecase.puml` | `FrmPhanCong` | `SchedulingService.Assign` | `PhanCongHuongDan`, `NhanVienHD` | TC14, TC15, TC-SQL05 |
| R11 | Lương tháng = lương cơ bản + công tour **đã hoàn thành** bắt đầu trong tháng | `class.puml` | `FrmBaoCao` | `ReportService.Payroll` | `NhanVienHD`, `PhanCongHuongDan`, `ChuyenKhachLe`, `PhieuDangKyDoan` | TC16, TC-SQL06 |
| R12 | Ghi nhận góp ý sau tour từ khách tham gia | `usecase.puml` | `FrmKhaoSat`, `FrmBaoCao` | `SurveyService.Create`, `ReportService.Surveys` | `PhieuKhaoSat` | TC17 |

## Kiến trúc

`Frm*.cs` (UI) → `CatalogService / ItineraryService / LookupService / BookingService / SchedulingService / SurveyService / ReportService` → `Db / CatalogRepository` (Data) → `SQL Server`.

Các Form chỉ gọi Service. `LookupService` cấp danh sách chọn cho ComboBox; `ItineraryService` quản lý bảng liên kết nhiều-nhiều. Các giao dịch nhiều bước được thực thi nhất quán tại Service.

## Quy tắc bổ sung để xử lý điểm chưa rõ trong đề

- Bài gốc nêu **trên 12** là đoàn, **dưới 12** là khách lẻ nhưng không đề cập **đúng 12**; project chọn **12 người tính là đoàn** để không bỏ sót trường hợp.
- Đơn giá được **chốt tại thời điểm** tạo chuyến/đoàn (`DonGiaApDung`) để chỉnh giá tour về sau không làm sai vé/tiền cọc.
- Vé lẻ được xác nhận **đã trả đủ** khi xuất; hủy vé chưa tự hoàn tiền vì tài liệu không có chính sách hoàn vé.
- Đoàn bị hủy giữ tiền đặt cọc đúng mô tả; chỉ tất toán được sau khi trạng thái tour đã hoàn thành. Nhân viên hoàn thành tour sau ngày về thực tế.
- Quy ước lịch HDV: **cả ngày đi và ngày về đều tính bận**, không cho phân công hai tour trùng một trong hai ngày.
- Công tour ghi nhận vào **tháng khởi hành**, chỉ cộng lương khi chuyến/đoàn đã hoàn thành. Đây là quy ước để tính lương tháng thống nhất.
- Khách sạn nếu có phải đạt 2–5 sao; không có thì `LoaiKhachSan` để NULL.
- Thứ tự từng nơi dừng được lưu trong `TourNoiDungChan` và phương tiện di chuyển tại từng chặng.

## Giới hạn bản demo

- Đây là **phần mềm quản trị nội bộ WinForms**, không phải website quảng cáo trong mô tả mở rộng của đề. Không có thanh toán qua ngân hàng; thanh toán tiền mặt được ghi nhận trong SQL.
- Người đăng nhập/ủy quyền, chức năng in hóa đơn, xuất PDF, tự động gửi khảo sát và kiểm thử UI tự động chưa được xây dựng.
- Trạng thái và dữ liệu phải thay đổi qua ứng dụng để kích hoạt đầy đủ các kiểm tra Service; chỉ SQL constraint có hiệu lực nếu sửa trực tiếp từ SSMS.
