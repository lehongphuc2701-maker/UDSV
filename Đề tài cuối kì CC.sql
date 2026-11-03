-- =====================================================================
--  ĐỀ TÀI CUỐI KỲ — ỨNG DỤNG QUẢN LÝ SINH VIÊN (UDSV)
--  Mô hình: quyền theo khóa (catalog-year) — mỗi khóa có một chương trình cố định
--  Môn: Công cụ và Môi trường Phát triển Phần mềm
--
--  QUY ƯỚC:
--  * Điểm hệ 4: 0.00 .. 4.00. Đạt khi Diem >= 2.0.
--  * Mỗi khóa (Khoa) có đúng một chương trình (ChuongTrinhChiTiet).
--    Chương trình bị KHOÁ khi khóa đã có sinh viên — thực thi ở tầng BLL.
--  * Lịch học gắn với MÔN (không gắn với khóa, không gắn với sinh viên).
--  * Kết quả: mỗi (sinh viên, môn) tối đa một dòng. Không có học lại.
--
--  ⚠️  Script XOÁ database DETAI nếu đã tồn tại.
--  Dữ liệu mẫu là một lát cắt 3–5 học kỳ; chương trình thật của trường có
--  54 học phần / 120 TC (khảo sát cổng thông tin sinh viên BVU 2026-11-03).
-- =====================================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DETAI')
BEGIN
    ALTER DATABASE DETAI SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DETAI;
END
GO

CREATE DATABASE DETAI;
GO
USE DETAI;
GO

-- ---------------------------------------------------------------
-- 1. Tài khoản đăng nhập
-- ---------------------------------------------------------------
CREATE TABLE TaiKhoan (
    Username VARCHAR(50)  NOT NULL PRIMARY KEY,
    Password VARCHAR(100) NOT NULL,          -- đồ án: lưu thô; thực tế phải băm
    HoTen    NVARCHAR(100) NOT NULL,
    VaiTro   VARCHAR(20)  NOT NULL DEFAULT 'Admin',
    CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN ('Admin', 'SinhVien'))
);
GO

-- ---------------------------------------------------------------
-- 2. Khóa (niên khóa) — mỗi khóa là một "quyền" theo chương trình của nó
-- ---------------------------------------------------------------
CREATE TABLE Khoa (
    MaKhoa        VARCHAR(10)   NOT NULL PRIMARY KEY,   -- K24, K25
    TenKhoa       NVARCHAR(100) NOT NULL,
    NamBatDau     INT           NOT NULL,
    NamKetThuc    INT           NOT NULL,
    TongTCBatBuoc INT           NOT NULL,
    TongTCTuChon  INT           NOT NULL,
    CONSTRAINT CK_Khoa_Nam CHECK (NamKetThuc > NamBatDau),
    CONSTRAINT CK_Khoa_TC  CHECK (TongTCBatBuoc > 0 AND TongTCTuChon >= 0)
);
GO

-- ---------------------------------------------------------------
-- 3. Sinh viên — thuộc đúng một khóa
-- ---------------------------------------------------------------
CREATE TABLE SinhVien (
    MaSV     VARCHAR(20)   NOT NULL PRIMARY KEY,
    HoTen    NVARCHAR(100) NOT NULL,
    NgaySinh DATE          NOT NULL,
    GioiTinh NVARCHAR(10)  NOT NULL,
    MaKhoa   VARCHAR(10)   NOT NULL,
    SDT      VARCHAR(15),
    CONSTRAINT FK_SinhVien_Khoa FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
);
GO

-- ---------------------------------------------------------------
-- 4. Môn học (danh mục) — không chứa khóa, không chứa điểm
-- ---------------------------------------------------------------
CREATE TABLE MonHoc (
    MaMH      VARCHAR(20)   NOT NULL PRIMARY KEY,
    TenMH     NVARCHAR(100) NOT NULL,
    SoTinChi  INT           NOT NULL,
    GiangVien NVARCHAR(100),
    CONSTRAINT CK_MonHoc_TC CHECK (SoTinChi BETWEEN 1 AND 10)
);
GO

-- ---------------------------------------------------------------
-- 5. Chương trình của từng khóa — bị khoá khi khóa đã có sinh viên
-- ---------------------------------------------------------------
CREATE TABLE ChuongTrinhChiTiet (
    MaKhoa  VARCHAR(10) NOT NULL,
    MaMH    VARCHAR(20) NOT NULL,
    BatBuoc BIT         NOT NULL DEFAULT 1,      -- 1 = bắt buộc, 0 = tự chọn
    CONSTRAINT PK_ChuongTrinhChiTiet PRIMARY KEY (MaKhoa, MaMH),
    CONSTRAINT FK_CTCT_Khoa FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa),
    CONSTRAINT FK_CTCT_Mon  FOREIGN KEY (MaMH)   REFERENCES MonHoc(MaMH)
);
GO

