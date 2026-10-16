# LAB6 — HỆ THỐNG QUẢN LÝ HỒ SƠ BỆNH ÁN

**Ngôn ngữ:** C# WinForms — **.NET Framework 4.7.2**  
**CSDL:** Microsoft SQL Server 2017+ (ADO.NET / `System.Data.SqlClient`)  
**Kiến trúc:** `UI → Service → Data → SQL Server`  
**Tài liệu gốc:** `Docs/DeBaiGoc.docx` (16 lớp, Class / Use Case / Activity / Sequence)

> Bản triển khai phục vụ bài thực hành / học tập. Đây không phải hệ thống bệnh án được đánh giá bảo mật, an toàn hoặc chứng nhận sử dụng trong bệnh viện thật. **Chưa xác minh biên dịch và chạy SQL Server trực tiếp trong môi trường xây dựng ZIP; cần thực hiện bước kiểm tra tại máy Windows trước khi nộp.**

## 1. Cấu trúc thư mục đầy đủ

```text
QuanLyHoSoBenhAn_LAB6/
├── README.md
├── .gitignore
├── Database/
│   ├── 01_Schema.sql                  # Tạo database + 16 bảng, PK/FK/CHECK/UNIQUE
│   ├── 02_Procedures.sql              # Stored procedures, transaction, nghiệp vụ
│   └── 03_Seed.sql                    # Khoa, bác sĩ, CSKH, thuốc, loại xét nghiệm
├── Docs/
│   ├── DeBaiGoc.docx                 # Tài liệu UML / mô tả do người dùng cung cấp
│   ├── StaticCheckResult.txt         # Kết quả kiểm tra cấu trúc tại môi trường tạo ZIP
│   ├── DacTaNghiepVu.md              # Các quy tắc và luồng nghiệp vụ
│   ├── QuyetDinhThietKe.md           # Diễn giải các lựa chọn mở rộng so với đề bài
│   ├── TruyVetYeuCau.md              # Yêu cầu → UML → Form → Service → SQL → Test
│   ├── TestCases.md                  # 12 test case với dữ liệu / mong đợi
│   └── HuongDanGitHub.md
├── Tests/
│   ├── 00_StaticChecks.py            # Kiểm tra cấu trúc offline (không thay build/runtime)
│   └── 01_TestCases.sql              # 12 nhóm kiểm thử tích hợp tự kiểm tra PASS/FAIL
├── UML/
│   ├── 01_ClassDiagram.puml
│   ├── 02_UseCase_TongQuat.puml
│   ├── 03_UseCase_PhanRaTiepNhan.puml
│   ├── 04_Activity_TiepNhan.puml
│   ├── 05_Activity_KhamToaVienPhi.puml
│   └── 06_Sequence_LapToaThuoc.puml
└── src/
    ├── QuanLyHoSoBenhAn.sln
    └── QuanLyHoSoBenhAn/
        ├── QuanLyHoSoBenhAn.csproj   # Windows Forms, .NET Framework 4.7.2
        ├── App.config                 # Chỉnh Data Source SQL Server
        ├── Program.cs
        ├── Properties/
        │   └── AssemblyInfo.cs
        ├── Models/
        │   └── Contracts.cs          # DTO cho đăng ký, khám, kê toa
        ├── Data/
        │   └── Db.cs                  # ADO.NET connection, Proc, Query, Execute
        ├── Services/
        │   └── BusinessServices.cs    # 8 lớp dịch vụ nghiệp vụ
        └── Forms/
            ├── BaseForm.cs
            ├── FrmMain.cs
            ├── FrmDangKy.cs
            ├── FrmLichHen.cs
            ├── FrmKhamBenh.cs
            ├── FrmXetNghiem.cs
            ├── FrmToaThuoc.cs
            ├── FrmVienPhi.cs
            ├── FrmLichSu.cs
            └── FrmDanhMuc.cs
```

## 2. Cài đặt môi trường

