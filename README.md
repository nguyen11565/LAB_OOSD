# LAB 3 - HỆ THỐNG QUẢN LÝ KHÁCH SẠN

## 1. Thông tin sinh viên

| Thông tin                   | Nội dung                               |
| --------------------------- | -------------------------------------- |
| **Họ và tên**               | **Hoàng Tuấn Nguyên**                  |
| **MSSV**                    | **1250080120**                         |
| **Tên bài Lab**             | **Lab 3 - Hệ thống quản lý khách sạn** |
| **Loại ứng dụng**           | Windows Forms Application              |
| **Ngôn ngữ**                | C#                                     |
| **Cơ sở dữ liệu**           | Microsoft SQL Server                   |
| **Công nghệ truy cập CSDL** | ADO.NET                                |
| **IDE**                     | Visual Studio 2022                     |

---

## 2. Giới thiệu

Bài Lab xây dựng một hệ thống **quản lý khách sạn** bằng C# Windows Forms kết hợp với Microsoft SQL Server.

Hệ thống được thiết kế theo mô hình phân tầng:

```text
┌──────────────────────────────┐
│       WinForms UI            │
│          Forms               │
└──────────────┬───────────────┘
               │
               ↓
┌──────────────────────────────┐
│    Service / Business Logic  │
│          Services            │
└──────────────┬───────────────┘
               │
               ↓
┌──────────────────────────────┐
│        Data Access           │
│          Data / Db.cs        │
└──────────────┬───────────────┘
               │
               ↓
┌──────────────────────────────┐
│        SQL Server            │
│      QuanLyKhachSan          │
└──────────────────────────────┘
```

Form giao diện không thực hiện SQL trực tiếp mà thông qua tầng Service và Data Access.

---

## 3. Môi trường và phiên bản

### Phần mềm

* **Visual Studio 2022**
* **C#**
* **.NET Framework 4.7.2**
* **Microsoft SQL Server**
* **SQL Server Management Studio (SSMS)** hoặc công cụ quản lý SQL Server tương đương
* **ADO.NET**
* **Windows Forms**

### Kiến trúc dự án

```text
QuanLyKhachSan
│
├── Data
│   └── Db.cs
│
├── Services
│   ├── DanhMucService.cs
│   ├── PhongTienNghiService.cs
│   ├── DatPhongService.cs
│   ├── DichVuService.cs
│   ├── TraPhongService.cs
│   └── ThongKeService.cs
│
├── Forms
│   ├── FrmMain.cs
│   ├── FrmDanhMuc.cs
│   ├── FrmPhongTienNghi.cs
│   ├── FrmDatPhong.cs
│   ├── FrmDichVu.cs
│   ├── FrmTraPhong.cs
│   └── FrmThongKe.cs
│
├── Program.cs
├── Models.cs
├── App.config
└── README.md
```

---

# 4. Nội dung đã thực hiện

## 4.1. Thiết kế cơ sở dữ liệu SQL Server

Cơ sở dữ liệu được thiết kế để quản lý các nghiệp vụ chính của khách sạn.

Các nhóm dữ liệu chính gồm:

* Khu vực
* Phòng
* Loại tiện nghi
* Tiện nghi
* Lắp đặt/luân chuyển tiện nghi
* Khách hàng
* Nhân viên
* Đặt phòng
* Chi tiết đặt phòng
* Người lưu trú
* Dịch vụ
* Phiếu dịch vụ
* Chi tiết dịch vụ
* Quy định đền bù
* Phiếu đền bù
* Chi tiết đền bù
* Hóa đơn
* Thanh toán

CSDL sử dụng:

* **Primary Key (PK)** để định danh bản ghi.
* **Foreign Key (FK)** để liên kết giữa các bảng.
* Các ràng buộc dữ liệu để đảm bảo tính hợp lệ.
* Transaction cho các nghiệp vụ có nhiều bước cập nhật dữ liệu.

---

## 4.2. Các nghiệp vụ chính

### Quản lý danh mục

Module `FrmDanhMuc` quản lý:

* Khu vực
* Nhân viên
* Loại tiện nghi
* Dịch vụ
* Quy định đền bù

---

### Quản lý phòng và tiện nghi

Module `FrmPhongTienNghi` thực hiện:

* Thêm phòng.
* Quản lý sức chứa phòng.
* Quản lý đơn giá phòng.
* Thêm tiện nghi.
* Quản lý tình trạng tiện nghi.
* Lắp đặt tiện nghi vào phòng.
* Luân chuyển tiện nghi.

---

