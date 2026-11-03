using System;
using System.Data.SqlClient;
using UDSV.DTO; // Gọi thư mục DTO để dùng TaiKhoanDTO

namespace UDSV.DAL
{
    public class TaiKhoanDAL
    {
        // Hàm 1: Kiểm tra đăng nhập
        public bool KiemTraDangNhap(string username, string password)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Username = @User AND Password = @Pass";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Parameters.AddWithValue("@Pass", password);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Hàm 2: Đăng ký tài khoản mới
        public bool DangKy(TaiKhoanDTO tk, out string error)
        {
            error = string.Empty;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Bước A: Kiểm tra xem tên đăng nhập đã có ai dùng chưa
                    string checkQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE Username = @User";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@User", tk.Username);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            error = "Tên tài khoản này đã tồn tại, vui lòng chọn tên khác!";
                            return false;
                        }
                    }

                    // Bước B: Nếu chưa ai dùng thì Thêm mới vào SQL
                    string insertQuery = "INSERT INTO TaiKhoan (Username, Password, HoTen) VALUES (@User, @Pass, @Name)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@User", tk.Username);
                        cmd.Parameters.AddWithValue("@Pass", tk.Password);
                        cmd.Parameters.AddWithValue("@Name", tk.HoTen);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                error = "Lỗi kết nối CSDL: " + ex.Message;
                return false;
            }
        }
    }
}