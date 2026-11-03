-- =====================================================================
--  ĐỀ TÀI CUỐI KỲ — ỨNG DỤNG QUẢN LÝ SINH VIÊN (UDSV)
--  Mô hình: quyền theo khóa (catalog year) — mỗi khóa một chương trình riêng
--
--  CHƯƠNG TRÌNH: 39 học phần / 120 tín chỉ / 10 học kỳ, lấy từ "Chương trình khung"
--  của cổng thông tin sinh viên trường (khảo sát 03/11/2026):
--  112 TC bắt buộc + 8 TC tự chọn (Đồ án tốt nghiệp).
--  11 học phần có dấu * (Giáo dục thể chất, Giáo dục quốc phòng, chứng chỉ Mos,
--  Tiếng Anh CĐR…) không tính vào điểm trung bình nên KHÔNG nạp vào đồ án.
--
--  QUY ƯỚC:
--  * Điểm hệ 4: 0.00 .. 4.00. Đạt khi Diem >= 2.0.
--  * Khoa.NgayKhoa IS NULL  = chương trình còn sửa được
--    Khoa.NgayKhoa NOT NULL = đã chốt (không mở khoá lại)
--  * Lịch học gắn với MÔN; kết quả gắn với (sinh viên, môn); không có học lại.
--
--  ⚠️  Script XOÁ database DETAI nếu đã tồn tại.
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

-- 1. Tài khoản
CREATE TABLE TaiKhoan (
    Username VARCHAR(50)  NOT NULL PRIMARY KEY,
    Password VARCHAR(100) NOT NULL,
    HoTen    NVARCHAR(100) NOT NULL,
    VaiTro   VARCHAR(20)  NOT NULL DEFAULT 'Admin',
    CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN ('Admin', 'SinhVien'))
);
GO

-- 2. Khóa (niên khóa) — chốt chương trình bằng NgayKhoa
CREATE TABLE Khoa (
    MaKhoa        VARCHAR(10)   NOT NULL PRIMARY KEY,
    TenKhoa       NVARCHAR(100) NOT NULL,
    NamBatDau     INT           NOT NULL,
    NamKetThuc    INT           NOT NULL,
    TongTCBatBuoc INT           NOT NULL,
    TongTCTuChon  INT           NOT NULL,
    NgayKhoa      DATETIME      NULL,
    NguoiKhoa     VARCHAR(50)   NULL,
    CONSTRAINT CK_Khoa_Nam CHECK (NamKetThuc > NamBatDau),
    CONSTRAINT CK_Khoa_TC  CHECK (TongTCBatBuoc >= 0 AND TongTCTuChon >= 0)
);
GO

-- 3. Sinh viên
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

-- 4. Môn học (danh mục)
CREATE TABLE MonHoc (
    MaMH      VARCHAR(20)   NOT NULL PRIMARY KEY,
    TenMH     NVARCHAR(150) NOT NULL,
    SoTinChi  INT           NOT NULL,
    GiangVien NVARCHAR(100),
    CONSTRAINT CK_MonHoc_TC CHECK (SoTinChi BETWEEN 1 AND 10)
);
GO

-- 5. Chương trình khung của từng khóa (có học kỳ 1..10)
CREATE TABLE ChuongTrinhChiTiet (
    MaKhoa    VARCHAR(10)   NOT NULL,
    MaMH      VARCHAR(20)   NOT NULL,
    BatBuoc   BIT           NOT NULL DEFAULT 1,
    HocKy     TINYINT       NOT NULL DEFAULT 1,
    Thu       TINYINT       NULL,          -- 2 = Thứ 2 … 8 = Chủ nhật; NULL = chưa xếp lịch
    TietBD    TINYINT       NULL,          -- tiết bắt đầu (1..12)
    TietKT    TINYINT       NULL,          -- tiết kết thúc
    GiangVien NVARCHAR(100) NULL,          -- giảng viên dạy lớp này (khác với môn học chung)
    CONSTRAINT PK_ChuongTrinhChiTiet PRIMARY KEY (MaKhoa, MaMH),
    CONSTRAINT FK_CTCT_Khoa FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa),
    CONSTRAINT FK_CTCT_Mon  FOREIGN KEY (MaMH)   REFERENCES MonHoc(MaMH),
    CONSTRAINT CK_CTCT_HocKy CHECK (HocKy BETWEEN 1 AND 10),
    CONSTRAINT CK_CTCT_Thu   CHECK (Thu IS NULL OR Thu BETWEEN 2 AND 8),
    CONSTRAINT CK_CTCT_Tiet  CHECK (TietBD IS NULL OR (TietBD BETWEEN 1 AND 12 AND TietKT >= TietBD AND TietKT <= 12))
);
GO