### Quản lý đặt phòng

Module `FrmDatPhong` thực hiện:

* Thêm khách hàng.
* Chọn khách hàng.
* Chọn nhân viên.
* Chọn kênh đặt phòng.
* Chọn ngày nhận/trả phòng.
* Chọn phòng.
* Nhập số người.
* Lập phiếu đặt phòng.
* Nhận phòng.
* Quản lý người lưu trú.

Hệ thống kiểm tra:

```text
Số người lưu trú <= Sức chứa phòng
```

và kiểm tra tình trạng trùng lịch trước khi xác nhận đặt phòng.

---

### Quản lý dịch vụ

Module `FrmDichVu` thực hiện:

* Chọn phiếu lưu trú đang ở.
* Hiển thị phòng hiện tại.
* Chọn dịch vụ.
* Chọn ngày sử dụng.
* Nhập số lượng.
* Chọn nhân viên.
* Ghi nhận dịch vụ.

Nếu cùng một dịch vụ được sử dụng nhiều lần trong cùng ngày, hệ thống cộng dồn:

```text
SoLuong mới = SoLuong cũ + SoLuong phát sinh
```

---

### Trả phòng

Module `FrmTraPhong` thực hiện:

* Chọn phiếu đang ở.
* Chọn phòng.
* Kiểm tra tiện nghi.
* Ghi nhận tài sản hỏng/mất.
* Lập phiếu đền bù.
* Lập hóa đơn.
* Tính tiền phòng.
* Tính tiền dịch vụ.
* Ghi nhận nhiều giao dịch thanh toán.
* Kiểm tra tổng tiền đã thanh toán.
* Cho phép trả phòng khi hóa đơn đã thanh toán đầy đủ.

---

### Thống kê

Module `FrmThongKe` thực hiện thống kê theo khoảng thời gian:

* Phiếu đặt.
* Phiếu đang ở.
* Hóa đơn.
* Doanh thu.
* Đền bù.
* Số lượng dịch vụ.
* Tiền dịch vụ.

---

# 5. Các quy tắc nghiệp vụ đã kiểm thử

## 5.1. Kiểm tra sức chứa phòng

Khi đặt phòng hoặc thêm người lưu trú, hệ thống kiểm tra số người với sức chứa của phòng.

Ví dụ:

```text
Sức chứa phòng: 4 người
Số người đăng ký: 5 người
```

Kết quả:

```text
Từ chối thao tác.
```

Nếu:

```text
Sức chứa phòng: 4 người
Số người đăng ký: 3 người
```

thì thao tác được chấp nhận.

---

## 5.2. Kiểm tra trùng lịch

Hệ thống kiểm tra các khoảng thời gian đặt phòng.

Nếu phòng đã được đặt trong khoảng thời gian có giao nhau thì không cho phép đặt tiếp.

Ví dụ:

```text
Phòng P101
Ngày nhận: 01/09/2026
Ngày trả: 05/09/2026
```

Một yêu cầu đặt khác:

```text
Ngày nhận: 03/09/2026
Ngày trả: 07/09/2026
```

Hai khoảng thời gian bị giao nhau nên hệ thống từ chối đặt phòng.

---

## 5.3. Kiểm tra một thiết bị / một phòng / một ngày

Khi lắp đặt hoặc luân chuyển tiện nghi, hệ thống kiểm tra để tránh trường hợp một tiện nghi bị gán đồng thời cho nhiều phòng trong cùng thời điểm.

Quy tắc cần đảm bảo:

```text
Một tiện nghi
      ↓
Không được thuộc nhiều phòng
trong cùng một thời điểm
```

---

## 5.4. Cộng dồn dịch vụ cùng ngày

Nếu khách sử dụng cùng một dịch vụ nhiều lần trong cùng ngày:

```text
Lần 1:
Nước suối = 2

Lần 2:
Nước suối = 3
```

Hệ thống không tạo bản ghi dịch vụ trùng mà cập nhật:

```text
Tổng số lượng = 2 + 3 = 5
```

---

## 5.5. Hóa đơn

Hóa đơn bao gồm:

```text
Tiền phòng
+
Tiền dịch vụ
+
Các khoản đền bù nếu có
```

Tiền phòng được tính dựa trên:

```text
Đơn giá phòng × Số ngày
```

Tiền dịch vụ được tổng hợp từ các dịch vụ khách đã sử dụng.

---

## 5.6. Nhiều phương thức thanh toán

Một hóa đơn có thể được thanh toán bằng nhiều giao dịch.

Ví dụ:

```text
Tổng hóa đơn: 2.000.000 VNĐ

Lần 1:
Tiền mặt:       1.000.000 VNĐ

Lần 2:
Chuyển khoản:   1.000.000 VNĐ

Tổng thanh toán:
2.000.000 VNĐ
```

Khi:

```text
Tổng tiền thanh toán = Tổng tiền hóa đơn
```

hóa đơn chuyển sang trạng thái:

```text
Đã thanh toán
```

Nếu tổng tiền thanh toán nhỏ hơn hóa đơn thì vẫn còn nợ.

Nếu số tiền thanh toán vượt quá tổng hóa đơn thì hệ thống từ chối giao dịch.

---

## 5.7. Điều kiện trả phòng

Không cho phép trả phòng khi hóa đơn chưa thanh toán đầy đủ.

Quy trình:

```text
Hóa đơn
   ↓
Thanh toán
   ↓
Kiểm tra tổng tiền
   ↓
Đã thanh toán đủ?
   ├── Không → Không cho trả phòng
   │
   └── Có
        ↓
     Trả phòng
        ↓
   Giải phóng phòng
```

---

# 6. Các Form đã xây dựng

## `FrmMain`

Form chính dùng để điều hướng đến các module.

---

## `FrmDanhMuc`

Quản lý:

```text
Khu vực
Nhân viên
Loại tiện nghi
Dịch vụ
Quy định đền bù
```

---

## `FrmPhongTienNghi`

Quản lý:

```text
Phòng
Tiện nghi
Lắp đặt / luân chuyển
```

---

## `FrmDatPhong`

Quản lý:

```text
Khách hàng
Đặt phòng
Nhận phòng
Người lưu trú
```

---

## `FrmDichVu`

Quản lý:

```text
Phiếu đang ở
Dịch vụ
Số lượng dịch vụ
Lịch sử sử dụng
```

---

## `FrmTraPhong`

Quản lý:

```text
Đền bù
Hóa đơn
Thanh toán
Trả phòng
```

---

## `FrmThongKe`

Thống kê:

```text
Phiếu đặt
Lượt đang ở
Hóa đơn
Doanh thu
Đền bù
Dịch vụ
```

---

# 7. Kết quả đạt được

Sau khi hoàn thành, hệ thống có thể thực hiện được quy trình chính:

```text
Quản lý danh mục
       ↓
Quản lý phòng + tiện nghi
       ↓
Tạo khách hàng
       ↓
Đặt phòng
       ↓
Kiểm tra sức chứa + trùng lịch
       ↓
Nhận phòng
       ↓
Ghi nhận người lưu trú
       ↓
Sử dụng dịch vụ
       ↓
Kiểm tra tiện nghi
       ↓
Lập đền bù nếu có
       ↓
Lập hóa đơn
       ↓
Thanh toán
       ↓
Kiểm tra thanh toán đủ
       ↓
Trả phòng
       ↓
Giải phóng phòng
```

Các quy tắc nghiệp vụ quan trọng được kiểm tra thông qua các test case:

* Đặt phòng khi phòng còn trống.
* Không cho vượt sức chứa.
* Không cho đặt phòng bị trùng lịch.
* Nhận phòng từ trạng thái `Đã đặt`.
* Không cho thêm người khi đã đủ sức chứa.
* Ghi nhận đền bù.
* Lập hóa đơn tiền phòng + dịch vụ.
* Thanh toán một phần.
* Thanh toán đủ.
* Không cho trả phòng khi chưa thanh toán đủ.
* Cho phép trả phòng sau khi thanh toán đủ.

---

# 8. Lỗi gặp phải và cách khắc phục

## 8.1. Thiếu thư viện `System.Configuration`

### Hiện tượng

Visual Studio báo lỗi tại:

```csharp
ConfigurationManager.ConnectionStrings
```

### Nguyên nhân

Project chưa tham chiếu `System.Configuration`.

### Cách khắc phục

Trong **Solution Explorer**:

```text
References
→ Add Reference
→ Assemblies
→ Framework
→ System.Configuration
→ OK
```

Sau đó Build lại project.

---

## 8.2. Lỗi chuỗi kết nối SQL Server

### Hiện tượng

Không kết nối được cơ sở dữ liệu.

### Nguyên nhân

Thông tin Server/Database trong `App.config` không đúng với máy đang chạy.

### Cách khắc phục

Kiểm tra:

```xml
<connectionStrings>
    ...
</connectionStrings>
```

Đảm bảo:

* Tên SQL Server chính xác.
* Database `QuanLyKhachSan` đã được tạo.
* SQL Server đang chạy.
* Tài khoản có quyền truy cập database.

---

## 8.3. Lỗi chưa tạo database

### Hiện tượng

Ứng dụng chạy nhưng báo không tìm thấy bảng hoặc database.

### Cách khắc phục

Mở SQL Server Management Studio:

```text
1. Kết nối SQL Server
2. Mở file script Database/QuanLyKhachSan.sql
3. Execute
4. Kiểm tra database QuanLyKhachSan
5. Kiểm tra các bảng
```

Sau đó chạy lại ứng dụng.

---

## 8.4. Lỗi ComboBox không hiển thị đúng dữ liệu

Khi sử dụng ComboBox với DataTable cần cấu hình đúng:

```csharp
cboKhach.DisplayMember = "HoTen";
cboKhach.ValueMember = "MaKhach";
```

Tương tự với nhân viên, dịch vụ, phiếu đặt và các danh mục khác.

---

## 8.5. Lỗi dữ liệu nghiệp vụ

Các thao tác nghiệp vụ được kiểm tra trước khi ghi dữ liệu, ví dụ:

```text
Kiểm tra sức chứa
Kiểm tra trùng lịch
Kiểm tra trạng thái phiếu
Kiểm tra số lượng dịch vụ
Kiểm tra số tiền thanh toán
Kiểm tra trạng thái hóa đơn
```

Việc xử lý nghiệp vụ được đặt trong tầng `Services`, thay vì viết SQL trực tiếp trong Form.

---

# 9. Hướng dẫn cài đặt và chạy lại

## Bước 1. Chuẩn bị

Cài đặt:

* Visual Studio 2022.
* .NET Framework phù hợp với project.
* SQL Server.
* SQL Server Management Studio.

---

## Bước 2. Mở project

Mở file:

```text
QuanLyKhachSan.sln
```

bằng:

```text
Visual Studio 2022
```

---

## Bước 3. Kiểm tra cấu trúc project

Trong Solution Explorer cần có:

```text
Data
Services
Forms
Program.cs
Models.cs
App.config
```

---

## Bước 4. Tạo database

Mở:

```text
Database/QuanLyKhachSan.sql
```

trong SQL Server Management Studio.

Chọn đúng SQL Server instance rồi chạy:

```text
Execute
```

Kiểm tra database:

```text
QuanLyKhachSan
```

đã xuất hiện trong SQL Server hay chưa.

---

## Bước 5. Kiểm tra `App.config`

Mở:

```text
App.config
```

Kiểm tra connection string.

Ví dụ:

```xml
<connectionStrings>
    <add name="QLKS"
         connectionString="Data Source=.;Initial Catalog=QuanLyKhachSan;Integrated Security=True"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

Nếu máy sử dụng SQL Server instance khác thì thay `Data Source` cho phù hợp.

Ví dụ:

```text
.\SQLEXPRESS
```

hoặc:

```text
(localdb)\MSSQLLocalDB
```

---

## Bước 6. Build project

Trong Visual Studio:

```text
Build
→ Build Solution
```

Hoặc nhấn:

```text
Ctrl + Shift + B
```

Đảm bảo project không còn lỗi Build.

---

## Bước 7. Chạy chương trình

Nhấn:

```text
F5
```

hoặc:

```text
Ctrl + F5
```

Sau khi chạy, form chính `FrmMain` được mở.

---

# 10. Hướng dẫn kiểm tra chức năng

Giảng viên có thể kiểm tra theo thứ tự sau.

### Test 1 - Danh mục

Vào:

```text
Danh mục
```

Kiểm tra:

* Thêm khu vực.
* Thêm nhân viên.
* Thêm loại tiện nghi.
* Thêm dịch vụ.
* Thêm quy định đền bù.

---

### Test 2 - Phòng

Vào:

```text
Phòng - Tiện nghi
```

Kiểm tra:

* Thêm phòng.
* Thiết lập sức chứa.
* Thiết lập đơn giá.
* Thêm tiện nghi.
* Lắp đặt tiện nghi.

---

### Test 3 - Đặt phòng

Vào:

```text
Đặt phòng - Nhận phòng
```

Kiểm tra:

1. Chọn khách hàng.
2. Chọn nhân viên.
3. Chọn ngày nhận/trả.
4. Chọn phòng.
5. Nhập số người.
6. Lập phiếu.

Sau đó thử đặt cùng phòng với khoảng thời gian bị trùng để kiểm tra hệ thống từ chối.

---

### Test 4 - Kiểm tra sức chứa

Ví dụ phòng có:

```text
Sức chứa = 4
```

Thử nhập:

```text
Số người = 5
```

Kết quả mong đợi:

```text
Không cho phép đặt/nhận thêm người.
```

---

### Test 5 - Dịch vụ

Sau khi khách đã ở:

```text
Đặt phòng
→ Nhận phòng
→ Dịch vụ
```

Chọn dịch vụ và nhập số lượng.

Thực hiện ghi cùng một dịch vụ hai lần trong cùng ngày để kiểm tra chức năng cộng dồn số lượng.

---

### Test 6 - Đền bù

Vào:

```text
Trả phòng
```

Chọn phiếu đang ở → chọn phòng → kiểm tra tiện nghi.

Nếu có tài sản hỏng/mất:

```text
Nhập mức độ
→ Nhập tiền đền bù
→ Thêm đền bù
→ Lập phiếu đền bù
```

---

### Test 7 - Hóa đơn

Lập hóa đơn và kiểm tra:

```text
Tiền phòng
+
Tiền dịch vụ
+
Đền bù nếu có
```

---

### Test 8 - Thanh toán nhiều lần

Ví dụ:

```text
Hóa đơn = 2.000.000

