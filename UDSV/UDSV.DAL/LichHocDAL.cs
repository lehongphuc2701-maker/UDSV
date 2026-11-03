using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>
    /// Truy cập bảng LichHoc. Lịch gắn với MÔN: một buổi học thuộc về môn, không thuộc về khóa
    /// và không thuộc về sinh viên. Ai đang học môn đó thì thấy buổi học đó.
    /// </summary>
    public class LichHocDAL : ILichHocDAL
    {
        private const string SelectColumns =
            "lh.MaLich, lh.MaMH, m.TenMH, lh.NgayHoc, lh.CaHoc, lh.TietBD, lh.TietKT, lh.PhongHoc";

        private const string FromJoin =
            "FROM LichHoc lh INNER JOIN MonHoc m ON m.MaMH = lh.MaMH";

        public DataTable LayDanhSachLichHoc()
        {
            string query = $"SELECT {SelectColumns} {FromJoin} ORDER BY lh.NgayHoc, lh.CaHoc";
            return DatabaseHelper.GetData(query);
        }

        public DataTable LayTheoMon(string maMH)
        {
            string query = $"SELECT {SelectColumns} {FromJoin} WHERE lh.MaMH = @MaMH ORDER BY lh.NgayHoc";
            SqlParameter p = new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH ?? "" };
            return DatabaseHelper.GetData(query, p);
        }

        /// <summary>
        /// Thời khoá biểu của một sinh viên: các buổi học của những môn mà sinh viên có dòng KetQua.
        /// KetQua chính là "đã ghi danh" — không cần bảng đăng ký riêng.
        /// </summary>
        public DataTable LayTheoSinhVien(string maSV)
        {
            const string query = @"
                SELECT lh.MaLich, lh.MaMH, m.TenMH, lh.NgayHoc, lh.CaHoc, lh.PhongHoc
                FROM LichHoc lh
                INNER JOIN MonHoc m ON m.MaMH = lh.MaMH
                INNER JOIN KetQua kq ON kq.MaMH = lh.MaMH
                WHERE kq.MaSV = @MaSV
                ORDER BY lh.NgayHoc, lh.CaHoc";

            SqlParameter p = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" };
            return DatabaseHelper.GetData(query, p);
        }

        public bool ThemLichHoc(LichHocDTO lh)
        {
            const string query = @"INSERT INTO LichHoc (MaMH, NgayHoc, CaHoc, PhongHoc)
                                   VALUES (@MaMH, @NgayHoc, @CaHoc, @PhongHoc)";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(lh)) == 1;
        }

        public bool SuaLichHoc(LichHocDTO lh)
        {
            const string query = @"UPDATE LichHoc
                                   SET MaMH = @MaMH, NgayHoc = @NgayHoc, CaHoc = @CaHoc, PhongHoc = @PhongHoc
                                   WHERE MaLich = @MaLich";

            SqlParameter[] ps = BuildParameters(lh);
            Array.Resize(ref ps, ps.Length + 1);
            ps[ps.Length - 1] = new SqlParameter("@MaLich", SqlDbType.Int) { Value = lh.MaLich };

            return DatabaseHelper.ExecuteNonQuery(query, ps) == 1;
        }

        public bool XoaLichHoc(int maLich)
        {
            const string query = "DELETE FROM LichHoc WHERE MaLich = @MaLich";
            SqlParameter p = new SqlParameter("@MaLich", SqlDbType.Int) { Value = maLich };
            return DatabaseHelper.ExecuteNonQuery(query, p) == 1;
        }

        /// <summary>
        /// Buổi học của một khóa trong một học kỳ, giới hạn theo tuần.
        /// Lọc qua ChuongTrinhChiTiet để biết môn nào thuộc học kỳ đó của khóa.
        /// </summary>
        public DataTable LayTheoTuan(string maKhoa, int hocKy, DateTime tuNgay, DateTime denNgay)
        {
            const string query = @"
                SELECT lh.MaLich, lh.MaMH, m.TenMH, m.GiangVien,
                       lh.NgayHoc, lh.CaHoc, lh.TietBD, lh.TietKT, lh.PhongHoc
                FROM LichHoc lh
                INNER JOIN MonHoc m             ON m.MaMH  = lh.MaMH
                INNER JOIN ChuongTrinhChiTiet c ON c.MaMH  = lh.MaMH
                WHERE c.MaKhoa = @MaKhoa AND c.HocKy = @HocKy
                  AND lh.NgayHoc >= @TuNgay AND lh.NgayHoc <= @DenNgay
                ORDER BY lh.NgayHoc, lh.TietBD";

            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@HocKy", SqlDbType.TinyInt) { Value = hocKy },
                new SqlParameter("@TuNgay", SqlDbType.Date) { Value = tuNgay.Date },
                new SqlParameter("@DenNgay", SqlDbType.Date) { Value = denNgay.Date }
            };
            return DatabaseHelper.GetData(query, ps);
        }

        private static SqlParameter[] BuildParameters(LichHocDTO lh)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = lh.MaMH ?? "" },
                new SqlParameter("@NgayHoc", SqlDbType.Date) { Value = lh.NgayHoc },
                new SqlParameter("@CaHoc", SqlDbType.NVarChar, 50) { Value = lh.CaHoc ?? "" },
                new SqlParameter("@TietBD", SqlDbType.TinyInt) { Value = lh.TietBD },
                new SqlParameter("@TietKT", SqlDbType.TinyInt) { Value = lh.TietKT },
                new SqlParameter("@PhongHoc", SqlDbType.VarChar, 50) { Value = (object?)lh.PhongHoc ?? DBNull.Value }
            };
        }
    }
}