1. Windows 10/11; **Visual Studio 2022** có workload **.NET desktop development** và **.NET Framework 4.7.2 Developer/Targeting Pack**.
2. SQL Server 2017+ (`SQL Server Express` cũng được), SQL Server Management Studio (SSMS).
3. Mở SSMS, kết nối tài khoản có quyền tạo database, chạy theo đúng thứ tự **từng file riêng** (nhấn `Execute`):
   1. `Database/01_Schema.sql`
   2. `Database/02_Procedures.sql`
   3. `Database/03_Seed.sql`
4. Mở `src/QuanLyHoSoBenhAn/App.config` để sửa kết nối:

   - SQL Server mặc định trên cùng máy: `Data Source=.;Initial Catalog=QuanLyHoSoBenhAn_LAB6;Integrated Security=True;Encrypt=False`
   - SQL Server Express: `Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyHoSoBenhAn_LAB6;Integrated Security=True;Encrypt=False`
   - Instance đặt tên khác: thay phần sau `Data Source=` bằng tên instance nhìn thấy ở SSMS.

   Nếu dùng SQL Authentication, chỉnh chuỗi thành `Data Source=TEN_SERVER;Initial Catalog=QuanLyHoSoBenhAn_LAB6;User ID=...;Password=...;Encrypt=False` và **không commit mật khẩu lên GitHub**.
5. Double-click `src/QuanLyHoSoBenhAn.sln` để mở Visual Studio.
6. Trong `Solution Explorer`, chọn project `QuanLyHoSoBenhAn` → `Set as Startup Project`; chọn `Build → Build Solution` rồi nhấn **F5** hoặc `Ctrl + F5`.
7. Nếu Visual Studio báo thiếu Targeting Pack, vào Visual Studio Installer → Modify → Individual components → cài **.NET Framework 4.7.2 targeting pack** rồi mở lại.

**Lưu ý:** Các Form được tạo hoàn toàn bằng C# nên không cần file `.Designer.cs`. Không được mở file `App.config` và xóa nội dung `<connectionStrings>`.

## 3. Các chức năng WinForms

| Tab | Màn hình | Chức năng |
|---|---|---|
| 1 | `FrmDangKy` | Đăng ký lần đầu (thu phí năm + cấp hồ sơ trong một giao dịch), gia hạn phí |
| 2 | `FrmLichHen` | Đặt / hủy lịch khám, kiểm tra quyền định kỳ từng quý |
| 3 | `FrmKhamBenh` | Mở phiếu khám, ghi triệu chứng, nhiệt độ, chẩn đoán, hoàn tất |
| 4 | `FrmXetNghiem` | Chỉ định loại xét nghiệm, nhập kết quả |
| 5 | `FrmToaThuoc` | Lập toa, thêm từng thuốc, xem trước/in toa |
| 6 | `FrmVienPhi` | Lập hóa đơn, xác nhận nhận tiền, hoàn tất khám, theo dõi điều trị |
| 7 | `FrmLichSu` | Tìm hồ sơ bằng mã/tên/điện thoại, xem lịch sử, xét nghiệm, toa, theo dõi |
| 8 | `FrmDanhMuc` | Xem / thêm khoa, bác sĩ, thuốc, loại xét nghiệm, nhân viên CSKH |

> **Đặc điểm triển khai:** Form gọi Service; Service dùng `Db.Proc` / `Db.Query` đã tham số hóa; nghiệp vụ quan trọng được thực thi tập trung trong stored procedure (không tách nhỏ transaction đăng ký).

## 4. Quy trình demo hoàn chỉnh

Để quay video hoặc chụp màn hình nộp bài:

1. **Danh mục:** Xem sẵn 3 khoa, 3 bác sĩ, 3 thuốc, 3 xét nghiệm, 1 nhân viên CSKH.
2. **Đăng ký:** Nhập bệnh nhân mới `Trần Văn A`, ngày sinh `20/05/2000`, điện thoại `0912345678`, địa chỉ `TP.HCM`, phí năm `500000` → nhấn **Đăng ký + thu phí + cấp hồ sơ**; lưu **Mã hồ sơ** được trả về. Nếu chạy demo lần nữa, đổi số điện thoại cho bệnh nhân để tránh trùng.
3. **Lịch:** Nhập mã hồ sơ, chọn bác sĩ và ngày giờ **trong tương lai**. Chọn `DINH_KY` để kiểm tra quyền khám định kỳ theo quý. Có thể đăng ký khám trực tiếp không qua hẹn.
4. **Khám:** Nhập mã hồ sơ, chọn bác sĩ **thuộc đúng khoa**, chọn `KHAM_BENH` rồi **Mở phiếu khám**. Điền triệu chứng, nhiệt độ, huyết áp, chẩn đoán, kết luận; nhấn **Lưu nội dung khám**. Ghi nhớ **Mã phiếu khám**.
5. **Xét nghiệm:** Nhập mã phiếu khám, chọn `Công thức máu` → chỉ định. Chọn dòng phiếu xét nghiệm → nhập kết quả và kết luận → **Ghi kết quả**.
6. **Toa:** Nhập mã phiếu khám → **Tạo toa** → chọn Paracetamol, số lượng 10, liều dùng 1 viên, 2 lần/ngày, sau ăn, 5 ngày → **Thêm thuốc** → **Xem trước/In toa**.
7. **Viện phí:** Nhập mã phiếu khám → **Lập hóa đơn** (phí khám bệnh mẫu 150.000đ + công thức máu 120.000đ = **270.000đ**). Chọn hóa đơn để tự điền số tiền nhận → **Xác nhận thanh toán** → **Hoàn tất phiếu khám** → ghi theo dõi diễn biến (sau khi hoàn tất).
8. **Tra cứu:** Nhập mã hồ sơ → **Lịch sử khám**, **Kết quả xét nghiệm**, **Chi tiết toa thuốc** và **Theo dõi điều trị**.
9. **Định kỳ miễn phí:** Mở một phiếu `DINH_KY` cho hồ sơ đang còn hiệu lực (mỗi quý tối đa một phiếu). Không chỉ định xét nghiệm → lập hóa đơn **0đ**, nhập tiền nhận 0 → xác nhận → hoàn tất. Nếu chỉ định xét nghiệm, tiền khám vẫn 0đ nhưng xét nghiệm tính theo danh mục.

### Quy tắc đặc biệt để giải thích khi thầy hỏi

- **Đăng ký lần đầu:** 4 bảng `BenhNhan` + `PhieuDangKy` + `PhieuThuLePhi` + `HoSoBenhAn` cùng transaction; nếu một bước lỗi thì rollback toàn bộ.
- **Lệ phí năm:** Hiệu lực từ ngày bắt đầu, hết hạn vào ngày kỷ niệm năm kế tiếp (ngày hết hạn không còn hiệu lực). Gia hạn khi phí cũ còn hạn sẽ nối tiếp từ ngày hết hạn, không mất ngày còn lại.
- **Khám định kỳ:** Mỗi hồ sơ chỉ được tối đa một lần khám định kỳ trong **một quý dương lịch**, phải có lệ phí năm hiệu lực. Hủy lịch đang chờ khám thì được đặt lại.
- **Xét nghiệm / toa thuốc:** Có `MaPhieuKham` bắt buộc, chi tiết toa liên kết `MaToaThuoc` và `MaThuoc`.
- **Hóa đơn:** Tiền khám bệnh mẫu `150000`; khám định kỳ tiền khám = `0`; giá xét nghiệm được chụp tại thời điểm chỉ định (`DonGiaApDung`). Không thêm thuốc/XN khi đã lập hóa đơn.
- **Hoàn tất:** `HoaDonVienPhi.TrangThaiThanhToan = 'DA_THANH_TOAN'`; hóa đơn 0đ cũng phải xác nhận 0đ. Chỉ nhân viên CSKH ghi theo dõi sau khi hoàn tất.

## 5. Kiểm thử

Mở SSMS, chọn **đúng database bài tập**, chạy `Tests/01_TestCases.sql`. Có **12 nhóm test case**, script tự tạo 2 bệnh nhân mẫu với số điện thoại ngẫu nhiên để tránh trùng, kiểm tra nhiều `ASSERT` và in `TCxx PASS` nếu điều kiện đạt. **Dữ liệu mẫu được lưu lại** để tra cứu từ WinForms; **không chạy trên dữ liệu bệnh nhân thật**.

