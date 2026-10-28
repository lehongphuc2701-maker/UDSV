using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>
    /// Truy cập bảng TaiKhoan (đăng nhập / đăng ký).
    /// Lưu ý: mật khẩu đang lưu dạng thô theo đúng script CSDL của nhóm;
    /// khi triển khai thật phải băm (hash) trước khi lưu.
    /// </summary>
    public class TaiKhoanDAL
    {
        // 1. Kiểm tra đăng nhập: trả về tài khoản nếu đúng, null nếu sai
        public TaiKhoanDTO DangNhap(string username, string password)
        {
            const string query = @"SELECT Username, Password, HoTen
                                   FROM TaiKhoan
                                   WHERE Username = @Username AND Password = @Password";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Username", SqlDbType.VarChar, 50) { Value = username },
                new SqlParameter("@Password", SqlDbType.VarChar, 100) { Value = password }
            };

            DataTable dt = DatabaseHelper.GetData(query, parameters);
            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];
            return new TaiKhoanDTO(
                r["Username"].ToString(),
                r["Password"].ToString(),
                r["HoTen"].ToString());
        }

        // 2. Tài khoản đã tồn tại chưa
        public bool KiemTraTaiKhoanTonTai(string username)
        {
            const string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Username = @Username";
            SqlParameter p = new SqlParameter("@Username", SqlDbType.VarChar, 50) { Value = username };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p)) > 0;
        }

        // 3. Thêm tài khoản mới
        public bool ThemTaiKhoan(TaiKhoanDTO tk)
        {
            const string query = @"INSERT INTO TaiKhoan (Username, Password, HoTen)
                                   VALUES (@Username, @Password, @HoTen)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Username", SqlDbType.VarChar, 50) { Value = tk.Username },
                new SqlParameter("@Password", SqlDbType.VarChar, 100) { Value = tk.Password },
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = tk.HoTen }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) == 1;
        }
    }
}
