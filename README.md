# HỆ THỐNG QUẢN LÝ CÔNG TY DU LỊCH VĂN HÓA VIỆT

**Bài 6 — C# WinForms (.NET Framework 4.7.2) + SQL Server**, (quản lý tour, đoàn, chuyến, hướng dẫn viên, bảo hiểm, thanh toán và khảo sát).

> Mã nguồn và dữ liệu mẫu phục vụ học tập. Chưa được build/chạy thật trên Windows trong môi trường tạo mã nguồn; hãy thực hiện các bước kiểm chứng ở mục 5 trước khi nộp. Các test case là **kịch bản dự kiến**, không phải biên bản chạy đã đạt.

## 1. Mục tiêu / chức năng

1. CRUD 7 danh mục: tour, địa điểm tham quan, nội dung dừng chân, phương tiện, điểm bán vé, khách hàng, hướng dẫn viên.
2. Gắn điểm tham quan, nơi dừng, phương tiện theo thứ tự của tour.
3. Lập chuyến khách lẻ, tự tính ngày về; bán vé cho 1–11 người, kiểm tra sức chứa, thu đủ tiền, hủy vé trước giờ đi.
4. Lập phiếu theo đoàn từ **12 người**, chọn lịch tự do, nơi đón, nhận cọc; có bảo hiểm thì bắt buộc khai đủ thành viên. Hủy đoàn mất cọc; hoàn thành và tất toán sau chuyến.
5. Phân công HDV: **1 HDV/chuyến lẻ**, **nhiều HDV/đoàn**, chống trùng ngày đi/ngày về.
6. Báo cáo các chuyến, vé, đoàn, góp ý; lương tháng = lương cơ bản + tiền công các tour **đã hoàn thành**, quy vào tháng bắt đầu tour.
7. Ghi nhận khảo sát sau tour chỉ cho khách đã tham gia.

**Quy ước:** tài liệu gốc không nói rõ trường hợp **đúng 12 người**; ứng dụng xếp vào **đoàn** (khách lẻ 1–11). Giao dịch tiền dùng `DECIMAL(18,2)`, không dùng `float`; đơn giá chốt trong `DonGiaApDung` để lịch sử không thay đổi khi sửa giá tour. Mọi tour xuất phát từ TP.HCM theo giả định đề bài.

## 2. Công nghệ / kiến trúc

- **UI** (`Forms/*.cs`): WinForms, tạo giao diện trực tiếp bằng code (không cần `*.Designer.cs`).
- **Service** (`Services/*.cs`): xác thực dữ liệu, nghiệp vụ mua vé, đoàn, phân công, báo cáo và danh sách chọn.
- **Data** (`Data/*.cs`): `System.Data.SqlClient`, SQL tham số, transaction, thao tác dữ liệu.
- **Database**: SQL Server, 16 bảng có PK/FK, UNIQUE, CHECK, chỉ mục; 12 bảng lớp chính + 4 bảng hỗ trợ quan hệ/nghiệp vụ.

```text
UI (FrmMain, FrmDanhMuc, FrmTuyenDiem, FrmChuyenLe,
    FrmPhieuDoan, FrmPhanCong, FrmKhaoSat, FrmBaoCao)
       ↓
Service (Catalog, Itinerary, Lookup, Booking, Scheduling,
         Survey, Report)
       ↓
Data (Db, CatalogRepository)
       ↓
SQL Server (QuanLyCongTyDuLich)
```

## 3. Cấu trúc thư mục (đưa NGUYÊN THƯ MỤC lên GitHub)

