-- ============================================================================
--  Thêm phần XOÁ MỀM cho CSDL DETAI (2026-11-04)
--
--  Nội dung:
--    1) SinhVien.TrangThai  NVARCHAR(30) — trạng thái học tập, đồng thời là cờ xoá mềm
--                             của sinh viên ('Đã xóa'). Có DEFAULT + CHECK.
--    2) MonHoc.DaXoa        BIT          — cờ xoá mềm của môn học. Có DEFAULT 0.
--
--  Dùng khi bạn ĐÃ CÓ dữ liệu và không muốn chạy lại toàn bộ "Đề tài cuối kì CC.sql"
--  (script đó XOÁ database DETAI rồi tạo lại từ đầu).
--
--  Cách chạy (đổi tên instance cho đúng máy của bạn):
--    sqlcmd -S localhost\SQLEXPRESS05 -E -d DETAI -f 65001 -i docs\migrations\2026-11-04-them-xoa-mem.sql
--  (-f 65001 = đọc file theo UTF-8, để chữ có dấu trong N'...' không bị lệch.)
--
--  Chạy lại nhiều lần được: mỗi bước đều kiểm tra "đã có chưa" trước khi thêm.
--  Lưu ý: bước thêm CHECK sẽ báo lỗi nếu bảng SinhVien đã có dòng mang trạng thái
--  nằm ngoài 4 giá trị dưới đây — sửa dữ liệu đó rồi chạy lại, không mất dòng nào.
-- ============================================================================

USE DETAI;
GO

-- 1) Cột trạng thái của sinh viên -------------------------------------------------
IF COL_LENGTH('SinhVien', 'TrangThai') IS NULL
BEGIN
    ALTER TABLE SinhVien
        ADD TrangThai NVARCHAR(30) NOT NULL
            CONSTRAINT DF_SinhVien_TrangThai DEFAULT N'Chưa tốt nghiệp';
    PRINT N'Đã thêm cột SinhVien.TrangThai.';
END
ELSE
    PRINT N'Cột SinhVien.TrangThai đã có — bỏ qua.';
GO

-- 1b) Bảo đảm có DEFAULT, phòng trường hợp cột được thêm tay mà thiếu default
IF COL_LENGTH('SinhVien', 'TrangThai') IS NOT NULL
   AND NOT EXISTS (SELECT 1
                   FROM sys.default_constraints dc
                   JOIN sys.columns c ON c.object_id = dc.parent_object_id
                                     AND c.column_id = dc.parent_column_id
                   WHERE dc.parent_object_id = OBJECT_ID(N'SinhVien')
                     AND c.name = N'TrangThai')
BEGIN
    ALTER TABLE SinhVien
        ADD CONSTRAINT DF_SinhVien_TrangThai DEFAULT N'Chưa tốt nghiệp' FOR TrangThai;
    PRINT N'Đã thêm DEFAULT DF_SinhVien_TrangThai.';
END
GO

-- 1c) Bảo đảm có CHECK: chỉ nhận 4 trạng thái của ứng dụng
IF COL_LENGTH('SinhVien', 'TrangThai') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = N'CK_SinhVien_TrangThai')
BEGIN
    ALTER TABLE SinhVien
        ADD CONSTRAINT CK_SinhVien_TrangThai
            CHECK (TrangThai IN (N'Chưa tốt nghiệp', N'Đã thôi học', N'Đã tốt nghiệp', N'Đã xóa'));
    PRINT N'Đã thêm ràng buộc CK_SinhVien_TrangThai.';
END
GO

-- 2) Cờ xoá mềm của môn học ------------------------------------------------------
IF COL_LENGTH('MonHoc', 'DaXoa') IS NULL
BEGIN
    ALTER TABLE MonHoc
        ADD DaXoa BIT NOT NULL
            CONSTRAINT DF_MonHoc_DaXoa DEFAULT 0;
    PRINT N'Đã thêm cột MonHoc.DaXoa.';
END
ELSE
    PRINT N'Cột MonHoc.DaXoa đã có — bỏ qua.';
GO

-- 3) Kiểm tra kết quả ------------------------------------------------------------
SELECT t.name AS Bang, c.name AS Cot, ty.name AS Kieu,
       CASE WHEN c.is_nullable = 1 THEN N'cho phép NULL' ELSE N'NOT NULL' END AS ChoNull,
       dc.name AS TenDefault, dc.definition AS GiaTriMacDinh
FROM sys.columns c
JOIN sys.tables t ON t.object_id = c.object_id
JOIN sys.types ty ON ty.user_type_id = c.user_type_id
LEFT JOIN sys.default_constraints dc ON dc.parent_object_id = c.object_id
                                   AND dc.parent_column_id = c.column_id
WHERE (t.name = N'SinhVien' AND c.name = N'TrangThai')
   OR (t.name = N'MonHoc'  AND c.name = N'DaXoa');
GO
