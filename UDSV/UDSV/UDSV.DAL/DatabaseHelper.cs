using System;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace UDSV.DAL
{
    /// <summary>
    /// Nơi duy nhất trong ứng dụng biết cách mở kết nối SQL Server.
    /// Toàn bộ câu lệnh đi qua đây đều dùng SqlParameter — không ghép chuỗi.
    /// Mỗi lệnh được ghi log ở mức Debug (thời gian chạy) và Error/Critical khi hỏng.
    /// </summary>
    public static class DatabaseHelper
    {
        // DatabaseHelper là lớp static nên không dùng được AppLog.For<T>() — đặt tên category trực tiếp.
        private static readonly ILogger log = AppLog.Factory.CreateLogger("UDSV.DAL.DatabaseHelper");

        // Chuỗi kết nối mặc định của nhóm (SQL Server Express trên máy từng thành viên).
        // Muốn đổi mà không sửa code: đặt biến môi trường UDSV_CONNECTION trước khi chạy.
        private static readonly string ConnectionString =
    Environment.GetEnvironmentVariable("UDSV_CONNECTION")
    ?? @"Server=.\SQL2022;Database=DETAI;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>SELECT → trả về DataTable để bind thẳng lên DataGridView.</summary>
        public static DataTable GetData(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            Stopwatch dongHo = Stopwatch.StartNew();

            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    ThemThamSo(cmd, parameters);
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                log.LogDebug("SELECT trả về {SoDong} dòng trong {ThoiGian} ms", dt.Rows.Count, dongHo.ElapsedMilliseconds);
                return dt;
            }
            catch (SqlException ex)
            {
                GhiLoiSql("SELECT", query, ex);
                throw new Exception("Lỗi lấy dữ liệu: " + ex.Message, ex);
            }
        }

        /// <summary>INSERT / UPDATE / DELETE → trả về số dòng bị ảnh hưởng.</summary>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            Stopwatch dongHo = Stopwatch.StartNew();

            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    ThemThamSo(cmd, parameters);
                    conn.Open();
                    int soDong = cmd.ExecuteNonQuery();

                    log.LogDebug("Lệnh ghi ảnh hưởng {SoDong} dòng trong {ThoiGian} ms", soDong, dongHo.ElapsedMilliseconds);
                    return soDong;
                }
            }
            catch (SqlException ex)
            {
                GhiLoiSql("INSERT/UPDATE/DELETE", query, ex);
                throw new Exception("Lỗi thực thi dữ liệu: " + ex.Message, ex);
            }
        }

        /// <summary>SELECT đếm / kiểm tra tồn tại → trả về một giá trị đơn.</summary>
        public static object? ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            Stopwatch dongHo = Stopwatch.StartNew();

            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    ThemThamSo(cmd, parameters);
                    conn.Open();
                    object? ketQua = cmd.ExecuteScalar();

                    log.LogDebug("Truy vấn đơn trả về {KetQua} trong {ThoiGian} ms", ketQua, dongHo.ElapsedMilliseconds);
                    return ketQua;
                }
            }
            catch (SqlException ex)
            {
                GhiLoiSql("SELECT (scalar)", query, ex);
                throw new Exception("Lỗi truy vấn dữ liệu: " + ex.Message, ex);
            }
        }

        private static void ThemThamSo(SqlCommand cmd, SqlParameter[]? parameters)
        {
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
        }

        /// <summary>
        /// Lỗi kết nối (không mở được server) là sự cố nghiêm trọng → Critical.
        /// Lỗi câu lệnh (sai tên bảng, vi phạm ràng buộc) chỉ là Error.
        /// </summary>
        private static void GhiLoiSql(string loaiLenh, string query, SqlException ex)
        {
            // Không dựa vào ex.Number một mình: mất kết nối tới instance không tồn tại trả về -1.
            // Mức độ nghiêm trọng >= 20 của SQL Server cũng là lỗi cấp kết nối/hệ thống.
            bool loiKetNoi = ex.Class >= 20
                || ex.Number == -2 || ex.Number == 2 || ex.Number == 53
                || ex.Number == 18456 || ex.Number == 4060
                || ex.Message.Contains("network-related", StringComparison.OrdinalIgnoreCase)
                || ex.Message.Contains("Could not open a connection", StringComparison.OrdinalIgnoreCase);

            if (loiKetNoi)
                log.LogCritical(ex, "KHÔNG KẾT NỐI ĐƯỢC SQL SERVER (mã {SoLoi}). Kiểm tra lại instance và TCP/IP.", ex.Number);
            else
                log.LogError(ex, "Lỗi {LoaiLenh} (mã {SoLoi}). Câu lệnh: {CauLenh}", loaiLenh, ex.Number, RutGon(query));
        }

        private static string RutGon(string query)
        {
            string motDong = query.Replace("\r", " ").Replace("\n", " ").Trim();
            while (motDong.Contains("  ")) motDong = motDong.Replace("  ", " ");
            return motDong.Length <= 160 ? motDong : motDong.Substring(0, 160) + "…";
        }
    }
}
