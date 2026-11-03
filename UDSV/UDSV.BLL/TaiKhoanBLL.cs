using System;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    public class TaiKhoanBLL
    {
        // Khởi tạo đối tượng DAL để Form gọi thông qua BLL
        private readonly TaiKhoanDAL tkDAL = new TaiKhoanDAL();

        // Xử lý Đăng nhập
        public bool KiemTraDangNhap(string username, string password)
        {
            return tkDAL.KiemTraDangNhap(username, password);
        }

        // Xử lý Đăng ký
        public bool DangKy(TaiKhoanDTO tk, out string error)
        {
            // Kiểm tra nghiệp vụ: Mật khẩu phải đủ an toàn
            if (tk.Password.Length < 6)
            {
                error = "Mật khẩu quá ngắn! Phải từ 6 ký tự trở lên.";
                return false;
            }

            // Gọi xuống DAL để tiến hành lưu vào SQL
            return tkDAL.DangKy(tk, out error);
        }
    }
}