using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Truy cập bảng ChuongTrinhChiTiet — chương trình của từng khóa.</summary>
    public class ChuongTrinhDAL : IChuongTrinhDAL
    {
        public DataTable LayTheoKhoa(string maKhoa)
        {
            const string query = @"
                SELECT c.MaKhoa,
                       c.MaMH,
                       m.TenMH,
                       m.SoTinChi,
                       c.BatBuoc
                FROM ChuongTrinhChiTiet c
                INNER JOIN MonHoc m ON m.MaMH = c.MaMH
                WHERE c.MaKhoa = @MaKhoa
                ORDER BY c.BatBuoc DESC, c.MaMH";

            SqlParameter p = new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" };
            return DatabaseHelper.GetData(query, p);
        }

        public bool KiemTraMonDaCo(string maKhoa, string maMH)
        {
            const string query = @"SELECT COUNT(*) FROM ChuongTrinhChiTiet
                                   WHERE MaKhoa = @MaKhoa AND MaMH = @MaMH";
            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH ?? "" }
            };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, ps)) > 0;
        }

        public bool Them(ChuongTrinhDTO dong)
        {
            const string query = @"INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc)
                                   VALUES (@MaKhoa, @MaMH, @BatBuoc)";
            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = dong.MaKhoa ?? "" },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = dong.MaMH ?? "" },
                new SqlParameter("@BatBuoc", SqlDbType.Bit) { Value = dong.BatBuoc }
            };
            return DatabaseHelper.ExecuteNonQuery(query, ps) == 1;
        }

        public bool Xoa(string maKhoa, string maMH)
        {
            const string query = @"DELETE FROM ChuongTrinhChiTiet
                                   WHERE MaKhoa = @MaKhoa AND MaMH = @MaMH";
            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH ?? "" }
            };
            return DatabaseHelper.ExecuteNonQuery(query, ps) == 1;
        }

        public int TongTinChi(string maKhoa, bool batBuoc)
        {
            const string query = @"
                SELECT ISNULL(SUM(m.SoTinChi), 0)
                FROM ChuongTrinhChiTiet c
                INNER JOIN MonHoc m ON m.MaMH = c.MaMH
                WHERE c.MaKhoa = @MaKhoa AND c.BatBuoc = @BatBuoc";

            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@BatBuoc", SqlDbType.Bit) { Value = batBuoc }
            };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, ps));
        }
    }
}
