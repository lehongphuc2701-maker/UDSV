using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Truy cập bảng KetQua — điểm của từng sinh viên ở từng môn.</summary>
    public class KetQuaDAL : IKetQuaDAL
    {
        public DataTable LayTheoSinhVien(string maSV)
        {
            const string query = @"
                SELECT k.MaMH,
                       m.TenMH,
                       m.SoTinChi,
                       k.Diem,
                       k.LanNhap
                FROM KetQua k
                INNER JOIN MonHoc m ON m.MaMH = k.MaMH
                WHERE k.MaSV = @MaSV
                ORDER BY k.MaMH";

            SqlParameter p = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" };
            return DatabaseHelper.GetData(query, p);
        }

        public DataTable LayTheoMon(string maMH)
        {
            const string query = @"
                SELECT k.MaSV,
                       sv.HoTen,
                       k.Diem,
                       k.LanNhap
                FROM KetQua k
                INNER JOIN SinhVien sv ON sv.MaSV = k.MaSV
                WHERE k.MaMH = @MaMH
                ORDER BY k.MaSV";

            SqlParameter p = new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH ?? "" };
            return DatabaseHelper.GetData(query, p);
        }

        public bool KiemTraDaCo(string maSV, string maMH)
        {
            const string query = "SELECT COUNT(*) FROM KetQua WHERE MaSV = @MaSV AND MaMH = @MaMH";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, KeyParams(maSV, maMH))) > 0;
        }

        public bool Them(KetQuaDTO kq)
        {
            const string query = @"INSERT INTO KetQua (MaSV, MaMH, Diem, LanNhap)
                                   VALUES (@MaSV, @MaMH, @Diem, GETDATE())";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = kq.MaSV ?? "" },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = kq.MaMH ?? "" },
                new SqlParameter("@Diem", SqlDbType.Decimal) { Precision = 4, Scale = 2,
                                                               Value = (object?)kq.Diem ?? DBNull.Value }
            };
            return DatabaseHelper.ExecuteNonQuery(query, ps) == 1;
        }

        public bool Sua(KetQuaDTO kq)
        {
            const string query = @"UPDATE KetQua SET Diem = @Diem, LanNhap = GETDATE()
                                   WHERE MaSV = @MaSV AND MaMH = @MaMH";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = kq.MaSV ?? "" },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = kq.MaMH ?? "" },
                new SqlParameter("@Diem", SqlDbType.Decimal) { Precision = 4, Scale = 2,
                                                               Value = (object?)kq.Diem ?? DBNull.Value }
            };
            return DatabaseHelper.ExecuteNonQuery(query, ps) == 1;
        }

        public bool Xoa(string maSV, string maMH)
        {
            const string query = "DELETE FROM KetQua WHERE MaSV = @MaSV AND MaMH = @MaMH";
            return DatabaseHelper.ExecuteNonQuery(query, KeyParams(maSV, maMH)) == 1;
        }

        /// <summary>
        /// Chỉ tính môn NẰM TRONG chương trình của khóa sinh viên đang học, và điểm >= 2.0.
        /// Đây là truy vấn duy nhất trả lời "còn thiếu bao nhiêu tín chỉ".
        /// </summary>
        public int TinhTinChiTichLuy(string maSV)
        {
            const string query = @"
                SELECT ISNULL(SUM(m.SoTinChi), 0)
                FROM KetQua k
                INNER JOIN MonHoc m             ON m.MaMH  = k.MaMH
                INNER JOIN SinhVien sv          ON sv.MaSV = k.MaSV
                INNER JOIN ChuongTrinhChiTiet c ON c.MaMH  = k.MaMH AND c.MaKhoa = sv.MaKhoa
                WHERE k.MaSV = @MaSV AND k.Diem >= 2";

            SqlParameter p = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p));
        }

        private static SqlParameter[] KeyParams(string maSV, string maMH)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH ?? "" }
            };
        }
    }
}
