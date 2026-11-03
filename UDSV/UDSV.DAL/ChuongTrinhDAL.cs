using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Truy cập bảng ChuongTrinhChiTiet — chương trình khung của từng khóa.</summary>
    public class ChuongTrinhDAL : IChuongTrinhDAL
    {
        public DataTable LayTheoKhoa(string maKhoa)
        {
            const string query = @"
                SELECT c.MaKhoa,
                       c.MaMH,
                       m.TenMH,
                       m.SoTinChi,
                       c.BatBuoc,
                       c.HocKy
                FROM ChuongTrinhChiTiet c
                INNER JOIN MonHoc m ON m.MaMH = c.MaMH
                WHERE c.MaKhoa = @MaKhoa
                ORDER BY c.HocKy, c.MaMH";

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
            const string query = @"INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc, HocKy)
                                   VALUES (@MaKhoa, @MaMH, @BatBuoc, @HocKy)";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(dong)) == 1;
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

        /// <summary>
        /// Xoá và ghi lại toàn bộ chương trình trong một transaction — hoặc xong hết, hoặc không đổi gì.
        /// Lý do phải dùng transaction: nếu lỗi giữa đường, chương trình của khóa không được phép
        /// ở trạng thái nửa cũ nửa mới.
        /// </summary>
        public bool LuuCaChuongTrinh(string maKhoa, IList<ChuongTrinhDTO> danhSach)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand del = new SqlCommand(
                                   "DELETE FROM ChuongTrinhChiTiet WHERE MaKhoa = @MaKhoa", conn, tx))
                        {
                            del.Parameters.AddWithValue("@MaKhoa", maKhoa ?? "");
                            del.ExecuteNonQuery();
                        }

                        foreach (ChuongTrinhDTO dong in danhSach)
                        {
                            using (SqlCommand ins = new SqlCommand(
                                       @"INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc, HocKy)
                                         VALUES (@MaKhoa, @MaMH, @BatBuoc, @HocKy)", conn, tx))
                            {
                                ins.Parameters.AddWithValue("@MaKhoa", maKhoa ?? "");
                                ins.Parameters.AddWithValue("@MaMH", dong.MaMH ?? "");
                                ins.Parameters.AddWithValue("@BatBuoc", dong.BatBuoc);
                                ins.Parameters.AddWithValue("@HocKy", dong.HocKy);
                                ins.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        AppLog.For<ChuongTrinhDAL>().LogError(ex, "Lưu chương trình {MaKhoa} thất bại, đã rollback", maKhoa);
                        throw;
                    }
                }
            }
        }

        public int DemSinhVienDaCoDiem(string maKhoa, string maMH)
        {
            const string query = @"
                SELECT COUNT(DISTINCT k.MaSV)
                FROM KetQua k
                INNER JOIN SinhVien sv ON sv.MaSV = k.MaSV
                WHERE sv.MaKhoa = @MaKhoa AND k.MaMH = @MaMH";

            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH ?? "" }
            };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, ps));
        }

        private static SqlParameter[] BuildParameters(ChuongTrinhDTO dong)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = dong.MaKhoa ?? "" },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = dong.MaMH ?? "" },
                new SqlParameter("@BatBuoc", SqlDbType.Bit) { Value = dong.BatBuoc },
                new SqlParameter("@HocKy", SqlDbType.TinyInt) { Value = dong.HocKy }
            };
        }

        public DataTable LayHocKy(string maKhoa)
        {
            const string query = @"
                SELECT DISTINCT HocKy
                FROM ChuongTrinhChiTiet
                WHERE MaKhoa = @MaKhoa
                ORDER BY HocKy";

            SqlParameter p = new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" };
            return DatabaseHelper.GetData(query, p);
        }

        public DataTable LayMonTheoKy(string maKhoa, int hocKy)
        {
            const string query = @"
                SELECT c.MaMH, m.TenMH, m.SoTinChi
                FROM ChuongTrinhChiTiet c
                INNER JOIN MonHoc m ON m.MaMH = c.MaMH
                WHERE c.MaKhoa = @MaKhoa AND c.HocKy = @HocKy
                ORDER BY c.MaMH";

            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@HocKy", SqlDbType.TinyInt) { Value = hocKy }
            };
            return DatabaseHelper.GetData(query, ps);
        }
    }
}
