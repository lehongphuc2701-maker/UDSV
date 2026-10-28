using System;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Nghiệp vụ tài khoản: đăng nhập và đăng ký.
    /// UI không tự so sánh mật khẩu với CSDL — mọi thứ đi qua đây.
    /// </summary>
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL tkDAL = new TaiKhoanDAL();

        // 1. Đăng nhập
        public bool DangNhap(string username, string password, out TaiKhoanDTO taiKhoan, out string error)
        {
            error = "";
            taiKhoan = null;

            username = (username ?? "").Trim();
            password = password ?? "";

            if (string.IsNullOrWhiteSpace(username))
            {
                error = "Tên đăng nhập không được để trống!";
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                error = "Mật khẩu không được để trống!";
                return false;
            }

            taiKhoan = tkDAL.DangNhap(username, password);
            if (taiKhoan == null)
            {
                error = "Sai tên đăng nhập hoặc mật khẩu!";
                return false;
            }

            return true;
        }

        // 2. Đăng ký tài khoản mới
        public bool DangKy(string username, string password, string nhapLaiMatKhau, string hoTen, out string error)
        {
            error = "";

            username = (username ?? "").Trim();
            hoTen = (hoTen ?? "").Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                error = "Tên tài khoản không được để trống!";
                return false;
            }
            if (username.Length > 50)
            {
                error = "Tên tài khoản không được dài quá 50 ký tự.";
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                error = "Mật khẩu không được để trống!";
                return false;
            }
            if (password.Length < 6)
            {
                error = "Mật khẩu phải có ít nhất 6 ký tự.";
                return false;
            }
            if (password.Length > 100)
            {
                error = "Mật khẩu không được dài quá 100 ký tự.";
                return false;
            }
            if (password != nhapLaiMatKhau)
            {
                error = "Hai lần nhập mật khẩu không khớp!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                error = "Họ tên không được để trống!";
                return false;
            }
            if (hoTen.Length > 100)
            {
                error = "Họ tên không được dài quá 100 ký tự.";
                return false;
            }
            if (tkDAL.KiemTraTaiKhoanTonTai(username))
            {
                error = $"Tên tài khoản '{username}' đã được sử dụng!";
                return false;
            }

            TaiKhoanDTO tk = new TaiKhoanDTO(username, password, hoTen);
            return tkDAL.ThemTaiKhoan(tk);
        }
    }
}
