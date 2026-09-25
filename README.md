

## 1. Giới thiệu

**QuanLyThuVien** là ứng dụng quản lý thư viện được xây dựng nhằm tin học hóa các nghiệp vụ quản lý nhân viên, đầu sách, thể loại, nhà xuất bản, độc giả, thẻ thư viện và hoạt động mượn – trả sách.

Hệ thống hỗ trợ thủ thư và nhân viên quản lý sách thực hiện các nghiệp vụ chính:

- Quản lý thông tin nhân viên.
- Quản lý đầu sách.
- Quản lý thể loại.
- Quản lý nhà xuất bản.
- Quản lý độc giả.
- Cấp và gia hạn thẻ thư viện.
- Lập phiếu mượn sách.
- Kiểm tra điều kiện mượn.
- Xử lý trả sách.
- Lập phiếu phạt khi sách trễ hạn, mất hoặc hư hỏng.
- Thống kê sách mượn, mất, hư hỏng và quá hạn theo tháng.

---

## 2. Mục tiêu

Hệ thống được xây dựng để:

1. Tin học hóa quá trình quản lý thư viện.
2. Giảm thao tác quản lý thủ công.
3. Hỗ trợ kiểm tra điều kiện mượn sách.
4. Quản lý số lượng sách hiện có.
5. Quản lý thông tin độc giả và thẻ thư viện.
6. Quản lý quá trình mượn – trả sách.
7. Theo dõi và xử lý các trường hợp vi phạm như quá hạn, mất hoặc hư hỏng sách.
8. Hỗ trợ thống kê phục vụ quản lý thư viện.

---

## 3. Chức năng chính

### 3.1. Quản lý nhân viên

Quản lý các thông tin:

- Mã nhân viên
- Họ
- Tên
- Phái
- Ngày sinh
- Chức vụ
- Số điện thoại

Các thao tác:

- Thêm
- Sửa
- Xóa
- Tìm kiếm

### 3.2. Quản lý đầu sách

Quản lý:

- Mã đầu sách
- Tên sách
- Năm xuất bản
- Thể loại
- Nhà xuất bản
- Số lượng hiện có

### 3.3. Quản lý thể loại

Quản lý mã và tên thể loại.

Một thể loại có thể có nhiều đầu sách, trong khi một đầu sách chỉ thuộc một thể loại.

### 3.4. Quản lý nhà xuất bản

Quản lý:

- Mã xuất bản
- Địa chỉ
- Số điện thoại

### 3.5. Quản lý độc giả

Quản lý:

- Mã độc giả
- Họ tên
- Ngày sinh
- Phái
- Số điện thoại
- Địa chỉ
- Email
- Ảnh 3x4

### 3.6. Quản lý thẻ thư viện

Hệ thống hỗ trợ:

- Cấp thẻ
- Gia hạn thẻ
- Cấp mới thẻ
- Tra cứu thẻ
- Theo dõi ngày cấp và hạn sử dụng
- Theo dõi lệ phí và trạng thái thẻ

Độc giả muốn mượn sách phải có thẻ thư viện còn giá trị.

### 3.7. Mượn sách

Khi lập phiếu mượn, hệ thống kiểm tra:

- Thẻ thư viện còn hạn.
- Độc giả đã đóng lệ phí.
- Độc giả không có sách quá hạn chưa trả.
- Số lượng sách còn trong thư viện.
- Một phiếu không được chứa hai sách cùng một đầu sách.
- Một độc giả được mượn tối đa 3 cuốn sách khác nhau về nhà.

Nếu hợp lệ, hệ thống lập phiếu mượn và cập nhật số lượng sách hiện có.

### 3.8. Trả sách

Khi trả sách, thủ thư kiểm tra:

- Ngày trả.
- Tình trạng sách.

Các trường hợp có thể xảy ra:

- Trả đúng hạn và sách bình thường.
- Trả quá hạn.
- Mất sách.
- Rách hoặc hư hỏng sách.

Nếu thuộc trường hợp phải phạt, hệ thống lập phiếu phạt ghi nhận ngày phạt, lý do, phí phạt và nhân viên lập phiếu.

### 3.9. Thống kê

Hỗ trợ thống kê hàng tháng:

- Sách mượn.
- Sách mất.
- Sách hư hỏng.
- Sách quá hạn.

---

## 4. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Ngôn ngữ | C# |
| Giao diện | Windows Forms |
| IDE | Visual Studio |
| Kiến trúc mã nguồn | Phân chia Models – Data – Services – Forms |
| Cấu hình | App.config |

> Các công nghệ cơ sở dữ liệu cụ thể phụ thuộc vào cấu hình và mã nguồn thực tế của project.

---

## 5. Cấu trúc thư mục

```text
QuanLyThuVien/
│
├── Data/
│   └── Db.cs
│
├── Services/
│   ├── DanhMucService.cs
│   ├── SachService.cs
│   ├── DocGiaService.cs
│   ├── MuonTraService.cs
│   └── ThongKeService.cs
│
├── Forms/
│   ├── FrmMain.cs
│   ├── FrmDanhMuc.cs
│   ├── FrmSach.cs
│   ├── FrmDocGia.cs
│   ├── FrmMuonTra.cs
│   └── FrmThongKe.cs
│
├── Models.cs
├── Program.cs
├── App.config
└── README.md