| Nhóm | Nội dung |
|---|---|
| TC01–TC02 | Đăng ký lần đầu; chống trùng |
| TC03–TC05 | Lịch định kỳ; hủy/đặt lại; miễn tiền khám; 1 lần/quý |
| TC06–TC07 | Phiếu khám → chỉ định XN → kết quả → toa; tính tiền viện phí |
| TC08 | Từ chối trả thiếu/chưa trả; thanh toán → hoàn tất → theo dõi |
| TC09–TC10 | Gia hạn đúng ngày; tra cứu lịch sử liên kết |
| TC11–TC12 | Chặn định kỳ khi phí hết hạn; bác sĩ phải thuộc đúng khoa |

Kết quả **dự kiến**: in `TC01 PASS` … `TC12 PASS` và `12/12 ASSERTION GROUPS PASS`. Chưa ghi nhận kết quả chạy thực tế. Nếu có lỗi, ghi lại số `Msg`, dòng `Line`, phần `Messages` của SSMS để kiểm tra.

Đã thực hiện kiểm tra cấu trúc offline bằng `Tests/00_StaticChecks.py`; log nằm trong `Docs/StaticCheckResult.txt`. Kiểm tra này **không thay thế** build C# hay chạy SQL Server.

Xem dữ liệu đầu vào/kết quả mong đợi ở `Docs/TestCases.md`; bảng truy vết theo cấu trúc yêu cầu → UML → Form → Service → CSDL → Test ở `Docs/TruyVetYeuCau.md`.

## 6. Sơ đồ UML

Các sơ đồ `.puml` mở bằng extension PlantUML trong VS Code hoặc qua PlantUML để xuất PNG/SVG:

- `01_ClassDiagram`: 16 lớp và bội số quan hệ (1–1; 1–N; 0–1).
- `02_UseCase_TongQuat`: các Actor trong tài liệu, include/extend.
- `03_UseCase_PhanRaTiepNhan`: phân rã đăng ký khám → khám → XN → toa.
- `04_Activity_TiepNhan`: đăng ký + lệ phí năm + cấp hồ sơ.
- `05_Activity_KhamToaVienPhi`: khám, kết quả XN, kê toa, thanh toán, hoàn tất.
- `06_Sequence_LapToaThuoc`: `FrmToaThuoc → ToaThuocService → Db → SQL Server`.

Chú ý: tài liệu gốc cho các thuộc tính `Ma...` kiểu `String`, còn cơ sở dữ liệu sử dụng `INT IDENTITY` nhất quán toàn bộ FK. Chi tiết trong `Docs/QuyetDinhThietKe.md`.

## 7. Phạm vi đã thực hiện / chưa thực hiện

**Có mã nguồn:** 16 lớp entity C#, CSDL 16 bảng và constraints, stored procedures có transaction, UI 8 phân hệ, Services và DTO, dữ liệu danh mục mẫu, UML `.puml`, truy vết và test SQL. **Cần kiểm chứng trên máy người nộp:** chạy SQL Server thực tế, build Visual Studio, chụp màn hình chạy và bổ sung kết quả kiểm thử thực tế. **Ngoài phạm vi bài tập:** đăng nhập/role, xử lý ảnh 3x4 trực tiếp (bản demo chỉ lưu đường dẫn), tích hợp cổng thanh toán, kho thuốc, lưu trữ dữ liệu y tế có bảo mật cấp bệnh viện, quản lý thiết bị xét nghiệm, kế toán thuế, kiểm thử tự động GUI.

## 8. Đưa lên GitHub

Xem `Docs/HuongDanGitHub.md`. **Đưa toàn bộ thư mục** lên repository, đặc biệt `README.md`, `src`, `Database`, `UML`, `Docs`, `Tests`; không đưa file `bin/`, `obj/`, `.vs/` và mật khẩu thật.