-- 6. Lịch học — gắn với MÔN
CREATE TABLE LichHoc (
    MaLich   INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
    MaMH     VARCHAR(20)   NOT NULL,
    NgayHoc  DATE          NOT NULL,
    CaHoc    NVARCHAR(50)  NOT NULL,
    TietBD   TINYINT       NOT NULL DEFAULT 1,   -- tiết bắt đầu (1..12) — để vẽ lưới tuần
    TietKT   TINYINT       NOT NULL DEFAULT 1,   -- tiết kết thúc
    PhongHoc VARCHAR(50),
    CONSTRAINT FK_LichHoc_Mon FOREIGN KEY (MaMH) REFERENCES MonHoc(MaMH),
    CONSTRAINT CK_LichHoc_Tiet CHECK (TietBD BETWEEN 1 AND 12 AND TietKT >= TietBD AND TietKT <= 12)
);
GO

-- 7. Kết quả — mỗi (sinh viên, môn) một dòng, điểm là cuối cùng
CREATE TABLE KetQua (
    MaSV    VARCHAR(20)  NOT NULL,
    MaMH    VARCHAR(20)  NOT NULL,
    Diem    DECIMAL(4,2) NULL,
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

-- K24: đã chốt chương trình (NgayKhoa có giá trị). K25: còn sửa được.
INSERT INTO Khoa (MaKhoa, TenKhoa, NamBatDau, NamKetThuc, TongTCBatBuoc, TongTCTuChon, NgayKhoa, NguoiKhoa) VALUES
    ('K24', N'Công nghệ thông tin K24', 2024, 2028, 112, 8, '2026-09-01 08:00:00', 'admin'),
    ('K25', N'Công nghệ thông tin K25', 2025, 2029, 112, 8, NULL, NULL);
GO

INSERT INTO MonHoc (MaMH, TenMH, SoTinChi, GiangVien) VALUES
    ('0101123601', N'Nhập môn ngành Công nghệ thông tin', 3, NULL),
    ('0101123622', N'Tiếng Anh/Tiếng Nhật giao tiếp', 3, NULL),
    ('0101123626', N'Tin học văn phòng', 3, NULL),
    ('0101123627', N'Văn hóa sinh viên BVU', 3, NULL),
    ('0101110018', N'Kỹ thuật lập trình', 3, NULL),
    ('0101123675', N'Tiếng Anh thương mại A1', 3, NULL),
    ('0101123680', N'Toán kỹ thuật', 3, NULL),
    ('0101123740', N'Kiến trúc máy tính và hệ điều hành', 4, NULL),
    ('0101123741', N'Toán chuyên ngành công nghệ thông tin', 3, NULL),
    ('0101110023', N'Lập trình hướng đối tượng', 3, NULL),
    ('0101121610', N'Mạng và thiết bị mạng', 3, NULL),
    ('0101123676', N'Tiếng Anh thương mại A2', 3, NULL),
    ('0101122791', N'Triết học Mác - Lênin', 3, NULL),
    ('0101123677', N'Tiếng Anh thương mại A3', 3, NULL),
    ('0101123742', N'Phân tích thiết kế hệ thống hướng đối tượng', 2, NULL),
    ('0101123764', N'Cơ sở dữ liệu', 4, NULL),
    ('0101122792', N'Kinh tế chính trị Mác - Lênin', 2, NULL),
    ('0101123678', N'Tiếng Anh văn phòng A1', 3, NULL),
    ('0101123743', N'Thiết kế và phát triển website', 3, NULL),
    ('0101123744', N'Giao diện người - máy', 4, NULL),
    ('0101123745', N'Tổ chức dữ liệu và giải thuật', 3, NULL),
    ('0101060020', N'Chủ nghĩa xã hội khoa học', 2, NULL),
    ('0101110021', N'Lập trình .Net', 3, NULL),
    ('0101123679', N'Tiếng Anh văn phòng A2', 3, NULL),
    ('0101060016', N'Tư tưởng Hồ Chí Minh', 2, NULL),
    ('0101122664', N'Luật Lao động', 3, NULL),
    ('0101123121', N'Trí tuệ nhân tạo', 3, NULL),
    ('0101123746', N'Thiết kế và phát triển website mã nguồn mở', 3, NULL),
    ('0101123749', N'Công nghệ phần mềm', 3, NULL),
    ('0101123762', N'Công cụ và môi trường phát triển phần mềm', 3, NULL),
    ('0101060034', N'Lịch sử Đảng Cộng sản Việt Nam', 2, NULL),
    ('0101123447', N'Quản trị doanh nghiệp', 2, NULL),
    ('0101123747', N'Lập trình web với ASP.Net', 3, NULL),
    ('0101123763', N'Kiểm thử và đảm bảo chất lượng phần mềm', 3, NULL),
    ('0101124065', N'Quản lý dự án phần mềm', 3, NULL),
    ('0101124427', N'Thực hành kỹ năng nghề nghiệp', 3, NULL),
    ('0101124440', N'Thực tập tốt nghiệp', 4, NULL),
    ('0101124461', N'Văn hóa doanh nghiệp', 3, NULL),
    ('0101110079', N'Đồ án tốt nghiệp', 8, NULL);
GO

INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc, HocKy) VALUES
    ('K24','0101123601',1,1),
    ('K24','0101123622',1,1),
    ('K24','0101123626',1,1),
    ('K24','0101123627',1,1),
    ('K24','0101110018',1,2),
    ('K24','0101123675',1,2),
    ('K24','0101123680',1,2),
    ('K24','0101123740',1,2),
    ('K24','0101123741',1,2),
    ('K24','0101110023',1,3),
    ('K24','0101121610',1,3),
    ('K24','0101123676',1,3),
    ('K24','0101122791',1,4),
    ('K24','0101123677',1,4),
    ('K24','0101123742',1,4),
    ('K24','0101123764',1,4),
    ('K24','0101122792',1,5),
    ('K24','0101123678',1,5),
    ('K24','0101123743',1,5),
    ('K24','0101123744',1,5),
    ('K24','0101123745',1,5),
    ('K24','0101060020',1,6),
    ('K24','0101110021',1,6),
    ('K24','0101123679',1,6),
    ('K24','0101060016',1,7),
    ('K24','0101122664',1,7),
    ('K24','0101123121',1,7),
    ('K24','0101123746',1,7),
    ('K24','0101123749',1,7),
    ('K24','0101123762',1,7),
    ('K24','0101060034',1,8),
    ('K24','0101123447',1,8),
    ('K24','0101123747',1,8),
    ('K24','0101123763',1,8),
    ('K24','0101124065',1,8),
    ('K24','0101124427',1,9),
    ('K24','0101124440',1,9),
    ('K24','0101124461',1,9),
    ('K24','0101110079',0,10);
