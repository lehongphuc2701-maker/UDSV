using System;
using Microsoft.Extensions.Logging;
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
        private static readonly ILogger log = AppLog.For<TaiKhoanBLL>();

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
                log.LogWarning("Đăng nhập bị chặn: tên đăng nhập rỗng");
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                error = "Mật khẩu không được để trống!";
                log.LogWarning("Đăng nhập bị chặn: mật khẩu rỗng (user {Username})", username);
                return false;
            }

            taiKhoan = tkDAL.DangNhap(username, password);
            if (taiKhoan == null)
            {
                error = "Sai tên đăng nhập hoặc mật khẩu!";
                log.LogWarning("Đăng nhập thất bại cho tài khoản {Username}", username);
                return false;
            }

            log.LogInformation("Đăng nhập thành công: {Username} ({HoTen})", taiKhoan.Username, taiKhoan.HoTen);
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
                log.LogWarning("Đăng ký bị chặn cho {Username}: mật khẩu quá ngắn", username);
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
                log.LogWarning("Đăng ký bị chặn cho {Username}: nhập lại mật khẩu không khớp", username);
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
                log.LogWarning("Đăng ký bị chặn: tài khoản {Username} đã tồn tại", username);
                return false;
            }

            TaiKhoanDTO tk = new TaiKhoanDTO(username, password, hoTen);
            bool ok = tkDAL.ThemTaiKhoan(tk);
            if (ok) log.LogInformation("Đăng ký tài khoản mới: {Username} ({HoTen})", username, hoTen);
            return ok;
        }
    }
}
