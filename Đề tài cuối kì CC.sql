USE master;
GO

-- Xóa DB cũ nếu đã tồn tại để làm mới hoàn toàn
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DETAI')
BEGIN
    ALTER DATABASE DETAI SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DETAI;
END
GO

-- 1. Tạo Database tên là DETAI
CREATE DATABASE DETAI;
GO
USE DETAI;
GO

-- 2. Bảng Tài Khoản (Dùng cho Form 1: Đăng nhập & Đăng ký)
CREATE TABLE TaiKhoan (
    Username VARCHAR(50) PRIMARY KEY,
    Password VARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL
);
GO

-- 3. Bảng Sinh Viên (Dùng cho Form 3: Quản lý Sinh viên)
CREATE TABLE SinhVien (
    MaSV VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(10) NOT NULL,
    Lop VARCHAR(50) NOT NULL,
    SDT VARCHAR(15)
);
GO

-- 4. Bảng Môn Học (Dùng cho Form 4: Quản lý Môn học)
CREATE TABLE MonHoc (
    MaMH VARCHAR(20) PRIMARY KEY,
    TenMH NVARCHAR(100) NOT NULL,
    SoTinChi INT NOT NULL,
    GiangVien NVARCHAR(100)
);
GO

-- 5. Bảng Lịch Học (Dùng cho Form 5: Liên kết Môn học + Sinh viên + Ngày học)
CREATE TABLE LichHoc (
    MaLich INT IDENTITY(1,1) PRIMARY KEY,
    MaMH VARCHAR(20) FOREIGN KEY REFERENCES MonHoc(MaMH),
    MaSV VARCHAR(20) FOREIGN KEY REFERENCES SinhVien(MaSV),
    NgayHoc DATE NOT NULL,
    CaHoc NVARCHAR(50) NOT NULL,
    PhongHoc VARCHAR(50)
);
GO

-- ================= THÊM DỮ LIỆU MẪU =================
INSERT INTO TaiKhoan VALUES ('admin', '123456', N'Quản trị viên');
INSERT INTO TaiKhoan VALUES ('phuc', '123456', N'Lê Hồng Phúc');

INSERT INTO SinhVien VALUES ('SV001', N'Nguyễn Văn An', '2005-05-12', N'Nam', 'CNTT01', '0901234567');
INSERT INTO SinhVien VALUES ('SV002', N'Trần Thị Bình', '2005-10-20', N'Nữ', 'CNTT01', '0912345678');
INSERT INTO SinhVien VALUES ('SV003', N'Lê Hoàng Nam', '2005-02-15', N'Nam', 'KTPM02', '0987654321');

INSERT INTO MonHoc VALUES ('MH01', N'Lập trình Windows Forms C#', 3, N'ThS. Huỳnh Văn Huy');
INSERT INTO MonHoc VALUES ('MH02', N'Cơ sở dữ liệu SQL Server', 3, N'TS. Trần Thị B');

INSERT INTO LichHoc (MaMH, MaSV, NgayHoc, CaHoc, PhongHoc) VALUES ('MH01', 'SV001', '2026-10-28', N'Ca 1 (07:00 - 11:00)', 'P.301');
INSERT INTO LichHoc (MaMH, MaSV, NgayHoc, CaHoc, PhongHoc) VALUES ('MH01', 'SV002', '2026-10-28', N'Ca 1 (07:00 - 11:00)', 'P.301');
INSERT INTO LichHoc (MaMH, MaSV, NgayHoc, CaHoc, PhongHoc) VALUES ('MH02', 'SV003', '2026-10-29', N'Ca 2 (13:00 - 17:00)', 'P.205');
GO