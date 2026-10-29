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
        private const string SelectColumns = "MaSV, HoTen, NgaySinh, GioiTinh, Lop, SDT, DiemTB";

        // 1. Lấy toàn bộ danh sách sinh viên
        public DataTable LayDanhSachSinhVien()
        {
            string query = $"SELECT {SelectColumns} FROM SinhVien ORDER BY MaSV";
            return DatabaseHelper.GetData(query);
        }

        // 2. Tìm theo mã hoặc họ tên (LIKE, tham số hoá)
        public DataTable TimKiemSinhVien(string keyword)
        {
            string query = $@"SELECT {SelectColumns} FROM SinhVien
                              WHERE MaSV LIKE @Keyword OR HoTen LIKE @Keyword
                              ORDER BY MaSV";

            SqlParameter keywordParam = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100)
            {
                Value = "%" + keyword + "%"
            };
            return DatabaseHelper.GetData(query, keywordParam);
        }

        // 3. Lấy một sinh viên theo mã
        public DataTable LaySinhVienTheoMa(string maSV)
        {
            string query = $"SELECT {SelectColumns} FROM SinhVien WHERE MaSV = @MaSV";
            SqlParameter maParam = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV };
            return DatabaseHelper.GetData(query, maParam);
        }

        // 4. Kiểm tra mã đã tồn tại chưa (dùng ở BLL trước khi thêm)
        public bool KiemTraMaTonTai(string maSV)
        {
            string query = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";
            SqlParameter maParam = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV };
            object result = DatabaseHelper.ExecuteScalar(query, maParam);
            return Convert.ToInt32(result) > 0;
        }

        // 5. Thêm sinh viên mới
        public bool ThemSinhVien(SinhVienDTO sv)
        {
            string query = @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Lop, SDT, DiemTB)
                             VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Lop, @SDT, @DiemTB)";

            int rows = DatabaseHelper.ExecuteNonQuery(query, BuildParameters(sv));
            return rows == 1;
        }

        // 6. Cập nhật sinh viên (mã sinh viên là khoá nên không đổi)
        public bool SuaSinhVien(SinhVienDTO sv)
        {
            string query = @"UPDATE SinhVien
                             SET HoTen = @HoTen, NgaySinh = @NgaySinh,
                                 GioiTinh = @GioiTinh, Lop = @Lop, SDT = @SDT, DiemTB = @DiemTB
                             WHERE MaSV = @MaSV";

            int rows = DatabaseHelper.ExecuteNonQuery(query, BuildParameters(sv));
            return rows == 1;
        }

        // 7. Xoá sinh viên theo mã
        public bool XoaSinhVien(string maSV)
        {
            string query = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
            SqlParameter maParam = new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = maSV };
            int rows = DatabaseHelper.ExecuteNonQuery(query, maParam);
            return rows == 1;
        }

        /// <summary>
        /// Gom tham số dùng chung cho INSERT và UPDATE.
        /// Dùng kiểu dữ liệu tường minh thay vì AddWithValue để tránh SQL Server tự đoán kiểu.
        /// </summary>
        private static SqlParameter[] BuildParameters(SinhVienDTO sv)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = sv.MaSV },
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = sv.HoTen },
                new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = sv.NgaySinh },
                new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10) { Value = (object)sv.GioiTinh ?? DBNull.Value },
                new SqlParameter("@Lop", SqlDbType.VarChar, 50) { Value = sv.Lop },
                new SqlParameter("@SDT", SqlDbType.VarChar, 15) { Value = (object)sv.SDT ?? DBNull.Value },
                new SqlParameter("@DiemTB", SqlDbType.Decimal) { Precision = 4, Scale = 2,
                                                                 Value = (object)sv.DiemTB ?? DBNull.Value }
            };
        }
    }
}
