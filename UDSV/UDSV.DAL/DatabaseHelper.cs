using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace UDSV.DAL
{
    /// <summary>
    /// Nơi duy nhất trong ứng dụng biết cách mở kết nối SQL Server.
    /// Toàn bộ câu lệnh đi qua đây đều dùng SqlParameter — không ghép chuỗi.
    /// </summary>
    public static class DatabaseHelper
    {
        // Chuỗi kết nối mặc định của nhóm (SQL Server Express trên máy từng thành viên).
        // Muốn đổi mà không sửa code: đặt biến môi trường UDSV_CONNECTION trước khi chạy.
        // Ví dụ: setx UDSV_CONNECTION "Server=localhost\SQLEXPRESS05;Database=DETAI;Integrated Security=True;TrustServerCertificate=True"
        private static readonly string ConnectionString =
            Environment.GetEnvironmentVariable("UDSV_CONNECTION")
            ?? @"Server=localhost\SQLEXPRESS05;Database=DETAI;Integrated Security=True;TrustServerCertificate=True;";

        /// <summary>Mở một kết nối mới (người gọi chịu trách nhiệm đóng — dùng trong using).</summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>SELECT → trả về DataTable để bind thẳng lên DataGridView.</summary>
        public static DataTable GetData(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi lấy dữ liệu: " + ex.Message, ex);
            }
            return dt;
        }

        /// <summary>INSERT / UPDATE / DELETE → trả về số dòng bị ảnh hưởng.</summary>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi thực thi dữ liệu: " + ex.Message, ex);
            }
        }

        /// <summary>SELECT đếm / kiểm tra tồn tại → trả về một giá trị đơn.</summary>
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi truy vấn dữ liệu: " + ex.Message, ex);
            }
        }
    }
}
