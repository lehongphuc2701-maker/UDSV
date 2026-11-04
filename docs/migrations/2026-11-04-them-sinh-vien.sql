-- ============================================================================
--  Nâng cấp CSDL DETAI cho phần SINH VIÊN (2026-11-04)
--  Dùng khi bạn ĐÃ CÓ dữ liệu và không muốn chạy lại toàn bộ script (file đó xoá DB).
--  Cách chạy:
--    sqlcmd -S localhost\SQLEXPRESS05 -E -d DETAI -i docs\migrations\2026-11-04-them-sinh-vien.sql
--  (đổi tên instance cho đúng máy của bạn)
-- ============================================================================

-- 1) Phòng học của buổi hằng tuần (nếu chưa có)
IF COL_LENGTH('ChuongTrinhChiTiet', 'PhongHoc') IS NULL
    ALTER TABLE ChuongTrinhChiTiet ADD PhongHoc VARCHAR(50) NULL;
GO

-- 2) Tài khoản gắn với sinh viên (nếu chưa có)
IF COL_LENGTH('TaiKhoan', 'MaSV') IS NULL
    ALTER TABLE TaiKhoan ADD MaSV VARCHAR(20) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_TaiKhoan_SinhVien')
    ALTER TABLE TaiKhoan ADD CONSTRAINT FK_TaiKhoan_SinhVien
        FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV);
GO

-- 3) Tạo tài khoản cho mọi sinh viên đang có: tên đăng nhập = mã SV, mật khẩu 123456
INSERT INTO TaiKhoan (Username, Password, HoTen, VaiTro, MaSV)
SELECT s.MaSV, '123456', s.HoTen, 'SinhVien', s.MaSV
FROM SinhVien s
WHERE NOT EXISTS (SELECT 1 FROM TaiKhoan t WHERE t.Username = s.MaSV);
GO

-- 4) Kiểm tra
SELECT Username, VaiTro, MaSV FROM TaiKhoan ORDER BY VaiTro, Username;
GO
