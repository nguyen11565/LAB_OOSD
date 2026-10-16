# Quyết định thiết kế và giả định được thêm vào so với đề bài

## 1. Những gì lấy trực tiếp từ file LAB6

Tài liệu gốc mô tả bệnh nhân đăng ký khám lần đầu, cung cấp họ tên/địa chỉ/ngày sinh/điện thoại/ảnh 3x4, đóng lệ phí hằng năm để được cấp hồ sơ. Lần khám sau bác sĩ được xem lịch sử, phiếu khám có bác sĩ/khoa/triệu chứng/những xét nghiệm cần thiết và toa thuốc; nhân viên chăm sóc khách hàng ghi lại sau thanh toán. Bệnh nhân đang có hồ sơ và quyền lợi được khám sức khỏe định kỳ **theo quý mà không phải đóng tiền khám**. Bảng Câu 1 trong tài liệu liệt kê **16 lớp**, từ `BenhNhan` đến `TheoDoiDieuTri`; các sơ đồ Câu 2–6 làm cơ sở ánh xạ.

## 2. Các giả định cần công khai

1. **Khóa định danh:** Bảng lớp gốc ghi `ma...: String`. Để dùng SQL Server FK hiệu quả và giảm sai sót khi nhập tay, toàn bộ khóa trong CSDL và sơ đồ triển khai được chuẩn hóa thành **`INT IDENTITY`**. Tên thuộc tính và quan hệ nghiệp vụ giữ nguyên. Không khẳng định đây là phép chuyển nguyên trạng kiểu dữ liệu của sơ đồ cũ.
2. **Đăng ký trùng:** Chặn khi trùng đúng bộ `(HoTen, NgaySinh, SoDienThoai)`. Không có CCCD trong yêu cầu; tổ hợp này chỉ phù hợp bài tập, chưa đủ nhận dạng bệnh nhân thực tế.
3. **Lệ phí năm:** Số tiền phí là dữ liệu nhập ở Form, ví dụ 500.000đ, **không có mức phí cố định trong đề**. Một biên lai bắt đầu từ ngày đăng ký/đóng phí, hết hiệu lực đúng ngày kỷ niệm năm sau. Phí năm không nằm trong hóa đơn viện phí từng lần khám.
4. **Định kỳ quý:** Một **quý dương lịch** = Q1 (1–3), Q2 (4–6), Q3 (7–9), Q4 (10–12). Tối đa một phiếu khám `DINH_KY`/hồ sơ/quý; lịch định kỳ còn hiệu lực cũng chỉ được một lịch/quý. Phải có phí năm còn hiệu lực tại ngày dự kiến (lịch) hoặc ngày khám thực tế.
5. **Chi phí viện phí:** Bài không cho bảng giá. Giả định **phí khám bệnh thông thường 150.000đ/lần** và **khám định kỳ 0đ tiền khám**. Giá của một xét nghiệm là giá danh mục lúc chỉ định (mặc định 120k/60k/80k). Bài không nói xét nghiệm định kỳ có miễn phí hay không; demo **chỉ miễn tiền khám, vẫn thu xét nghiệm nếu thực hiện**.
6. **Hoàn tất:** Có hóa đơn thanh toán trước khi `PhieuKhamBenh` sang `HOAN_TAT`, kể cả hóa đơn tiền bằng 0 (xác nhận mức thu 0đ). Dữ liệu `TheoDoiDieuTri` được ghi sau khi hoàn tất để bám mô tả nhân viên CSKH ghi nhận sau thanh toán.
7. **Toa thuốc:** Một phiếu khám có 0 hoặc 1 toa; một toa có nhiều dòng thuốc, mỗi thuốc xuất hiện tối đa một lần/toa. Xét nghiệm có thể nhiều chỉ định nhưng cùng một loại tối đa một lần/phiếu khám.
8. **Lịch hẹn:** Không đặt lịch vào quá khứ; không đặt hai lịch chưa hủy cho cùng bác sĩ tại cùng thời điểm. Khi tiếp nhận theo lịch phải đúng ngày hẹn, bác sĩ, loại khám và hồ sơ. Có thể mở phiếu khám trực tiếp mà không cần lịch.
9. **Dữ liệu ảnh:** `Anh3x4` là chuỗi đường dẫn hình, không lưu BLOB/không tải file trực tiếp; mức này phù hợp demo form.
10. **Không có phân quyền đăng nhập:** Có các vai trò Actor trong UML, nhưng màn hình dùng một tài khoản hệ điều hành kết nối SQL vì đề chỉ yêu cầu các nghiệp vụ, không yêu cầu bảo mật/đăng nhập.

## 3. Mô hình dữ liệu thêm cột để có truy vết

Các FK `MaBenhNhan`, `MaHoSo`, `MaPhieuKham`, `MaBacSi`, `MaKhoa`, `MaThuoc`, `MaNhanVien` được thêm vào các bảng tương ứng so với phần liệt kê thuộc tính rời rạc của Câu 1. Các cột `LoaiHen`, `DonGiaApDung`, `TienKham`, `TienXetNghiem`, `SoTienNhan`, `NgayHoanTat` là các cột triển khai bổ sung, có tác dụng ràng buộc nghiệp vụ và kiểm thử, không được ngầm hiểu là đã có trong lớp nguồn.

## 4. Một số giới hạn kỹ thuật

- Cơ chế transaction và kiểm tra tập trung ở stored procedures; việc ghi trực tiếp vào bảng từ tài khoản SQL có quyền `INSERT/UPDATE` vẫn có thể vượt một số quy tắc workflow. Môi trường thực tế cần quyền `EXECUTE` tối thiểu và cấm sửa bảng trực tiếp.
- Chưa xử lý đồng thời nhiều máy thanh toán ở cấp giao diện, nhưng các stored procedure có điều kiện cập nhật/trạng thái, khóa và unique index cho phần quan trọng.
- Các nghiệp vụ cập nhật hoặc xóa mọi danh mục, báo cáo thống kê, và in hóa đơn chuyên nghiệp chưa làm vì không nằm trong 5 nhóm quy tắc bắt buộc của yêu cầu bổ sung.
- Chưa compile/chạy trên Windows và SQL Server ở môi trường tạo dự án; test SQL cung cấp để chạy tại máy người nộp, không công bố “12/12 thực tế” khi chưa có đầu ra.
