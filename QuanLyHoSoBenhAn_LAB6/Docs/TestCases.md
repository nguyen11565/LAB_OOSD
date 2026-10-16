# TEST CASES — QUẢN LÝ HỒ SƠ BỆNH ÁN

**Môi trường kiểm thử yêu cầu:** Windows + SSMS + SQL Server 2017+, đã chạy `Database/01_Schema.sql`, `02_Procedures.sql`, `03_Seed.sql`. Chạy `Tests/01_TestCases.sql` trên **CSDL thử nghiệm**, không dùng bệnh án thật. Mỗi lần test tự sinh 2 SĐT số ngẫu nhiên để tránh xung đột. Đầu ra dưới đây là **kỳ vọng**, không phải kết quả đo thực tế.

| ID | Yêu cầu | Dữ liệu / bước kiểm thử cụ thể | Kết quả mong đợi | Trạng thái |
|---|---|---|---|---|
| TC01 | Đăng ký đầu tiên + phí năm + cấp hồ sơ | Đăng ký `Nguyễn Văn Kiểm Thử`, ngày sinh 22/05/1998, SĐT random bắt đầu 09, địa chỉ `123 Đường Kiểm Thử`, lệ phí **500.000đ** | Tạo đúng bệnh nhân, phiếu đăng ký, phiếu thu phí hạn 1 năm, 1 mã hồ sơ; không thiếu bảng nào | Chưa chạy trực tiếp |
| TC02 | Chống trùng bệnh nhân | Gửi lại đúng họ tên + ngày sinh + SĐT của TC01 | Thủ tục từ chối; không cấp hồ sơ mới | Chưa chạy trực tiếp |
| TC03 | Lịch định kỳ theo quý | Đặt `DINH_KY` cho hồ sơ TC01 sau 3 ngày, sau đó cố đặt lịch khác cùng quý; hủy lịch đầu và đặt lại | Lần đầu thành công; đặt trùng bị chặn; sau hủy được đặt lại | Chưa chạy trực tiếp |
| TC04 | Miễn tiền khám quý + hóa đơn 0 | Mở phiếu `DINH_KY`, nhiệt độ **36,8°C**, không xét nghiệm, lập hóa đơn, thử hoàn tất trước thanh toán 0, rồi xác nhận 0 và hoàn tất | `TienKham=0`, `TongTien=0`; chưa xác nhận thì không hoàn tất, sau xác nhận thì `HOAN_TAT` | Chưa chạy trực tiếp |
| TC05 | Chống khám 2 lần/quý | Sau TC04 mở tiếp `DINH_KY` cho cùng hồ sơ cùng quý | SQL từ chối vì trùng quý (`UX_PhieuKham_HoSo_Quy`) | Chưa chạy trực tiếp |
| TC06 | Phiếu khám – XN – toa | Khám thông thường triệu chứng `Đau đầu, sốt`, nhiệt độ **38,5°C**, chỉ định `Công thức máu`, tạo toa kê **10 viên**, 1 viên/lần, 2 lần/ngày, 5 ngày; thử số lượng 0 | Cả xét nghiệm và toa đều có đúng `MaPhieuKham`, có `ChiTietToaThuoc` thuộc `MaToaThuoc`, số lượng 0 bị chặn | Chưa chạy trực tiếp |
| TC07 | Hoá đơn và kết quả XN | Thử lập hóa đơn khi xét nghiệm `CHO_KET_QUA`; ghi kết quả `Chỉ số trong giới hạn`, rồi lập lại | Ban đầu bị từ chối; sau khi có kết quả, `TongTien=150.000+120.000=270.000đ` theo dữ liệu seed | Chưa chạy trực tiếp |
| TC08 | Hoàn tất sau viện phí | Với hóa đơn 270.000đ: trả 1.000đ → thử hoàn tất → trả đủ 270.000đ → hoàn tất → ghi theo dõi `Hết sốt sau 2 ngày` | Trả thiếu bị chặn, hoàn tất sớm bị chặn; thanh toán đủ thì trạng thái `HOAN_TAT`, theo dõi ghi được | Chưa chạy trực tiếp |
| TC09 | Gia hạn lệ phí năm | Phí đầu hạn 1 năm; gia hạn **500.000đ** khi phí vẫn còn hiệu lực | Ngày bắt đầu biên lai mới chính là ngày hết hạn biên lai cũ (không mất thời gian) | Chưa chạy trực tiếp |
| TC10 | Tra cứu lịch sử | Truy theo `MaHoSo` TC01 sau 1 lượt định kỳ và 1 lượt bệnh thường | Tối thiểu 2 phiếu khám; XN có kết quả, toa và hóa đơn đã thanh toán gắn đúng phiếu bệnh thường | Chưa chạy trực tiếp |
| TC11 | Khám định kỳ cần phí hiệu lực | Tạo bệnh nhân 2, giả lập ngày phí đã hết hạn 1 năm, mở `DINH_KY`; sau đó gia hạn và thử lại | Khi hết hạn bị từ chối; sau gia hạn được mở phiếu định kỳ | Chưa chạy trực tiếp |
| TC12 | Bác sĩ đúng khoa | Chọn bác sĩ nội tổng quát nhưng truyền mã khoa ngoại vào `usp_MoPhieuKham` | Từ chối tạo phiếu khám vì bác sĩ không thuộc khoa | Chưa chạy trực tiếp |

## Mẫu ghi biên bản kiểm thử khi chạy trên máy

| Lần chạy | Ngày giờ | SQL Server instance | Test PASS | Test FAIL | Screenshot / Ghi chú |
|---|---|---|---|---|---|
| 1 | ... | ... | .../12 | ... | ... |

**Hướng dẫn ghi bằng chứng:** trong SSMS chụp tab `Messages` có các dòng `TC01 PASS` … `TC12 PASS` và câu kết. Nếu test không qua, chụp lỗi `Msg ...`, đọc lại `Database/01_Schema.sql`, `02_Procedures.sql` và dữ liệu seed. Kiểm thử SQL và UI nên tiến hành trên database học tập mới tạo.

**Lưu ý về TC07:** Mức 120.000đ lấy từ dòng đầu tiên của `03_Seed.sql`. Nếu đã thay đổi danh mục, test SQL so sánh giá hiện hành `DonGia` thay vì hardcode 120.000đ; bảng ca kiểm thử này trình bày dữ liệu mặc định.