```text
QuanLyCongTyDuLich/
├── .gitignore
├── README.md
├── QuanLyCongTyDuLich.sln
├── sql/
│   ├── 01_Schema.sql             # Tạo DB, 16 bảng, PK/FK/constraints
│   └── 02_Seed.sql               # Dữ liệu mẫu cụ thể
├── src/
│   └── QuanLyCongTyDuLich/
│       ├── QuanLyCongTyDuLich.csproj
│       ├── App.config            # Chỉnh kết nối SQL Server
│       ├── Program.cs
│       ├── Models/
│       │   ├── Domain.cs
│       │   └── Entities.cs
│       ├── Data/
│       │   ├── Db.cs
│       │   └── CatalogRepository.cs
│       ├── Services/
│       │   ├── CatalogService.cs
│       │   ├── LookupService.cs
│       │   ├── ItineraryService.cs
│       │   ├── BookingService.cs
│       │   ├── SchedulingService.cs
│       │   ├── SurveyService.cs
│       │   └── ReportService.cs
│       └── Forms/
│           ├── Ui.cs
│           ├── FrmMain.cs
│           ├── FrmDanhMuc.cs
│           ├── FrmTuyenDiem.cs
│           ├── FrmChuyenLe.cs
│           ├── FrmPhieuDoan.cs
│           ├── FrmPhanCong.cs
│           ├── FrmKhaoSat.cs
│           └── FrmBaoCao.cs
├── docs/
│   ├── Traceability.md           # Yêu cầu → UML → UI/Service → CSDL → TC
│   ├── ModelClass_DB.md          # Đối chiếu 16 lớp POCO với bảng SQL
│   └── uml/
│       ├── class.puml
│       ├── usecase.puml
│       ├── activity_doan.puml
│       ├── activity_khach_le.puml
│       ├── sequence_doan.puml
│       └── sequence_khach_le.puml
└── tests/
    ├── 03_SmokeTests.sql         # Các kiểm tra DB có ASSERT/THROW
    └── KichBanKiemThu.md        # 23 kịch bản, dữ liệu và đầu ra dự kiến
```

## 4. Cài đặt lần đầu

**Yêu cầu máy:** Windows 10/11, **Visual Studio 2022** đã chọn workload **.NET desktop development**, bộ **.NET Framework 4.7.2 Developer Pack/Targeting Pack**, **SQL Server 2019/2022 hoặc bản Express**, **SQL Server Management Studio (SSMS)**.

**Bước 1 — Tạo database.** Mở SSMS, kết nối vào SQL Server. Mở `sql/01_Schema.sql` → **Execute (F5)**; tiếp tục `sql/02_Seed.sql` → Execute. Database tạo tên `QuanLyCongTyDuLich`. **Không chạy lại schema trên DB đã chứa bảng.**

**Bước 2 — Chỉnh kết nối.** Trong Visual Studio, mở `src/QuanLyCongTyDuLich/App.config` và sửa thuộc tính `Data Source` trong `connectionString`:

| SQL Server đang dùng | `Data Source` |
|---|---|
| SQL Server Express | `.\SQLEXPRESS` |
| SQL Server mặc định máy | `.` |
| Server tên máy/instance khác | `TEN_MAY\TEN_INSTANCE` |

Mặc định đăng nhập Windows (`Integrated Security=True`), vì vậy SQL Server cần cho phép tài khoản Windows đang chạy ứng dụng truy cập database. Nếu gặp lỗi chứng chỉ, thuộc tính `TrustServerCertificate=True` trong bản demo dành cho mạng cục bộ; khi triển khai thật nên dùng kết nối TLS cấu hình đúng.

**Bước 3 — Mở project.** Mở `QuanLyCongTyDuLich.sln` bằng Visual Studio 2022. Khi VS yêu cầu, cài **.NET Framework 4.7.2 targeting pack**. Chọn cấu hình **Debug / Any CPU** → **Build > Build Solution** (Ctrl+Shift+B) → **Start (F5)**.

**Bước 4 — Kiểm tra.** Từ màn hình chính bấm `Kiểm tra kết nối SQL`. Sau đó mở `Danh mục` và kiểm tra có tour `T001`, `T002`, `T003`; mở `Chuyến & vé khách lẻ` xem `CH001`, `CH002`.

**Bước 5 — Chạy kiểm thử.** Mở `tests/03_SmokeTests.sql` trong SSMS → Execute. Kết quả mong đợi: `ALL SQL SMOKE TESTS PASSED.`. Sau đó chạy 23 kịch bản trong `tests/KichBanKiemThu.md` và chụp ảnh minh chứng. **Các kiểm thử chưa được xác nhận là đã chạy trên máy của bạn.**

## 5. Dữ liệu demo có sẵn

| Thực thể | Dữ liệu |
|---|---|
| Tour | `T001` Đà Lạt (3 ngày, 2 đêm, 2.500.000đ); `T002` Nha Trang (4 ngày, 3 đêm, 3.200.000đ); `T003` Mũi Né (2 ngày, 1 đêm, 1.800.000đ) |
| Khách | `KH001`, `KH002`, `KH003` |
| Hướng dẫn viên | `NV001` (8.000.000đ), `NV002` (8.500.000đ), `NV003` (7.800.000đ) |
| Chuyến | `CH001` (10–12/12/2026, 2 khách), `CH002` (01–04/07/2026, đã hoàn thành) |
| Vé | `VE001` (2 khách, 5.000.000đ), `VE002` (1 khách, 3.200.000đ) |
| Đoàn | `PD001` 15 người, 5.000.000đ cọc, đã hoàn thành; `PD002` 12 người, 6.000.000đ cọc |
| Phân công | `PC001` NV001/CH001; `PC002` NV002/CH002; `PC003` NV003/PD001 |
| Khảo sát | `KS001` (CH002); `KS002` (PD001) |