GO

INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc, HocKy) VALUES
    ('K25','0101123601',1,1),
    ('K25','0101123622',1,1),
    ('K25','0101123626',1,1),
    ('K25','0101123627',1,1),
    ('K25','0101110018',1,2),
    ('K25','0101123675',1,2),
    ('K25','0101123680',1,2),
    ('K25','0101123740',1,2),
    ('K25','0101123741',1,2),
    ('K25','0101110023',1,3),
    ('K25','0101121610',1,3),
    ('K25','0101123676',1,3),
    ('K25','0101122791',1,4),
    ('K25','0101123677',1,4),
    ('K25','0101123742',1,4),
    ('K25','0101123764',1,4),
    ('K25','0101122792',1,5),
    ('K25','0101123678',1,5),
    ('K25','0101123743',1,5),
    ('K25','0101123744',1,5),
    ('K25','0101123745',1,5),
    ('K25','0101060020',1,6),
    ('K25','0101110021',1,6),
    ('K25','0101123679',1,6),
    ('K25','0101060016',1,7),
    ('K25','0101122664',1,7),
    ('K25','0101123121',1,7),
    ('K25','0101123746',1,7),
    ('K25','0101123749',1,7),
    ('K25','0101123762',1,7),
    ('K25','0101060034',1,8),
    ('K25','0101123447',1,8),
    ('K25','0101123747',1,8),
    ('K25','0101123763',1,8),
    ('K25','0101124065',1,8),
    ('K25','0101124427',1,9),
    ('K25','0101124440',1,9),
    ('K25','0101124461',1,9),
    ('K25','0101110079',0,10);
GO


-- Xếp lịch mẫu (1 buổi/tuần) cho các môn của K24
UPDATE ChuongTrinhChiTiet SET Thu = 2, TietBD = 1,  TietKT = 5, GiangVien = N'ThS. Đặng Thế Hân'            WHERE MaKhoa = 'K24' AND MaMH = '0101123762';
UPDATE ChuongTrinhChiTiet SET Thu = 3, TietBD = 1,  TietKT = 3, GiangVien = N'ThS. Nguyễn Thị Phương Nhung'  WHERE MaKhoa = 'K24' AND MaMH = '0101123622';
UPDATE ChuongTrinhChiTiet SET Thu = 2, TietBD = 6,  TietKT = 9, GiangVien = N'ThS. Huỳnh Mộng Nghi'          WHERE MaKhoa = 'K24' AND MaMH = '0101123626';
UPDATE ChuongTrinhChiTiet SET Thu = 4, TietBD = 1,  TietKT = 5, GiangVien = N'TS. Trần Thị B'                WHERE MaKhoa = 'K24' AND MaMH = '0101123764';
UPDATE ChuongTrinhChiTiet SET Thu = 5, TietBD = 6,  TietKT = 8, GiangVien = N'ThS. Lê Kinh Nam'              WHERE MaKhoa = 'K24' AND MaMH = '0101060016';
UPDATE ChuongTrinhChiTiet SET Thu = 6, TietBD = 1,  TietKT = 3, GiangVien = N'ThS. Đặng Thế Hân'            WHERE MaKhoa = 'K24' AND MaMH = '0101123601';
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

