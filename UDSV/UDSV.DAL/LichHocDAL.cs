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
            "lh.MaLich, lh.MaMH, m.TenMH, lh.NgayHoc, lh.CaHoc, lh.PhongHoc";

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

        private static SqlParameter[] BuildParameters(LichHocDTO lh)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = lh.MaMH ?? "" },
                new SqlParameter("@NgayHoc", SqlDbType.Date) { Value = lh.NgayHoc },
                new SqlParameter("@CaHoc", SqlDbType.NVarChar, 50) { Value = lh.CaHoc ?? "" },
                new SqlParameter("@PhongHoc", SqlDbType.VarChar, 50) { Value = (object?)lh.PhongHoc ?? DBNull.Value }
            };
        }
    }
}
