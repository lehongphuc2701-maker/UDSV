using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>
    /// Tầng truy cập dữ liệu của bảng SinhVien.
    /// Chỉ chứa SQL; không kiểm tra nghiệp vụ (việc đó ở BLL), không hiển thị gì (việc đó ở UI).
    /// </summary>
    public class SinhVienDAL : ISinhVienDAL
    {
        // Kèm TenKhoa để lưới hiện tên khóa thay vì chỉ mã
        private const string SelectColumns =
            "sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.MaKhoa, kh.TenKhoa, sv.SDT, sv.TrangThai";

        private const string FromJoin =
            "FROM SinhVien sv INNER JOIN Khoa kh ON kh.MaKhoa = sv.MaKhoa";

        /// <summary>Danh sách sinh viên, lọc theo trạng thái; null hoặc rỗng = tất cả.</summary>
        public DataTable LayDanhSachTheoTrangThai(string? trangThai)
        {
            string query = $"SELECT {SelectColumns} {FromJoin}";
            if (!string.IsNullOrWhiteSpace(trangThai))
                query += " WHERE sv.TrangThai = @TrangThai";
            query += " ORDER BY sv.MaSV";

            SqlParameter[] ps = string.IsNullOrWhiteSpace(trangThai)
                ? Array.Empty<SqlParameter>()
                : new[] { new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = trangThai } };
            return DatabaseHelper.GetData(query, ps);
        }

        /// <summary>Đổi trạng thái sinh viên ('Đã xóa' khi xoá mềm, 'Chưa tốt nghiệp' khi phục hồi).</summary>
        public bool DoiTrangThai(string maSV, string trangThai)
        {
            const string query = "UPDATE SinhVien SET TrangThai = @TrangThai WHERE MaSV = @MaSV";
            SqlParameter[] ps =
            {
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = trangThai ?? "" },
                new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" }
            };
            return DatabaseHelper.ExecuteNonQuery(query, ps) > 0;
        }

        public DataTable LayDanhSachSinhVien()
        {
            string query = $"SELECT {SelectColumns} {FromJoin} ORDER BY sv.MaSV";
            return DatabaseHelper.GetData(query);
        }

        public DataTable TimKiemSinhVien(string keyword)
        {
            string query = $@"SELECT {SelectColumns} {FromJoin}
                              WHERE sv.MaSV LIKE @Keyword OR sv.HoTen LIKE @Keyword
                              ORDER BY sv.MaSV";

            SqlParameter keywordParam = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100)
            {
                Value = "%" + (keyword ?? "") + "%"
            };
            return DatabaseHelper.GetData(query, keywordParam);
        }

        public DataTable LaySinhVienTheoMa(string maSV)
        {
            string query = $"SELECT {SelectColumns} {FromJoin} WHERE sv.MaSV = @MaSV";
            SqlParameter p = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" };
            return DatabaseHelper.GetData(query, p);
        }

        public bool KiemTraMaTonTai(string maSV)
        {
            const string query = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";
            SqlParameter p = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p)) > 0;
        }

        public bool ThemSinhVien(SinhVienDTO sv)
        {
            const string query = @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, MaKhoa, SDT)
                                   VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @MaKhoa, @SDT)";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(sv)) == 1;
        }

        public bool SuaSinhVien(SinhVienDTO sv)
        {
            const string query = @"UPDATE SinhVien
                                   SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh,
                                       MaKhoa = @MaKhoa, SDT = @SDT
                                   WHERE MaSV = @MaSV";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(sv)) == 1;
        }

        public bool XoaSinhVien(string maSV)
        {
            const string query = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
            SqlParameter p = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV ?? "" };
            return DatabaseHelper.ExecuteNonQuery(query, p) == 1;
        }

        private static SqlParameter[] BuildParameters(SinhVienDTO sv)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = sv.MaSV ?? "" },
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = sv.HoTen ?? "" },
                new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = sv.NgaySinh },
                new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10) { Value = (object?)sv.GioiTinh ?? DBNull.Value },
                new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = sv.MaKhoa ?? "" },
                new SqlParameter("@SDT", SqlDbType.VarChar, 15) { Value = (object?)sv.SDT ?? DBNull.Value }
            };
        }
    }
}
