# Hướng dẫn nộp nguyên folder lên GitHub (không dùng Terminal)

1. Giải nén `QuanLyHoSoBenhAn_LAB6.zip` vào một thư mục trên máy.
2. Đăng nhập https://github.com → bấm dấu **+** (góc trên phải) → **New repository**.
3. Đặt tên ví dụ `LAB6-QuanLyHoSoBenhAn`, chọn **Public** (nếu giảng viên yêu cầu xem trực tiếp) → **Create repository**.
4. Trên trang repository mới, chọn **uploading an existing file** hoặc **Add file → Upload files**.
5. Mở thư mục đã giải nén trong File Explorer. Chọn toàn bộ nội dung **bên trong** thư mục (`Database`, `Docs`, `Tests`, `UML`, `src`, `README.md`, `.gitignore`) rồi kéo-thả lên trang upload. GitHub sẽ giữ thư mục con. Kiểm tra đủ các folder trước khi commit.
6. Nhập commit message `Add LAB6 hospital records WinForms SQL Server project` → **Commit changes**.
7. Mở lại repository, kiểm tra có `README.md`, `src/QuanLyHoSoBenhAn.sln`, các file SQL trong `Database/`, `.puml` trong `UML/` và `Tests/01_TestCases.sql`.
8. Copy đường dẫn repository trên thanh địa chỉ gửi giảng viên. Nên chụp màn hình ứng dụng đã chạy, 12 test SQL PASS (sau khi chạy thật) và kèm ảnh trong bài nộp nếu giảng viên yêu cầu.

**Bảo mật:** Không đưa `bin`, `obj`, `.vs`, file `.mdf`/`.bak`, ảnh/hồ sơ bệnh nhân thật, mật khẩu tài khoản SQL. Nếu cấu hình dùng SQL Login, trước khi upload hãy thay User/Password thật trong `App.config` bằng giá trị mẫu hoặc dùng `Integrated Security=True`.