-- ---------------------------------------------------------------
-- 6. Lịch học — gắn với MÔN
-- ---------------------------------------------------------------
CREATE TABLE LichHoc (
    MaLich   INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
    MaMH     VARCHAR(20)   NOT NULL,
    NgayHoc  DATE          NOT NULL,
    CaHoc    NVARCHAR(50)  NOT NULL,
    PhongHoc VARCHAR(50),
    CONSTRAINT FK_LichHoc_Mon FOREIGN KEY (MaMH) REFERENCES MonHoc(MaMH)
);
GO

-- ---------------------------------------------------------------
-- 7. Kết quả — mỗi (sinh viên, môn) một dòng, điểm là cuối cùng
-- ---------------------------------------------------------------
CREATE TABLE KetQua (
    MaSV    VARCHAR(20)  NOT NULL,
    MaMH    VARCHAR(20)  NOT NULL,
    Diem    DECIMAL(4,2) NULL,                    -- NULL = đang học, chưa có điểm
    LanNhap DATETIME     NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_KetQua PRIMARY KEY (MaSV, MaMH),
    CONSTRAINT FK_KetQua_SV FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    CONSTRAINT FK_KetQua_MH FOREIGN KEY (MaMH) REFERENCES MonHoc(MaMH),
    CONSTRAINT CK_KetQua_Diem CHECK (Diem IS NULL OR (Diem >= 0 AND Diem <= 4))
);
GO

-- =====================================================================
--  DỮ LIỆU MẪU
-- =====================================================================

INSERT INTO TaiKhoan (Username, Password, HoTen, VaiTro) VALUES
    ('admin', '123456', N'Quản trị viên',    'Admin'),
    ('phuc',  '123456', N'Lê Hồng Phúc',     'Admin'),
    ('huan',  '123456', N'Nguyễn Cảnh Huân', 'Admin');
GO

-- Hai khóa: K24 đủ 9 môn bắt buộc (27 TC) + 2 môn tự chọn (6 TC)
--           K25 giống K24 nhưng THAY MH02 bằng MH12 → chứng minh chương trình đổi theo khóa
INSERT INTO Khoa (MaKhoa, TenKhoa, NamBatDau, NamKetThuc, TongTCBatBuoc, TongTCTuChon) VALUES
    ('K24', N'Công nghệ thông tin K24', 2024, 2028, 27, 6),
    ('K25', N'Công nghệ thông tin K25', 2025, 2029, 27, 6);
GO

INSERT INTO MonHoc (MaMH, TenMH, SoTinChi, GiangVien) VALUES
    ('MH01', N'Nhập môn ngành Công nghệ thông tin',        3, N'ThS. Đặng Thế Hân'),
    ('MH02', N'Tin học văn phòng',                          3, N'ThS. Huỳnh Mộng Nghi'),
    ('MH03', N'Lập trình hướng đối tượng',                  3, N'TS. Trần Thị B'),
    ('MH04', N'Cơ sở dữ liệu',                              3, N'TS. Trần Thị B'),
    ('MH05', N'Công cụ và môi trường phát triển phần mềm',  3, N'ThS. Đặng Thế Hân'),
    ('MH06', N'Thiết kế và phát triển website',             3, N'ThS. Lê Kinh Nam'),
    ('MH07', N'Mạng và thiết bị mạng',                      3, N'ThS. Huỳnh Văn Huy'),
    ('MH08', N'Giao diện người – máy',                      3, N'ThS. Nguyễn Thị Phương Nhung'),
    ('MH09', N'Tổ chức dữ liệu và giải thuật',              3, N'TS. Trần Thị B'),
    ('MH10', N'Tiếng Anh CĐR 1',                            3, N'ThS. Nguyễn Thị Phương Nhung'),
    ('MH11', N'Tiếng Anh thương mại A2',                    3, N'ThS. Nguyễn Thị Phương Nhung'),
    ('MH12', N'Kỹ năng số và ứng dụng AI',                  3, N'ThS. Đặng Thế Hân');
GO

-- Chương trình K24: 9 bắt buộc + 2 tự chọn
INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc) VALUES
    ('K24','MH01',1), ('K24','MH02',1), ('K24','MH03',1), ('K24','MH04',1),
    ('K24','MH05',1), ('K24','MH06',1), ('K24','MH07',1), ('K24','MH08',1),
    ('K24','MH09',1),
    ('K24','MH10',0), ('K24','MH11',0);
GO

