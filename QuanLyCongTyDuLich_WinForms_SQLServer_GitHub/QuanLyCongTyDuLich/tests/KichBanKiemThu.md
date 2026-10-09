# KỊCH BẢN KIỂM THỬ NGHIỆM THU (DỮ LIỆU CỤ THỂ)

**Hệ thống:** Quản lý công ty du lịch Văn Hóa Việt. **Môi trường:** Windows + Visual Studio 2022 + .NET Framework 4.7.2 + SQL Server. **Tiền điều kiện:** chạy `01_Schema.sql`, `02_Seed.sql`. **Ngày thử nghiệm giả định:** 09/10/2026; thay các ngày tương lai nếu chạy vào ngày khác.

**Cách ghi kết quả:** cột *Kết quả thực tế* chưa được đánh dấu là Đạt; người kiểm thử chạy trên Windows thật rồi điền kết quả, chụp ảnh và ký xác nhận.

| TC | Mã yêu cầu | Dữ liệu nhập / thao tác | Kết quả mong đợi | Kết quả thực tế |
|---|---|---|---|---|
| TC01 | R01, R03 | `Danh mục` → Tour → tạo `T004`, `Vũng Tàu`, 2 ngày, 1 đêm, 1.500.000đ | Thêm thành công, grid xuất hiện `T004` | Chưa chạy |
| TC02 | R01 | Tạo `T005`, số ngày 2, số đêm 3, giá 1.500.000đ | Bị từ chối; không có `T005` trong SQL | Chưa chạy |
| TC03 | R02 | `Tuyến và điểm` → Tour `T001`, điểm `DT001`, thứ tự 1 (đã tồn tại) | Từ chối do khóa chính/thứ tự duy nhất, không tạo bản ghi trùng | Chưa chạy |
| TC04 | R04 | `Chuyến khách lẻ` → tạo `CH003`, `T001`, đi `15/12/2026`, 20 chỗ | Ngày về `17/12/2026`, trạng thái `DangMo`, đang có 0 khách | Chưa chạy |
| TC05 | R05 | Bán `VE003`: `CH003`, `KH001`, `DBV01`, 2 người, đón `Bến Thành` | Vé `DaThanhToan`, tiền 5.000.000đ, số lượng chuyến 2 | Chưa chạy |
| TC06 | R05 | Tạo `CH004` tour `T001` sức chứa 2; thử bán `VE004` với 3 người | Từ chối vượt sức chứa; `CH004` vẫn 0 khách, không có `VE004` | Chưa chạy |
| TC07 | R05 | Thử bán vé số người = 12 | Giao diện từ chối (chuyển đăng ký đoàn) | Chưa chạy |
| TC08 | R06 | Chọn `VE003` → Hủy vé | Vé `DaHuy`, số khách `CH003` giảm từ 2 về 0, không tạo hoàn tiền tự động | Chưa chạy |
| TC09 | R07 | Đoàn `PD003`, `T001`, `KH001`, tên `Công ty XYZ`, địa chỉ `TP.HCM`, số ĐT `0909888777`, đại diện `Nguyễn Văn An`, 12 khách, đi `25/12/2026`, đón `Quận 1`, cọc 6.000.000đ, không bảo hiểm | Phiếu `DaDatCoc`; tổng chi phí 30.000.000đ, còn lại 24.000.000đ | Chưa chạy |
| TC10 | R07 | Tạo `PD004` 12 người, `T001`, cọc = 0đ | Từ chối; không lưu phiếu | Chưa chạy |
| TC11 | R08 | Tạo `PD005` 12 người, `T001`, có bảo hiểm nhưng nhập 11 dòng `Họ tên\|CCCD\|dd/MM/yyyy` | Không lưu phiếu; báo phải đủ **12 người**. Nhập đủ 12 dòng hợp lệ thì lưu 12 bản ghi `DanhSachBaoHiem` | Chưa chạy |
| TC12 | R09 | Chọn `PD003` (chưa đi) → Hủy phiếu | `DaHuy`, cọc 6.000.000đ vẫn ghi nhận; phân công (nếu có) được giải phóng | Chưa chạy |
| TC13 | R09 | Chọn phiếu mẫu `PD001` (đã hoàn thành) → Tất toán | Thu 22.000.000đ, sinh một dòng `ThanhToanDoan`, phiếu `DaThanhToan` | Chưa chạy |
| TC14 | R10 | Tạo `CH003` theo TC04; phân công `NV001` cho `CH003`, tiền công 900.000đ | Thành công do không đụng lịch `CH001` (10–12/12/2026) | Chưa chạy |
| TC15 | R10 | Tạo `CH005` `T001` khởi hành `11/12/2026`; phân công `NV001` cho `CH005` | Bị từ chối vì `NV001` bận `CH001` từ 10–12/12/2026 | Chưa chạy |
| TC16 | R11 | `Báo cáo` → Bảng lương → tháng 7 năm 2026 | `NV001`: 8.000.000đ; `NV002`: 10.000.000đ; `NV003`: 8.900.000đ | Chưa chạy |
| TC17 | R12 | Khảo sát `KS003`: khách `KH002`, chuyến `CH002`, 5 sao, góp ý `Rất tốt` | Thêm thành công vì `CH002` đã hoàn thành và `KH002` có vé hợp lệ | Chưa chạy |
| TC18 | R12 | Khảo sát `KS004`: khách `KH001`, chuyến `CH002` | Bị từ chối vì `KH001` không tham gia `CH002` | Chưa chạy |
| TC19 | R09 | Sau TC13, tất toán tiếp `PD001` | Bị từ chối; bảng `ThanhToanDoan` vẫn chỉ có đúng 1 khoản tất toán cho `PD001` | Chưa chạy |
| TC20 | R07 | Đổi giá tour `T001` từ 2.500.000 lên 2.600.000 sau khi đã có vé và phiếu | Các vé/phiếu cũ vẫn giữ giá theo `DonGiaApDung`; các đặt mới dùng giá 2.600.000đ | Chưa chạy |