**Dữ liệu ngày được chốt ở năm 2026.** Nếu chạy sau những ngày khởi hành mẫu, chương trình không cho bán thêm vé/hủy những chuyến đã đi. Muốn thử luồng tạo mới, chọn ngày tương lai tại thời điểm kiểm thử.

## 6. Hướng dẫn thao tác / giới hạn

- **Danh mục:** chọn loại dữ liệu trên thanh trên, chọn dòng để điền các ô; Thêm mới, Cập nhật, Xóa. Không thể xóa bản ghi đã có khóa ngoại tham chiếu.
- **Tuyến và điểm tham quan:** trước tiên tạo điểm/nơi dừng/phương tiện trong Danh mục, sau đó thêm theo thứ tự tour.
- **Bán vé:** nhập mã vé mới, chọn chuyến, khách, điểm bán, số người và điểm đón. Khi bấm bán vé ứng dụng tính đơn giá và ghi nhận đã thanh toán ngay, **không** có cổng thanh toán thật.
- **Đoàn:** phải cọc trước; nếu chọn *Mua bảo hiểm*, ô danh sách nhập **mỗi người một dòng** theo cú pháp `Họ tên|Số giấy tờ|dd/MM/yyyy`, đủ bằng số người đi. Khi hoàn thành và đã qua ngày về, dùng nút `Hoàn thành tour` rồi `Tất toán sau tour`.
- **Phân công:** chuyến lẻ chỉ nhận 1 hướng dẫn viên; đoàn nhận được nhiều hướng dẫn viên nhưng mỗi hướng dẫn viên phải trống lịch.
- **Báo cáo lương:** chọn **Bảng lương theo tháng** và tháng/năm; chỉ tính khoản tour hoàn thành trong tháng khởi hành.
- **Bảo mật:** đây là bài học, chưa có chức năng phân quyền đăng nhập, audit log hay tích hợp ngân hàng; không nhập dữ liệu khách hàng thật.

## 7. Cách xem sơ đồ UML

Mở file `docs/uml/*.puml` với VS Code extension **PlantUML**, hoặc dán mã vào PlantUML, hoặc vào draw.io → **Arrange → Insert → Advanced → PlantUML** (tùy phiên bản). Sơ đồ được lưu dưới dạng mã văn bản có thể chỉnh sửa. Gồm Class Diagram, Use Case tổng quát, hai Activity và hai Sequence theo Boundary–Control–Entity.

## 8. Đưa lên GitHub

1. Giải nén ZIP rồi **giữ nguyên thư mục `QuanLyCongTyDuLich`**.
2. Vào GitHub → **New repository** → đặt tên `QuanLyCongTyDuLich`.
3. Vào repo → **Add file → Upload files**; kéo toàn bộ nội dung trong folder lên GitHub (gồm `.sln`, `src`, `sql`, `docs`, `tests`, README). Không cần upload `bin`/`obj` vì `.gitignore` đã loại trừ.
4. Commit với thông điệp `Hoan thanh Bai 6 - Quan ly cong ty du lich`.
5. Trước khi nộp, chạy thử 23 kịch bản, ghi kết quả và thêm ảnh minh chứng nếu giảng viên yêu cầu.

## 9. Lưu ý học thuật

- **12 lớp chính** trong tài liệu: `TourDuLich`, `DiemThamQuan`, `NoiDungChan`, `PhuongTien`, `ChuyenKhachLe`, `VeKhachLe`, `DiemBanVe`, `KhachHang`, `PhieuDangKyDoan`, `DanhSachBaoHiem`, `NhanVienHD`, `PhieuKhaoSat`.
- Các bảng còn lại là bảng liên kết hoặc phát sinh từ nghiệp vụ: `TourDiemThamQuan`, `TourNoiDungChan`, `ThanhToanDoan`, `PhanCongHuongDan`.
- Một số quy tắc triển khai được quy định thêm vì đề không nói rõ, được liệt kê trong `docs/Traceability.md`.
- Project **không giả vờ là đã triển khai website**; đây là WinForms desktop theo đúng phần hiện thực giảng viên yêu cầu.