-- Chương trình K25: thay MH02 bằng MH12
INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc) VALUES
    ('K25','MH01',1), ('K25','MH12',1), ('K25','MH03',1), ('K25','MH04',1),
    ('K25','MH05',1), ('K25','MH06',1), ('K25','MH07',1), ('K25','MH08',1),
    ('K25','MH09',1),
    ('K25','MH10',0), ('K25','MH11',0);
GO

INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, MaKhoa, SDT) VALUES
    ('SV001', N'Nguyễn Văn An',    '2006-05-12', N'Nam', 'K24', '0901234567'),
    ('SV002', N'Trần Thị Bình',    '2006-10-20', N'Nữ',  'K24', '0912345678'),
    ('SV003', N'Lê Hoàng Nam',     '2006-02-15', N'Nam', 'K24', '0987654321'),
    ('SV004', N'Phạm Minh Châu',   '2006-08-30', N'Nữ',  'K24', '0905556667'),
    ('SV005', N'Võ Thị Hà',        '2006-01-09', N'Nữ',  'K24', '0938111222'),
    ('SV006', N'Đỗ Công Huỳnh Hà', '2006-03-21', N'Nam', 'K24', '0977333444'),
    ('SV007', N'Bùi Thị Lan',      '2006-07-14', N'Nữ',  'K24', '0989012345'),
    ('SV008', N'Nguyễn Đức Minh',  '2006-04-07', N'Nam', 'K24', '0990123456'),
    ('SV101', N'Lê Gia Bảo',       '2007-02-11', N'Nam', 'K25', '0901112223'),
    ('SV102', N'Trần Lê Gia Hân',  '2007-06-25', N'Nữ',  'K25', '0902223334');
GO

-- Lịch học cho 4 môn (gắn với môn, không gắn với sinh viên)
INSERT INTO LichHoc (MaMH, NgayHoc, CaHoc, PhongHoc) VALUES
    ('MH01', '2026-09-07', N'Ca 1 (07:00 - 11:00)', 'P.301'),
    ('MH01', '2026-09-14', N'Ca 1 (07:00 - 11:00)', 'P.301'),
    ('MH01', '2026-09-21', N'Ca 1 (07:00 - 11:00)', 'P.301'),
    ('MH02', '2026-09-08', N'Ca 2 (13:00 - 17:00)', 'P.205'),
    ('MH02', '2026-09-15', N'Ca 2 (13:00 - 17:00)', 'P.205'),
    ('MH03', '2026-09-09', N'Ca 3 (17:30 - 21:00)', 'P.102'),
    ('MH04', '2026-09-10', N'Ca 1 (07:00 - 11:00)', 'P.301'),
    ('MH05', '2026-09-11', N'Ca 2 (13:00 - 17:00)', 'P.205');
GO

-- Kết quả: có đạt (>= 2.0), có không đạt (< 2.0), có đang học (NULL)
INSERT INTO KetQua (MaSV, MaMH, Diem) VALUES
    ('SV001','MH01', 3.20), ('SV001','MH02', 2.50), ('SV001','MH03', 1.80), ('SV001','MH04', NULL),
    ('SV002','MH01', 3.80), ('SV002','MH02', 3.10), ('SV002','MH03', 2.90),
    ('SV003','MH01', 1.50), ('SV003','MH02', 2.10),
    ('SV004','MH01', 3.00), ('SV004','MH02', 2.00),
    ('SV005','MH01', 2.70), ('SV005','MH03', 3.30),
    ('SV006','MH01', 2.20),
    ('SV101','MH01', 3.50), ('SV101','MH12', 3.90);
GO

-- =====================================================================
--  KIỂM TRA NHANH (chạy tay khi cần)
-- =====================================================================
-- Số bảng phải là 7:
--   SELECT name FROM sys.tables ORDER BY name;
--
-- Tín chỉ tích luỹ của một sinh viên (chỉ tính môn thuộc chương trình của khóa mình):
--   SELECT SUM(m.SoTinChi) AS TC
--   FROM KetQua k
--   JOIN MonHoc m             ON m.MaMH = k.MaMH
--   JOIN SinhVien sv          ON sv.MaSV = k.MaSV
--   JOIN ChuongTrinhChiTiet c ON c.MaMH = k.MaMH AND c.MaKhoa = sv.MaKhoa
--   WHERE k.MaSV = 'SV001' AND k.Diem >= 2;
--
-- Chương trình của một khóa đã bị khoá chưa:
--   SELECT CASE WHEN EXISTS (SELECT 1 FROM SinhVien WHERE MaKhoa = 'K24')
--               THEN N'Đã khoá' ELSE N'Còn sửa được' END;
