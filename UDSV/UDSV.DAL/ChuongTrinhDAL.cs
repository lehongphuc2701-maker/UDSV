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
                       c.HocKy,
                       c.Thu,
                       c.TietBD,
                       c.TietKT,
                       c.GiangVien,
                       c.PhongHoc
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
            const string query = @"INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc, HocKy, Thu, TietBD, TietKT, GiangVien, PhongHoc)
                                   VALUES (@MaKhoa, @MaMH, @BatBuoc, @HocKy, @Thu, @TietBD, @TietKT, @GiangVien, @PhongHoc)";
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
                                       @"INSERT INTO ChuongTrinhChiTiet (MaKhoa, MaMH, BatBuoc, HocKy, Thu, TietBD, TietKT, GiangVien, PhongHoc)
                                         VALUES (@MaKhoa, @MaMH, @BatBuoc, @HocKy, @Thu, @TietBD, @TietKT, @GiangVien, @PhongHoc)", conn, tx))
                            {
                                ins.Parameters.AddWithValue("@MaKhoa", maKhoa ?? "");
                                ins.Parameters.AddWithValue("@MaMH", dong.MaMH ?? "");
                                ins.Parameters.AddWithValue("@BatBuoc", dong.BatBuoc);
                                ins.Parameters.AddWithValue("@HocKy", dong.HocKy);
                                ins.Parameters.AddWithValue("@Thu", (object?)dong.Thu ?? DBNull.Value);
                                ins.Parameters.AddWithValue("@TietBD", (object?)dong.TietBD ?? DBNull.Value);
                                ins.Parameters.AddWithValue("@TietKT", (object?)dong.TietKT ?? DBNull.Value);
                                ins.Parameters.AddWithValue("@GiangVien", (object?)dong.GiangVien ?? DBNull.Value);
                                ins.Parameters.AddWithValue("@PhongHoc", (object?)dong.PhongHoc ?? DBNull.Value);
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
                new SqlParameter("@HocKy", SqlDbType.TinyInt) { Value = dong.HocKy },
                new SqlParameter("@Thu", SqlDbType.TinyInt) { Value = (object?)dong.Thu ?? DBNull.Value },
                new SqlParameter("@TietBD", SqlDbType.TinyInt) { Value = (object?)dong.TietBD ?? DBNull.Value },
                new SqlParameter("@TietKT", SqlDbType.TinyInt) { Value = (object?)dong.TietKT ?? DBNull.Value },
                new SqlParameter("@GiangVien", SqlDbType.NVarChar, 100) { Value = (object?)dong.GiangVien ?? DBNull.Value },
                new SqlParameter("@PhongHoc", SqlDbType.VarChar, 50) { Value = (object?)dong.PhongHoc ?? DBNull.Value }
            };
        }

        /// <summary>
        /// Thời khoá biểu mẫu của một khóa + học kỳ: mỗi môn một buổi/tuần (Thứ, Tiết BĐ, Tiết KT, GV).
        /// Đây là nguồn cho lưới 7 ngày × 12 tiết.
        /// </summary>
        /// <summary>
        /// Chương trình của khóa trong một học kỳ, kèm điểm của sinh viên (null nếu chưa có điểm).
        /// Dùng cho màn CT Đào tạo ở phía sinh viên.
        /// </summary>
        public DataTable LayChuongTrinhCoDiem(string maKhoa, int hocKy, string maSV)
        {
            const string query = @"
                SELECT ct.MaMH, m.TenMH, m.SoTinChi, ct.BatBuoc,
                       ct.Thu, ct.TietBD, ct.TietKT, ct.GiangVien, ct.PhongHoc,
                       kq.Diem
                FROM ChuongTrinhChiTiet ct
                INNER JOIN MonHoc m ON m.MaMH = ct.MaMH
                LEFT JOIN KetQua kq ON kq.MaMH = ct.MaMH AND kq.MaSV = @MaSV
                WHERE ct.MaKhoa = @MaKhoa AND ct.HocKy = @HocKy
                ORDER BY ct.MaMH";

            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@HocKy", SqlDbType.TinyInt) { Value = hocKy },
                new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" }
            };
            return DatabaseHelper.GetData(query, ps);
        }

        public DataTable LayTuanMau(string maKhoa, int hocKy)
        {
            const string query = @"
                SELECT c.MaMH, m.TenMH, c.Thu, c.TietBD, c.TietKT, c.GiangVien, c.PhongHoc
                FROM ChuongTrinhChiTiet c
                INNER JOIN MonHoc m ON m.MaMH = c.MaMH
                WHERE c.MaKhoa = @MaKhoa AND c.HocKy = @HocKy AND c.Thu IS NOT NULL
                ORDER BY c.Thu, c.TietBD";

            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@HocKy", SqlDbType.TinyInt) { Value = hocKy }
            };
            return DatabaseHelper.GetData(query, ps);
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
            // Môn của học kỳ + MỌI môn mà sinh viên của khóa này đã có điểm.
            // Nhánh sau cố tình KHÔNG lọc cờ xoá / trạng thái: điểm đã nhập phải luôn xem được.
            const string query = @"
                SELECT m.MaMH, m.TenMH, m.SoTinChi,
                       CAST(CASE WHEN c.MaMH IS NULL THEN 1 ELSE 0 END AS BIT) AS NgoaiChuongTrinh
                FROM MonHoc m
                LEFT JOIN ChuongTrinhChiTiet c
                       ON c.MaMH = m.MaMH AND c.MaKhoa = @MaKhoa AND c.HocKy = @HocKy
                WHERE c.MaMH IS NOT NULL
                   OR m.MaMH IN (SELECT kq.MaMH
                                 FROM KetQua kq
                                 INNER JOIN SinhVien sv ON sv.MaSV = kq.MaSV
                                 WHERE sv.MaKhoa = @MaKhoa)
                ORDER BY m.MaMH";

            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@HocKy", SqlDbType.TinyInt) { Value = hocKy }
            };
            return DatabaseHelper.GetData(query, ps);
        }
    }
}
