# UDSV — Ứng dụng quản lý sinh viên

Đồ án cuối kỳ môn **Công cụ và Môi trường Phát triển Phần mềm**
(GV: ThS. Đặng Thế Hân).

WinForms · .NET 8 · SQL Server Express — kiến trúc 3 tầng **UI → BLL → DAL → SQL Server**, dữ liệu
đi qua **DTO**.

---

## 1. Cấu trúc solution

| Project | Vai trò | Không được chứa |
|---|---|---|
| `UDSV` | Giao diện WinForms (các `Frm*`) | SQL, quy tắc nghiệp vụ |
| `UDSV.BLL` | Quy tắc nghiệp vụ, kiểm tra dữ liệu | SQL, điều khiển giao diện |
| `UDSV.DAL` | Truy cập SQL Server, cấu hình logging | Giao diện, thông báo cho người dùng |
| `UDSV.DTO` | Lớp dữ liệu dùng chung (`SinhVienDTO`, `MonHocDTO`, …) | Mọi xử lý |

Chiều tham chiếu: `UDSV → BLL → DAL → DTO`. UI **không** tham chiếu trực tiếp DAL.

Mọi câu lệnh SQL đều dùng `SqlParameter` (không ghép chuỗi), theo yêu cầu Chương 9 và worksheet Buổi 3.

## 2. Chức năng

| Màn hình | Chức năng |
|---|---|
| `FrmDangNhap` / `FrmDangKy` | Đăng nhập, đăng ký tài khoản |
| `FrmTrangChu` | Trang chủ, điều hướng, đăng xuất |
| `FrmSinhVien` | CRUD + tìm kiếm sinh viên |
| `FrmMonHoc` | CRUD môn học (chặn xoá môn còn lịch học) |
| `FrmLichHoc` | CRUD lịch học (chọn môn + sinh viên từ CSDL) |

Tài khoản mẫu (do script CSDL tạo): **admin / 123456** và **phuc / 123456**.

> ⚠️ Mật khẩu đang lưu dạng thô theo đúng script CSDL hiện tại. Khi triển khai thật phải băm
> (hash) trước khi lưu — ghi ở đây để không ai tưởng đây là cách làm đúng.

## 3. Cài đặt trên máy mới (làm đúng thứ tự)

### 3.1. Bật TCP/IP cho SQL Server Express — **bắt buộc**

SQL Server Express cài mới **tắt TCP/IP**. SSMS vẫn kết nối được (nó dùng Shared Memory), nhưng
`Microsoft.Data.SqlClient` trong ứng dụng **không dùng Shared Memory**, nên sẽ báo:

```
A network-related or instance-specific error ... (provider: Shared Memory Provider, error: 40)
```

Sửa:

1. `Win + R` → gõ `SQLServerManager16.msc` (SQL Server 2022; bản 2019 dùng `…15.msc`).
   Chương trình này không có trong Start Menu.
2. `SQL Server Network Configuration` → `Protocols for <TÊN_INSTANCE>` → chuột phải **TCP/IP** →
   **Enabled**.
3. Khởi động lại dịch vụ: `services.msc` → `SQL Server (<TÊN_INSTANCE>)` → **Restart**.

Kiểm tra nhanh:

```sql
SELECT ip_address, port FROM sys.dm_tcp_listener_states;   -- phải có ít nhất 1 dòng
```

### 3.2. Tạo database

Mở file `Đề tài cuối kì CC.sql` trong SSMS rồi Execute. Script tạo database **DETAI** cùng 5 bảng
(`TaiKhoan`, `SinhVien`, `MonHoc`, `LichHoc`) và dữ liệu mẫu.

> ⚠️ Script **xoá DETAI nếu đã tồn tại**. Đừng chạy lại khi đã có dữ liệu thật.

### 3.3. Chuỗi kết nối

Mặc định trong `UDSV.DAL/DatabaseHelper.cs`:

```csharp
Server=localhost\SQLEXPRESS05;Database=DETAI;Integrated Security=True;TrustServerCertificate=True;
```

Máy bạn có instance tên khác thì **không cần sửa code**: đặt biến môi trường trước khi chạy.

```powershell
setx UDSV_CONNECTION "Server=localhost\SQLEXPRESS;Database=DETAI;Integrated Security=True;TrustServerCertificate=True"
```

(Đóng và mở lại Visual Studio sau khi `setx` để biến môi trường có hiệu lực.)

### 3.4. Chạy

Mở `UDSV.sln` bằng Visual Studio 2022 (đã cài workload **.NET desktop development**), chọn project
khởi động là `UDSV`, nhấn **F5**.

## 4. Ghi log

Dùng `Microsoft.Extensions.Logging` với 5 mức: `Debug`, `Information`, `Warning`, `Error`, `Critical`.

- `Debug` — mỗi câu lệnh SQL và thời gian chạy (DAL)
- `Information` — thêm/sửa/xoá thành công, đăng nhập thành công (BLL)
- `Warning` — dữ liệu bị từ chối bởi quy tắc nghiệp vụ
- `Error` — câu lệnh SQL sai
- `Critical` — không kết nối được SQL Server

Nơi ghi log (chọn vị trí ghi được đầu tiên): `%LOCALAPPDATA%\UDSV\Logs` → thư mục chứa file `.exe`
→ `%TEMP%\UDSV\Logs`. Mỗi ngày một file `UDSV-yyyyMMdd.log`.

Đổi mức log: biến môi trường `UDSV_LOG_LEVEL` (`Debug`, `Information`, `Warning`, `Error`, `Critical`).

## 5. Kiểm thử

```powershell
dotnet test
```

Bộ test nằm trong project `UDSV.Tests` (MSTest).

## 6. Xuất bản (Publish EXE)

```powershell
dotnet publish UDSV\UDSV.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

Kết quả nằm trong `UDSV\bin\Release\net8.0-windows\win-x64\publish\UDSV.exe` — chạy độc lập, máy đích
không cần cài .NET.