-- Giảng viên phụ trách một số môn (tên thật lấy từ cổng trường)
UPDATE MonHoc SET GiangVien = N'ThS. Đặng Thế Hân'            WHERE MaMH = '0101123762';
UPDATE MonHoc SET GiangVien = N'ThS. Lê Kinh Nam'             WHERE MaMH = '0101060016';
UPDATE MonHoc SET GiangVien = N'ThS. Huỳnh Mộng Nghi'         WHERE MaMH = '0101060034';
UPDATE MonHoc SET GiangVien = N'TS. Trần Thị B'               WHERE MaMH IN ('0101110023','0101123764','0101123745');
UPDATE MonHoc SET GiangVien = N'ThS. Nguyễn Thị Phương Nhung' WHERE MaMH IN ('0101123622','0101123675','0101123676','0101123678','0101123679');
UPDATE MonHoc SET GiangVien = N'ThS. Huỳnh Văn Huy'           WHERE MaMH = '0101121610';
GO

-- Lịch của tuần 02/11/2026 – 08/11/2026 (để màn hình tuần có dữ liệu ngay)
-- Tiết 1-5 = sáng, 6-9 = chiều, 10-12 = tối.
INSERT INTO LichHoc (MaMH, NgayHoc, CaHoc, TietBD, TietKT, PhongHoc) VALUES
    ('0101123762', '2026-11-02', N'06:40 - 11:05', 1, 5, '3C09-PM07-CS3'),
    ('0101123626', '2026-11-02', N'13:00 - 16:55', 6, 9, '1GD5-CS1'),
    ('0101123622', '2026-11-03', N'06:40 - 09:10', 1, 3, '1GD5-CS1'),
    ('0101122664', '2026-11-03', N'13:20 - 16:55', 6, 9, '1GD5-CS1'),
    ('0101123121', '2026-11-04', N'06:40 - 11:05', 1, 5, '3C05-PM03-CS3'),
    ('0101123749', '2026-11-04', N'17:30 - 20:00', 10, 12, '2A01-CS2'),
    ('0101123764', '2026-11-05', N'06:40 - 11:05', 1, 5, '3B01-GD1-CS3'),
    ('0101060016', '2026-11-05', N'12:30 - 15:00', 6, 8, '2503-CS2'),
    ('0101123601', '2026-11-06', N'06:40 - 09:10', 1, 3, 'P.301'),
    ('0101123762', '2026-11-06', N'13:00 - 16:55', 6, 9, '3C09-PM07-CS3'),
    ('0101123627', '2026-11-07', N'06:40 - 09:10', 1, 3, 'P.102'),
    ('0101123601', '2026-11-08', N'06:40 - 11:05', 1, 5, 'P.301');
GO

INSERT INTO KetQua (MaSV, MaMH, Diem) VALUES
    ('SV001','0101123601', 3.20), ('SV001','0101123622', 2.50),
    ('SV001','0101110018', 1.80), ('SV001','0101123764', NULL),
    ('SV002','0101123601', 3.80), ('SV002','0101123622', 3.10), ('SV002','0101110018', 2.90),
    ('SV003','0101123601', 1.50), ('SV003','0101123622', 2.10),
    ('SV004','0101123601', 3.00), ('SV004','0101123622', 2.00),
    ('SV005','0101123601', 2.70), ('SV005','0101110023', 3.30),
    ('SV006','0101123601', 2.20),
    ('SV101','0101123601', 3.50), ('SV101','0101110023', 3.90);
GO

-- =====================================================================
--  KIỂM TRA NHANH
-- =====================================================================
-- 7 bảng:            SELECT name FROM sys.tables ORDER BY name;
-- Tổng TC theo khóa: SELECT c.MaKhoa, SUM(m.SoTinChi) FROM ChuongTrinhChiTiet c
--                    JOIN MonHoc m ON m.MaMH = c.MaMH GROUP BY c.MaKhoa;
-- TC tích luỹ:       SELECT k.MaSV, SUM(m.SoTinChi) FROM KetQua k
--                    JOIN MonHoc m ON m.MaMH = k.MaMH
--                    JOIN SinhVien sv ON sv.MaSV = k.MaSV
--                    JOIN ChuongTrinhChiTiet c ON c.MaMH = k.MaMH AND c.MaKhoa = sv.MaKhoa
--                    WHERE k.Diem >= 2 GROUP BY k.MaSV;