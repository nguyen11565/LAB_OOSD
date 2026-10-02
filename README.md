# LAB4 - HỆ THỐNG CỬA HÀNG ONLINE e-SHOPPING

## 1. Giới thiệu

e-SHOPPING là hệ thống cửa hàng trực tuyến cho phép khách hàng đăng ký, đăng nhập, xem sản phẩm, quản lý giỏ hàng, đặt hàng, lựa chọn hình thức giao hàng và thanh toán trực tuyến.

Hệ thống kết nối với 3 hệ thống/dịch vụ bên ngoài:

- Hệ thống quản lý sản phẩm.
- Dịch vụ thanh toán trực tuyến.
- Dịch vụ Email.

## 2. Công nghệ

- C#: Windows Forms
- .NET Framework 4.7.2
- Microsoft SQL Server
- ADO.NET
- Visual Studio / Visual Studio Code
- UML / Draw.io
- Git / GitHub

## 3. Chức năng chính

### Quản lý tài khoản
- Đăng ký tài khoản.
- Đăng nhập.
- Quản lý thông tin khách hàng.
- Đổi mật khẩu.

### Sản phẩm
- Xem nhóm sản phẩm.
- Xem danh sách sản phẩm.
- Xem chi tiết sản phẩm.
- Xem giá và tình trạng sản phẩm.
- Thêm sản phẩm vào giỏ hàng.

### Giỏ hàng
- Xem giỏ hàng.
- Thêm sản phẩm.
- Cập nhật số lượng.
- Xóa sản phẩm.
- Tính tổng tiền.

### Đặt hàng
- Chọn loại giao hàng.
- Nhập thông tin người nhận.
- Tính phí giao hàng.
- Tính tổng tiền đơn hàng.

Hệ thống hỗ trợ:

- Giao hàng thường.
- Giao hàng nhanh.
- Giao hàng nhanh trong ngày.

Quy tắc miễn phí:

- Đơn hàng từ **1.000.000 VNĐ**: giao hàng nhanh miễn phí.
- Đơn hàng từ **5.000.000 VNĐ**: giao hàng nhanh trong ngày miễn phí.

### Thanh toán
Hỗ trợ:

- VISA.
- MasterCard.
- Discover.
- American Express.

Hệ thống kết nối dịch vụ thanh toán trực tuyến để kiểm tra thông tin thẻ và khả năng thanh toán.

### Đơn hàng
Sau khi thanh toán thành công, hệ thống ghi nhận:

- Sản phẩm.
- Số lượng.
- Đơn giá.
- Người mua.
- Người nhận.
- Loại giao hàng.
- Phí giao hàng.
- Tổng tiền.
- Thời gian đặt hàng.

### Email
Nếu khách hàng cung cấp email, hệ thống gửi email xác nhận đơn hàng và không đưa thông tin thẻ vào email.

## 4. Phân rã chức năng

```text
HỆ THỐNG e-SHOPPING
│
├── 1. Quản lý tài khoản khách hàng
│   ├── Đăng ký
│   └── Đăng nhập
│
├── 2. Tra cứu và chọn sản phẩm
│   ├── Xem nhóm sản phẩm
│   ├── Xem danh sách sản phẩm
│   ├── Xem chi tiết sản phẩm
│   └── Thêm vào giỏ hàng
│
├── 3. Quản lý giỏ hàng
│   ├── Xem giỏ hàng
│   ├── Cập nhật số lượng
│   └── Xóa sản phẩm
│
├── 4. Đặt hàng và tính tiền
│   ├── Chọn loại giao hàng
│   ├── Tính phí giao hàng
│   ├── Nhập thông tin người nhận
│   └── Tính tổng tiền
│
├── 5. Thanh toán
│   ├── Nhập thông tin thẻ
│   ├── Kiểm tra thẻ
│   └── Xác nhận thanh toán
│
└── 6. Ghi nhận và xác nhận đơn hàng
    ├── Ghi nhận đơn hàng
    └── Gửi email xác nhận
