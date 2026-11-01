# TỔNG HỢP LAB – PHƯƠNG PHÁP PHÁT TRIỂN PHẦN MỀM HƯỚNG ĐỐI TƯỢNG (OOSD)

> Bộ bài thực hành từ **LAB 0 đến LAB 8** của môn **Phương pháp phát triển phần mềm hướng đối tượng**. Nội dung được tổng hợp theo tài liệu báo cáo của sinh viên, bao gồm phân tích yêu cầu, mô hình UML, thiết kế lớp, giao diện và các bài tập thực hành tương ứng.

## Thông tin sinh viên

| Thông tin | Nội dung |
| --- | --- |
| Sinh viên | **Hoàng Tuấn Nguyên** |
| MSSV | **1250080120** |
| Lớp | **12_ĐH_CNPM2** |
| Khóa | **2023–2027** |
| Giảng viên hướng dẫn | **ThS. Phạm Trọng Huynh** |
| Trường | **Trường Đại học Tài nguyên và Môi trường TP. Hồ Chí Minh** |
| Khoa | **Công nghệ thông tin** |

**Repository:** [nguyen11565/LAB_OOSD](https://github.com/nguyen11565/LAB_OOSD)

## Mục lục LAB

| LAB | Chủ đề | Nội dung trọng tâm | Liên kết |
| --- | --- | --- | --- |
| 0 | Cài đặt và kiểm tra môi trường | JDK, Maven, Git, IDE, starter project | [LAB0](https://github.com/nguyen11565/LAB_OOSD/tree/LAB0)) |
| 1 | Đặc tả yêu cầu Use Case | Thư viện trực tuyến và cửa hàng nước giải khát | [LAB1](https://github.com/nguyen11565/LAB_OOSD/tree/LAB1) |
| 2 | Hệ thống quản lý thư viện | Activity, Sequence và giao diện FrmMain | [LAB2](https://github.com/nguyen11565/LAB_OOSD/tree/LAB2) |
| 3 | Hệ thống quản lý khách sạn | Các form nghiệp vụ, cơ sở dữ liệu | [LAB3](https://github.com/nguyen11565/LAB_OOSD/tree/LAB3) |
| 4 | Cửa hàng trực tuyến E-Shopping | Use Case, Activity, Sequence, Class, giao diện | [LAB4](https://github.com/nguyen11565/LAB_OOSD/tree/LAB4) |
| 5 | Quản lý công ty du lịch | Lớp, Use Case, Activity, Sequence | [LAB5](https://github.com/nguyen11565/LAB_OOSD/tree/LAB5) |
| 6 | Quản lý hồ sơ bệnh án | Lớp, Use Case, quy trình khám, lập toa thuốc | [LAB6](https://github.com/nguyen11565/LAB_OOSD/tree/LAB6) |
| 7 | Quản lý nhân sự | Nhân viên, hợp đồng, chấm công, lương, khen thưởng | [LAB7](https://github.com/nguyen11565/LAB_OOSD/tree/LAB7) |
| 8 | Quản lý cửa hàng nước giải khát | UML, quy trình bán/giao hàng, ghi nợ, giao diện | [LAB8](https://github.com/nguyen11565/LAB_OOSD/tree/LAB8) |

> **Lưu ý:** Tài liệu báo cáo có nội dung của LAB 1 nhưng không ghi riêng liên kết nhánh `LAB1`; do đó bảng trên chỉ trỏ về repository chính. Các liên kết là địa chỉ được ghi trong tài liệu, chưa xác minh tình trạng hiện tại của từng nhánh.

---

## LAB 0 – Cài đặt và kiểm tra môi trường

**Mục tiêu:** Chuẩn bị công cụ phát triển Java và xác nhận có thể biên dịch, chạy một dự án Java cơ bản.

**Nội dung thực hành:**

- Kiểm tra và ghi nhận thông tin **JDK**.
- Kiểm tra công cụ quản lý dự án **Maven**.
- Cấu hình, sử dụng **Git** và xem lịch sử commit.
- Sử dụng môi trường lập trình **IDE**.
- Chạy thành công **starter project**. Báo cáo có đề cập các tệp Java `Main.java`, `CDiem.java`, `CEllipse.java`, `CHinhVe.java`, `CTamGiac.java`, `CTuGiac.java`.
- Trả lời các câu hỏi tự kiểm tra về JDK/JRE, chuỗi công cụ Java, dấu hiệu áp dụng sai hoặc quá mức, artifact và việc cập nhật khi yêu cầu thay đổi.

**Sản phẩm thể hiện trong báo cáo:** ảnh/lệnh kiểm tra công cụ, minh chứng chạy dự án, kết quả Git và câu trả lời tự kiểm tra.

**Xem:** [LAB 0 / main](https://github.com/nguyen11565/LAB_OOSD/tree/main).

## LAB 1 – Đặc tả yêu cầu Use Case

**Mục tiêu:** Phân tích bài toán, xác định tác nhân và yêu cầu chức năng; biểu diễn quy trình nghiệp vụ bằng UML.

### Bài 1. Xây dựng hệ thống thư viện trực tuyến

- Xác định yêu cầu chức năng dành cho **độc giả**, **thủ thư** và chức năng **gửi email tự động** trước hạn trả sách 3 ngày.
- Xây dựng bảng thuật ngữ cho các đối tượng như thẻ thư viện, tài liệu điện tử, mượn/trả sách, hạn trả, đặt mua tài liệu.
- Xác định Actor và lập danh sách Use Case; vẽ mô hình Use Case tổng quát.
- Đặc tả các luồng chính, luồng thay thế, tiền/hậu điều kiện và vấn đề còn mở cho các nghiệp vụ tìm kiếm, đọc/tải tài liệu, đăng ký mượn, tài khoản, đặt mua, quản lý mượn/trả và nhắc hạn.

**Lưu ý về mã Use Case:** Phần tổng hợp đầu báo cáo có bảng **UC01–UC15**, còn phần bảng đặc tả chi tiết trình bày **UC1–UC13** theo thứ tự khác. Cần đối chiếu mã và tên trước khi tái sử dụng sơ đồ/bảng đặc tả.

### Bài 2. Hệ thống quản lý cửa hàng nước giải khát

- Xác định Actor: nhân viên, nhà cung ứng, khách vãng lai và khách quen.
- Xác định chức năng tổng quát về danh mục, nhập hàng, bán hàng, tồn kho, hóa đơn, giao hàng và công nợ.
- Thiết kế các lớp nghiệp vụ cùng thuộc tính và phương thức; vẽ **Class Diagram**.
- Vẽ **Use Case Diagram** tổng quát và phân rã từ lúc đặt mua đến khi giao hàng đầy đủ.
- Vẽ **Activity Diagram** theo quy trình bán/giao hàng và **Sequence Diagram** cho nghiệp vụ lập phiếu ghi nợ.

**Sản phẩm:** nội dung phân tích, bảng thuật ngữ, bảng đặc tả Use Case, sơ đồ UML và bài giải theo từng câu.

## LAB 2 – Hệ thống quản lý thư viện

**Mục tiêu:** Mô hình hóa các luồng nghiệp vụ quản lý thư viện và thể hiện sự tương tác giữa các thành phần.

- **Activity Diagram:** quản lý nhân viên, quản lý độc giả, gia hạn thẻ, quản lý đầu sách, quản lý thể loại, quản lý nhà xuất bản và quản lý mượn trả.
- **Sequence Diagram:** mô tả trình tự trao đổi thông điệp giữa đối tượng và hệ thống cho các quy trình liên quan.
- **Giao diện:** phần `FrmMain` được thể hiện trong tài liệu ở mục thiết kế bằng Excel.

**Sản phẩm:** các hình Activity Diagram, Sequence Diagram và mô phỏng giao diện.

**Xem:** [Nhánh LAB2](https://github.com/nguyen11565/LAB_OOSD/tree/LAB2).

## LAB 3 – Hệ thống quản lý khách sạn

**Mục tiêu:** Thiết kế các màn hình chính cho nghiệp vụ quản lý và vận hành khách sạn.

Các màn hình được trình bày trong báo cáo:

- `FrmMain`: điều hướng chức năng cho nhân viên.
- `FrmDanhMuc`: quản lý các danh mục nền.
- `FrmPhongTienNghi`: quản lý phòng, tiện nghi và phiếu lắp đặt, phục vụ nghiệp vụ phòng.
- `FrmDatPhong`: thao tác khách hàng, đặt phòng và nhận phòng, dành cho lễ tân.
- `FrmDichVu`: quản lý phiếu sử dụng dịch vụ.
- `FrmTraPhong`: đền bù, hóa đơn và thanh toán khi trả phòng.
- **Cơ sở dữ liệu:** phần trình bày thiết kế/lưu trữ dữ liệu của hệ thống.

**Sản phẩm:** hình giao diện các form và phần cơ sở dữ liệu trong báo cáo.

**Xem:** [Nhánh LAB3](https://github.com/nguyen11565/LAB_OOSD/tree/LAB3).

## LAB 4 – Hệ thống cửa hàng trực tuyến E-Shopping

**Mục tiêu:** Phân tích và thiết kế quy trình đặt mua hàng và thanh toán trực tuyến.

**Phạm vi nghiệp vụ:** Khách hàng xem sản phẩm, quản lý giỏ hàng, tạo đơn, chọn phương thức giao hàng, nhập thông tin người nhận, thanh toán bằng thẻ tín dụng và nhận email xác nhận.

**Các nội dung:**

1. Khảo sát yêu cầu và xác định phạm vi hệ thống.
2. Thiết kế **Use Case Diagram tổng quát** và **Use Case phân rã**.
3. Thiết kế **Activity Diagram** cho quá trình đặt mua hàng, tính tiền.
4. Thiết kế **Sequence Diagram** cho quá trình đặt mua và thanh toán.
5. Thiết kế **Class Diagram**.
6. Thiết kế giao diện `FrmMain`, `FrmDatHang`.

**Các tác nhân/hệ thống liên quan:** khách hàng, hệ thống quản lý sản phẩm, dịch vụ thanh toán trực tuyến và dịch vụ email. Theo yêu cầu trong báo cáo, email xác nhận không chứa thông tin thẻ.

**Sản phẩm:** sơ đồ UML và hình thiết kế giao diện.

**Xem:** [Nhánh LAB4](https://github.com/nguyen11565/LAB_OOSD/tree/LAB4).

## LAB 5 – Quản lý công ty du lịch

**Mục tiêu:** Phân tích và thiết kế hệ thống tổ chức tour, bán vé khách lẻ và đăng ký tour theo đoàn.

**Nội dung:**

- Liệt kê các lớp, thuộc tính ID và phương thức: `TourDuLich`, `DiemThamQuan`, `PhuongTien`, `ChuyenKhachLe`, `VeKhachLe`, `DiemBanVe`, `KhachHang`, `PhieuDangKyDoan`, `DanhSachBaoHiem`, `NhanVienHD`, `PhieuKhaoSat` và các lớp liên quan.
- Thiết kế **Class Diagram** với quan hệ và bội số.
- Xác định Actor và thiết kế **Use Case Diagram tổng quát**.
- Phân rã quá trình từ lúc đăng ký tour (theo đoàn/theo chuyến) đến khi kết thúc tour.
- Vẽ **Activity Diagram** cho nghiệp vụ trên.
- Vẽ **Sequence Diagram** cho nghiệp vụ lập phiếu đăng ký tour theo đoàn.

**Sản phẩm:** bảng lớp và các mô hình UML tương ứng với sáu câu hỏi bài tập.

**Xem:** [Nhánh LAB5](https://github.com/nguyen11565/LAB_OOSD/tree/LAB5).

## LAB 6 – Quản lý hồ sơ bệnh án của bệnh nhân

**Mục tiêu:** Mô hình hóa hoạt động đăng ký khám, tiếp nhận, khám bệnh, xét nghiệm và kê toa thuốc.

**Nội dung:**

- Xác định lớp và các thuộc tính/phương thức của `BenhNhan`, `PhieuDangKy`, `PhieuThuLePhi`, `HoSoBenhAn`, `PhieuKhamBenh`, `BacSi`, `KhoaDieuTri`, `ToaThuoc`, `ChiTietToaThuoc`, `Thuoc`, `LoaiXetNghiem`, `PhieuXetNghiem`, `LichHen`, `NhanVienCSKH`, `HoaDonVienPhi`, `TheoDoiDieuTri`.
- Vẽ **Class Diagram** và mô tả liên kết/bội số.
- Vẽ **Use Case Diagram tổng quát**.
- Phân rã nghiệp vụ từ đăng ký khám, xét nghiệm đến lúc nhận toa thuốc.
- Vẽ **Activity Diagram** của quy trình khám bệnh.
- Vẽ **Sequence Diagram** cho nghiệp vụ bác sĩ lập toa thuốc.

**Sản phẩm:** mô hình lớp và sơ đồ UML cho quá trình quản lý bệnh án.

**Xem:** [Nhánh LAB6](https://github.com/nguyen11565/LAB_OOSD/tree/LAB6).

## LAB 7 – Quản lý nhân sự

**Mục tiêu:** Mô hình hóa các hoạt động của hệ thống quản trị nhân sự trong tổ chức.

**Nội dung:**

- Xây dựng các nhóm lớp quản lý **nhân viên, phòng ban, chức vụ, hợp đồng lao động**; **các quyết định nhân sự**; **phân công và chấm công**; **lương, thưởng và phạt**.
- Vẽ **Class Diagram** với quan hệ và bội số.
- Vẽ **Use Case Diagram tổng quát** cho toàn hệ thống.
- Phân rã quy trình từ khi nhân viên vào thử việc đến khi được ký hợp đồng chính thức.
- Vẽ **Activity Diagram** cho quy trình thử việc và ký hợp đồng.
- Vẽ **Sequence Diagram** cho nghiệp vụ lập quyết định khen thưởng đối với cá nhân hoặc tập thể/phòng ban.

**Sản phẩm:** bảng lớp, sơ đồ lớp, Use Case, Activity và Sequence Diagram.

**Xem:** [Nhánh LAB7](https://github.com/nguyen11565/LAB_OOSD/tree/LAB7).

## LAB 8 – Hệ thống quản lý cửa hàng nước giải khát

**Mục tiêu:** Tiếp tục áp dụng phương pháp phân tích thiết kế hướng đối tượng cho hệ thống quản lý cửa hàng nước giải khát; tài liệu có thêm phần giao diện.

**Nội dung:**

1. Xác định Actor, danh sách Use Case và vẽ **Use Case Diagram tổng quát**.
2. Xác định các lớp, thuộc tính, phương thức và thuộc tính định danh.
3. Vẽ **Class Diagram** với mối quan hệ và bội số.
4. Vẽ **Use Case Diagram phân rã** từ lúc khách đặt mua đến khi giao đủ hàng.
5. Vẽ **Activity Diagram** gồm kiểm tra tồn kho, phân loại khách vãng lai/khách quen, giao ngay, hẹn giao nhiều đợt hoặc lập phiếu ghi nợ.
6. Vẽ **Sequence Diagram** cho nghiệp vụ lập phiếu ghi nợ.
7. Trình bày **thiết kế giao diện `FrmMain`**.

**Sản phẩm:** các sơ đồ UML và hình giao diện hệ thống.

**Xem:** [Nhánh LAB8](https://github.com/nguyen11565/LAB_OOSD/tree/LAB8).

---

## Công cụ và kiến thức áp dụng

Các công cụ và kỹ thuật được nhắc đến trong báo cáo gồm:

- **Java Development Kit (JDK), JRE, JVM:** phục vụ phát triển và thực thi Java ở LAB 0.
- **Maven:** quản lý cấu hình/build dự án Java ở LAB 0.
- **Git/GitHub:** quản lý mã nguồn, nhánh và lịch sử thay đổi.
- **IDE / IntelliJ IDEA:** soạn thảo và chạy chương trình Java.
- **StarUML / UML:** phân tích và mô hình hóa Use Case, Class, Activity, Sequence Diagram.
- **Thiết kế giao diện:** các bản phác thảo/màn hình `Frm...` trong những LAB có yêu cầu minh họa.

> Công cụ, cách chạy và cấu trúc mã nguồn cụ thể có thể khác nhau ở từng LAB. README này mô tả **nội dung theo báo cáo tổng hợp**, không khẳng định mọi nhánh GitHub đều chứa ứng dụng hoàn chỉnh có thể chạy ngay.

## Cách xem các LAB trên GitHub

Truy cập repository [LAB_OOSD](https://github.com/nguyen11565/LAB_OOSD), sử dụng menu chọn nhánh để chuyển giữa `main`, `LAB2`, `LAB3`, `LAB4`, `LAB5`, `LAB6`, `LAB7`, `LAB8` theo đường dẫn đã ghi trong báo cáo.

Ví dụ, nếu muốn tải mã nguồn từ nhánh LAB4 bằng Git:

```bash
git clone --branch LAB4 --single-branch https://github.com/nguyen11565/LAB_OOSD.git
```

Sau khi tải về, mở thư mục phù hợp bằng IDE tương ứng. Nếu có tệp dự án như `pom.xml`, `.sln` hoặc tệp cấu hình khác, dùng công cụ tương ứng để mở và chạy theo hướng dẫn cụ thể của nhánh đó. Không có lệnh chạy chung cho mọi LAB vì các bài thực hành có phạm vi khác nhau.

## Tài liệu báo cáo

Báo cáo tổng hợp: `1250080120_HOANGTUANNGUYEN_TONG_HOP_LAB_OOSD.docx` (bản có mục lục được cung cấp riêng). Báo cáo có ảnh minh chứng, bảng đặc tả, sơ đồ UML và hình giao diện theo từng LAB.

---

**Sinh viên thực hiện:** Hoàng Tuấn Nguyên — **MSSV:** 1250080120.
