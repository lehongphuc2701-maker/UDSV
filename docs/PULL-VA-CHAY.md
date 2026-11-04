# Cho thành viên khác trong nhóm (pull về máy mới)

Sau khi `git pull`, **phải chạy bước CSDL**, nếu không ứng dụng sẽ báo lỗi kết nối/không đăng nhập được.

## 1. Cập nhật CSDL (giữ nguyên dữ liệu đang có)

```powershell
sqlcmd -S localhost\SQLEXPRESS05 -E -d DETAI -i docs\migrations\2026-11-04-them-sinh-vien.sql
```

Đổi `localhost\SQLEXPRESS05` thành instance trên máy bạn (ví dụ `.\SQL2022`).

Lệnh này thêm:
- `ChuongTrinhChiTiet.PhongHoc` — phòng học của buổi hằng tuần
- `TaiKhoan.MaSV` + khoá ngoại sang `SinhVien`
- tài khoản cho mọi sinh viên: **tên đăng nhập = mã SV**, mật khẩu `123456`

## 2. Chuỗi kết nối

Mặc định trong code là `.\SQL2022`. Nếu máy bạn dùng instance khác, đặt biến môi trường
(không cần sửa code):

```powershell
setx UDSV_CONNECTION "Server=localhost\SQLEXPRESS05;Database=DETAI;Integrated Security=True;TrustServerCertificate=True;"
```

Rồi **mở lại Visual Studio** để nó nhận biến mới.

## 3. Đăng nhập thử

| Tài khoản | Mật khẩu | Vai trò |
|---|---|---|
| `admin` | `123456` | quản trị |
| `SV001` … `SV102` | `123456` | sinh viên |

## 4. Nếu muốn dựng lại từ đầu

Chạy `Đề tài cuối kì CC.sql` — file này **xoá và tạo lại** CSDL `DETAI`.
