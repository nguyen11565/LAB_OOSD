# Đặc tả nghiệp vụ triển khai

## Quy trình A — Đăng ký lần đầu + đóng lệ phí + cấp hồ sơ

**Actor:** Bệnh nhân, nhân viên CSKH, nhân viên thu ngân. **Tiền điều kiện:** Chưa tồn tại bệnh nhân trùng bộ họ tên + ngày sinh + điện thoại; nhập đủ họ tên, địa chỉ, điện thoại, ngày sinh hợp lệ và lệ phí năm > 0. **Luồng chính:** (1) CSKH nhận đăng ký; (2) thu phí năm; (3) `usp_DangKyLanDau` tạo `BenhNhan`, `PhieuDangKy`, `PhieuThuLePhi`, `HoSoBenhAn` trong **một transaction**; (4) trả `MaBenhNhan`, `MaHoSo`. **Ngoại lệ:** Thiếu dữ liệu, tên trùng, điện thoại không hợp lệ hoặc phí không hợp lệ => không phát hồ sơ, không thu phí vào CSDL. **Hậu điều kiện:** 1 bệnh nhân, 1 phiếu đăng ký, 1 biên lai hiệu lực 1 năm, 1 hồ sơ.

## Quy trình B — Tra cứu lịch sử

**Actor:** Bác sĩ, nhân viên CSKH. **Tiền điều kiện:** Có mã hồ sơ hoặc thông tin nhận dạng. **Luồng chính:** Tìm từ `BenhNhan ↔ HoSoBenhAn`; truy vấn các `PhieuKhamBenh` theo ngày; theo từng phiếu lấy kết quả XN, toa thuốc, hóa đơn, diễn biến điều trị. **Ngoại lệ:** Không có kết quả => trả bảng rỗng. **Hậu điều kiện:** Không thay đổi CSDL.

## Quy trình C — Hẹn khám định kỳ theo quý

**Actor:** Bệnh nhân, nhân viên CSKH. **Tiền điều kiện:** Có hồ sơ và biên lai phí năm hiệu lực **vào ngày dự kiến**. **Luồng chính:** Chọn bác sĩ và ngày giờ hẹn; `usp_DatLichHen` kiểm tra trùng quý, bác sĩ bận, loại lịch, phí; trả mã lịch. Khi khám thật, `usp_MoPhieuKham` kiểm tra phí hiện tại và chưa có phiếu `DINH_KY` trong quý. **Ngoại lệ:** Chưa đóng phí, quá hạn, trùng quý, giờ bác sĩ bận, ngày quá khứ => không tạo lịch; lịch chờ có thể hủy và đặt lại. **Hậu điều kiện:** Một hồ sơ có tối đa một lịch định kỳ chưa hủy trong mỗi quý, và một phiếu khám định kỳ/quý.

## Quy trình D — Khám bệnh, xét nghiệm và toa thuốc

**Actor:** Bác sĩ, nhân viên xét nghiệm. **Tiền điều kiện:** Phiếu khám `DANG_KHAM`. **Luồng chính:** Mở phiếu theo hồ sơ/bác sĩ/khoa; ghi triệu chứng, thân nhiệt, huyết áp, chẩn đoán; chỉ định XN nếu cần (lưu giá tại thời điểm chỉ định); nhân viên trả kết quả; bác sĩ lập một toa thuộc phiếu khám; thêm từng dòng thuốc với liều, cách dùng, số ngày. **Ngoại lệ:** Thuốc có liều/số lượng/số lần/ngày <= 0, thêm trùng thuốc, phiếu đã được lập hóa đơn, kết quả rỗng => bị từ chối. **Hậu điều kiện:** Bảng XN và Toa có FK đến cùng mã phiếu khám, không thể bị gán nhầm sang bệnh nhân khác.

## Quy trình E — Thanh toán viện phí rồi hoàn tất

**Actor:** Nhân viên thu ngân, nhân viên CSKH. **Tiền điều kiện:** Phiếu đang khám, kết quả xét nghiệm đã ghi đủ, toa nếu có đã có chi tiết. **Luồng chính:** Lập hóa đơn (khám thường 150k, định kỳ 0đ, XN theo giá chụp lúc chỉ định); nhận tiền >= tổng hóa đơn; xác nhận trạng thái `DA_THANH_TOAN`; `usp_HoanTatKham` kiểm tra hóa đơn + kết quả và đóng phiếu; CSKH ghi diễn biến/phản ứng phụ. **Ngoại lệ:** Thanh toán thiếu, hóa đơn chưa thanh toán, XN chưa có kết quả, toa rỗng => không được hoàn tất. **Hậu điều kiện:** Phiếu `HOAN_TAT`, có `NgayHoanTat`, hóa đơn đã trả, thông tin điều trị có thể được theo dõi.