Thanh toán lần 1 = 500.000
→ Chưa thanh toán đủ

Thanh toán lần 2 = 1.500.000
→ Đã thanh toán đủ
```

Kiểm tra hệ thống không cho thanh toán vượt quá tổng tiền hóa đơn.

---

### Test 9 - Trả phòng

Thử trả phòng khi:

```text
Hóa đơn chưa thanh toán đủ
```

Kết quả:

```text
Không cho trả phòng.
```

Sau đó thanh toán đủ và thử lại.

Kết quả:

```text
Trả phòng thành công.
Phòng được giải phóng.
```

---

# 11. Tiêu chí kiểm tra kết quả

| STT | Nội dung kiểm tra                   | Kết quả mong đợi                       |
| --: | ----------------------------------- | -------------------------------------- |
|   1 | Thêm phòng                          | Thành công                             |
|   2 | Thiết lập sức chứa                  | Thành công                             |
|   3 | Đặt phòng hợp lệ                    | Thành công                             |
|   4 | Đặt phòng vượt sức chứa             | Từ chối                                |
|   5 | Đặt phòng trùng lịch                | Từ chối                                |
|   6 | Lắp đặt tiện nghi                   | Thành công                             |
|   7 | Kiểm tra tiện nghi trùng phòng/ngày | Không cho dữ liệu không hợp lệ         |
|   8 | Nhận phòng                          | Chuyển sang `Đang ở`                   |
|   9 | Ghi dịch vụ                         | Thành công                             |
|  10 | Ghi cùng dịch vụ trong ngày         | Cộng dồn số lượng                      |
|  11 | Lập đền bù                          | Thành công                             |
|  12 | Lập hóa đơn                         | Thành công                             |
|  13 | Thanh toán một phần                 | Hóa đơn chưa thanh toán                |
|  14 | Thanh toán đủ                       | Chuyển `Đã thanh toán`                 |
|  15 | Trả phòng khi chưa thanh toán       | Từ chối                                |
|  16 | Trả phòng sau khi thanh toán        | Thành công                             |
|  17 | Thống kê                            | Hiển thị dữ liệu theo khoảng thời gian |

---

# 12. Kết luận

Bài Lab đã xây dựng hệ thống quản lý khách sạn bằng **C# WinForms + SQL Server + ADO.NET**, bao gồm các chức năng từ quản lý danh mục, phòng, tiện nghi, khách hàng, đặt phòng, nhận phòng, dịch vụ, đền bù, hóa đơn, thanh toán đến trả phòng và thống kê.

Hệ thống được tổ chức theo module và phân tầng:

```text
Forms
  ↓
Services
  ↓
Data
  ↓
SQL Server
```

Các quy tắc nghiệp vụ quan trọng được kiểm tra gồm:

* Sức chứa phòng.
* Trùng lịch đặt phòng.
* Kiểm soát tiện nghi.
* Cộng dồn dịch vụ cùng ngày.
* Tính hóa đơn.
* Thanh toán nhiều lần/nhiều phương thức.
* Không cho trả phòng khi chưa thanh toán đủ.

README này cung cấp các bước cần thiết để giảng viên có thể **mở project, tạo CSDL, cấu hình kết nối, chạy chương trình và kiểm tra các chức năng chính của hệ thống**.
