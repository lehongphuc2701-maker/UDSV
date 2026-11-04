namespace UDSV.DTO
{
    public class TaiKhoanDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string HoTen { get; set; }

        /// <summary>"Admin" hoặc "SinhVien" — dùng để chọn màn hình sau khi đăng nhập.</summary>
        public string VaiTro { get; set; } = "Admin";

        /// <summary>Mã sinh viên — chỉ có khi VaiTro = "SinhVien"; null với tài khoản quản trị.</summary>
        public string? MaSV { get; set; }

        /// <summary>true nếu đây là tài khoản sinh viên.</summary>
        public bool LaSinhVien => string.Equals(VaiTro, "SinhVien", StringComparison.OrdinalIgnoreCase);

        public TaiKhoanDTO() { }

        public TaiKhoanDTO(string username, string password, string hoTen,
                           string vaiTro = "Admin", string? maSV = null)
        {
            Username = username;
            Password = password;
            HoTen = hoTen;
            VaiTro = vaiTro;
            MaSV = maSV;
        }
    }
}