## Kịch bản kiểm thử transaction và đồng thời

**TC21 – Tính nguyên tử:** Tạo phiếu đoàn có mua bảo hiểm nhưng trùng một số CMND/Passport trong cùng phiếu. SQL sẽ từ chối khóa duy nhất; kiểm tra **không có phiếu đoàn và không có thành viên bảo hiểm nào** được lưu một phần.

**TC22 – Không bán quá số ghế khi 2 máy đồng thời:** Tạo chuyến `CH006` (sức chứa 2) rồi 2 người dùng khác nhau mỗi người cố đặt 2 ghế cùng lúc. Chỉ có 1 vé thành công, số khách kết thúc = 2, không phải 4. Kiểm tra với:

```sql
SELECT MaChuyen, SucChua, SoLuongHienTai
FROM dbo.ChuyenKhachLe WHERE MaChuyen = N'CH006';
```

**TC23 – Không phân công chồng lịch khi 2 máy đồng thời:** Hai máy cùng phân `NV003` vào hai đoàn có ngày giao nhau. Tại thời điểm commit, tối đa một thao tác thành công (trừ trường hợp SQL phát hiện deadlock rồi từ chối một thao tác). Không có hai lịch chồng nhau cho cùng một nhân viên.

## Tự kiểm tra SQL

Sau khi chạy `sql/01_Schema.sql` và `sql/02_Seed.sql`, mở `tests/03_SmokeTests.sql` trong SSMS, nhấn **Execute**. Khi mọi kiểm tra đạt, phần Messages sẽ có `ALL SQL SMOKE TESTS PASSED.`. Bài này kiểm tra bảng/seed, giá và cọc, số ghế/giá vé, lương, trùng lịch, danh sách bảo hiểm và SQL `CHECK` sức chứa. Đây **không phải** kiểm thử WinForms tự động.

## Mẫu ghi nhận kết quả nộp giảng viên

- Người kiểm thử: _______________________    Ngày chạy: ____ / ____ / ______
- Môi trường: Visual Studio ______ / SQL Server ______ / Windows ______
- Số ca đạt: ______ / 23    Số ca không đạt: ______
- Bug ID và mô tả (nếu có): __________________________________________
- Minh chứng: ảnh giao diện thao tác, bảng kết quả query trong SSMS và ảnh Messages.
