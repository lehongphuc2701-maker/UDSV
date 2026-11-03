namespace UDSV.DTO
{
    public class TaiKhoanDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string HoTen { get; set; }

        /// <summary>"Admin" hoặc "SinhVien" — dùng để chọn màn hình sau khi đăng nhập.</summary>
        public string VaiTro { get; set; } = "Admin";

        public TaiKhoanDTO() { }

        public TaiKhoanDTO(string username, string password, string hoTen, string vaiTro = "Admin")
        {
            Username = username;
            Password = password;
            HoTen = hoTen;
            VaiTro = vaiTro;
        }
    }
}
