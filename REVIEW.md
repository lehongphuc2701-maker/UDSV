# Gói xem trước — UDSV (bản 28/10/2026)

Đây là **bản chụp mã nguồn**, không phải repository. Không có lịch sử Git trong gói này; mục đích
là để mọi người đọc và chạy thử trước khi bàn chuyện commit.

- Xuất từ nhánh làm việc ngày 28/10/2026, tại commit cuối cùng của buổi làm việc.
- Gồm: toàn bộ mã nguồn, script CSDL, README. **Không** gồm `bin/`, `obj/`, `.git/`.
- Sau khi xem xong, mọi thay đổi thật vẫn phải làm trên repo GitHub của nhóm.

---

## 1. Chạy thử (làm đúng thứ tự)

1. Cài **.NET 8 SDK** và **Visual Studio 2022** (workload *.NET desktop development*).
2. **Bật TCP/IP cho SQL Server Express** — bắt buộc, nếu không ứng dụng báo lỗi *"error: 40"*:
   - `Win + R` → `SQLServerManager16.msc`
   - `SQL Server Network Configuration` → `Protocols for <TÊN_INSTANCE>` → chuột phải **TCP/IP** → **Enabled**
   - `services.msc` → `SQL Server (<TÊN_INSTANCE>)` → **Restart**
3. Mở SSMS, chạy file **`Đề tài cuối kì CC.sql`** (tạo database `DETAI` + dữ liệu mẫu).
   ⚠️ Script **xoá `DETAI` nếu đã tồn tại**.
4. Mở `UDSV\UDSV.sln`, chọn project khởi động là **UDSV**, nhấn **F5**.
5. Đăng nhập: **`admin` / `123456`** hoặc **`phuc` / `123456`**.

**Nếu instance SQL Server trên máy bạn không tên `SQLEXPRESS05`:** không cần sửa code, đặt biến môi
trường rồi mở lại Visual Studio:

```powershell
setx UDSV_CONNECTION "Server=localhost\TÊN_INSTANCE_CỦA_BẠN;Database=DETAI;Integrated Security=True;TrustServerCertificate=True"
```

## 2. Kiến trúc — thứ đáng xem nhất

```
UDSV (UI)  →  UDSV.BLL (quy tắc)  →  UDSV.DAL (SQL)  →  SQL Server
                    ↘        UDSV.DTO (dữ liệu dùng chung)        ↙
```

| Project | Nếu thấy SQL ở đây là sai | Nếu thấy MessageBox ở đây là sai |
|---|---|---|
| `UDSV` | ✗ | ✓ (được phép) |
| `UDSV.BLL` | ✗ | ✗ |
| `UDSV.DAL` | ✓ (đúng chỗ) | ✗ |
| `UDSV.DTO` | ✗ | ✗ |

## 3. Bốn điểm kỹ thuật để kiểm tra

1. **Không còn câu SQL nào ghép chuỗi.** Mọi lệnh trong `UDSV.DAL` dùng `SqlParameter`
   (yêu cầu Chương 9 + worksheet Buổi 3).
2. **Quy tắc nghiệp vụ nằm ở BLL**, không ở form và không ở DB: mã SV bắt đầu bằng `SV`, họ tên/lớp
   không rỗng, điểm TB trong `0..4`, chặn trùng mã, chặn xoá môn còn lịch học.
3. **Logging 5 mức** (`Debug`, `Information`, `Warning`, `Error`, `Critical`) — xem `UDSV.DAL\AppLog.cs`
   và cách gọi trong `DatabaseHelper` + các BLL. File log nằm cạnh `UDSV.exe` trong thư mục `Logs\`.
4. **10 unit test** trong `UDSV.Tests\SinhVienTests.cs`, đúng tên theo slide Chương 7. Chạy bằng
   Test Explorer (`Ctrl+E, T`) hoặc `dotnet test`. Test **không cần SQL Server** vì dùng DAL giả.

## 4. Nếu bị hỏi trong buổi demo, nên trả lời được

- Vì sao tách 4 project thay vì viết hết trong `Form1.cs`?
- `SqlParameter` khác gì ghép chuỗi — và chống được cái gì?
- Vì sao kiểm tra dữ liệu đặt ở BLL mà không đặt ở form?
- `DataTable` bind lên `DataGridView` hoạt động thế nào?
- Log `Critical` khác `Error` ở chỗ nào, ví dụ cụ thể trong dự án?

## 5. Góp ý thì gửi lại gì

Ghi ngắn gọn theo mẫu, đừng sửa code trực tiếp trong gói này:

| Chỗ | Vấn đề | Đề xuất |
|---|---|---|
| ví dụ: `FrmLichHoc` | ô chọn sinh viên hiện họ tên nhưng không hiện mã | thêm cột mã vào combo |

Cảm ơn đã xem. Mọi thay đổi thật sẽ làm trên repo, không làm trên gói này.
