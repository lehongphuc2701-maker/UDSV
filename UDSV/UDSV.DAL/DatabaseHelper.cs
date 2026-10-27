using System;
using System.Data;
using System.Data.SqlClient;

namespace UDSV.DAL
{
    public class DatabaseHelper
    {
        // CHÚ Ý: Đổi TEN_SERVER_CUA_BAN thành Server Name trong SQL của bạn
        private static string connectionString = @"Data Source=.\SQL2022;Initial Catalog=DETAI;Integrated Security=True;TrustServerCertificate=True";

        // Hàm 1: Mở kết nối
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Hàm 2: Dùng cho lệnh SELECT (Lấy dữ liệu lên DataGridView)
        public static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy dữ liệu: " + ex.Message);
            }
            return dt;
        }

        // Hàm 3: Dùng cho lệnh INSERT, UPDATE, DELETE (Thêm, Sửa, Xóa)
        public static bool ExecuteQuery(string query)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thực thi dữ liệu: " + ex.Message);
            }
        }
    }
}