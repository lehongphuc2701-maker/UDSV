using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Truy cập bảng Khoa (niên khóa).</summary>
    public class KhoaDAL : IKhoaDAL
    {
        private const string SelectColumns =
            "MaKhoa, TenKhoa, NamBatDau, NamKetThuc, TongTCBatBuoc, TongTCTuChon, NgayKhoa, NguoiKhoa";

        public DataTable LayDanhSach()
        {
            string query = $@"SELECT {SelectColumns} FROM Khoa ORDER BY MaKhoa";
            return DatabaseHelper.GetData(query);
        }

        public KhoaDTO? LayTheoMa(string maKhoa)
        {
            string query = $"SELECT {SelectColumns} FROM Khoa WHERE MaKhoa = @MaKhoa";
            SqlParameter p = new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" };
            DataTable dt = DatabaseHelper.GetData(query, p);
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new KhoaDTO(
                r["MaKhoa"].ToString() ?? "",
                r["TenKhoa"].ToString() ?? "",
                Convert.ToInt32(r["NamBatDau"]),
                Convert.ToInt32(r["NamKetThuc"]),
                Convert.ToInt32(r["TongTCBatBuoc"]),
                Convert.ToInt32(r["TongTCTuChon"]),
                r["NgayKhoa"] == DBNull.Value ? null : Convert.ToDateTime(r["NgayKhoa"]),
                r["NguoiKhoa"] == DBNull.Value ? null : r["NguoiKhoa"].ToString());
        }

        public bool KiemTraTonTai(string maKhoa)
        {
            const string query = "SELECT COUNT(*) FROM Khoa WHERE MaKhoa = @MaKhoa";
            SqlParameter p = new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p)) > 0;
        }

        public int DemSinhVien(string maKhoa)
        {
            const string query = "SELECT COUNT(*) FROM SinhVien WHERE MaKhoa = @MaKhoa";
            SqlParameter p = new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p));
        }

        public bool Them(KhoaDTO khoa)
        {
            const string query = @"INSERT INTO Khoa
                (MaKhoa, TenKhoa, NamBatDau, NamKetThuc, TongTCBatBuoc, TongTCTuChon, NgayKhoa, NguoiKhoa)
                VALUES (@MaKhoa, @TenKhoa, @NamBatDau, @NamKetThuc, @TongTCBatBuoc, @TongTCTuChon, NULL, NULL)";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(khoa)) == 1;
        }

        public bool Sua(KhoaDTO khoa)
        {
            const string query = @"UPDATE Khoa
                SET TenKhoa = @TenKhoa, NamBatDau = @NamBatDau, NamKetThuc = @NamKetThuc,
                    TongTCBatBuoc = @TongTCBatBuoc, TongTCTuChon = @TongTCTuChon
                WHERE MaKhoa = @MaKhoa";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(khoa)) == 1;
        }

        public bool Xoa(string maKhoa)
        {
            const string query = "DELETE FROM Khoa WHERE MaKhoa = @MaKhoa";
            SqlParameter p = new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" };
            return DatabaseHelper.ExecuteNonQuery(query, p) == 1;
        }

        public bool KhoaChuongTrinh(string maKhoa, string nguoiKhoa, int tongTCBatBuoc, int tongTCTuChon)
        {
            // WHERE NgayKhoa IS NULL: bấm lần hai không ghi đè được người/giờ khoá đầu tiên
            const string query = @"UPDATE Khoa
                SET NgayKhoa = GETDATE(), NguoiKhoa = @NguoiKhoa,
                    TongTCBatBuoc = @TongTCBatBuoc, TongTCTuChon = @TongTCTuChon
                WHERE MaKhoa = @MaKhoa AND NgayKhoa IS NULL";

            SqlParameter[] ps =
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa ?? "" },
                new SqlParameter("@NguoiKhoa", SqlDbType.VarChar, 50) { Value = (object?)nguoiKhoa ?? DBNull.Value },
                new SqlParameter("@TongTCBatBuoc", SqlDbType.Int) { Value = tongTCBatBuoc },
                new SqlParameter("@TongTCTuChon", SqlDbType.Int) { Value = tongTCTuChon }
            };
            return DatabaseHelper.ExecuteNonQuery(query, ps) == 1;
        }

        private static SqlParameter[] BuildParameters(KhoaDTO khoa)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = khoa.MaKhoa ?? "" },
                new SqlParameter("@TenKhoa", SqlDbType.NVarChar, 100) { Value = khoa.TenKhoa ?? "" },
                new SqlParameter("@NamBatDau", SqlDbType.Int) { Value = khoa.NamBatDau },
                new SqlParameter("@NamKetThuc", SqlDbType.Int) { Value = khoa.NamKetThuc },
                new SqlParameter("@TongTCBatBuoc", SqlDbType.Int) { Value = khoa.TongTCBatBuoc },
                new SqlParameter("@TongTCTuChon", SqlDbType.Int) { Value = khoa.TongTCTuChon }
            };
        }
    }
}
