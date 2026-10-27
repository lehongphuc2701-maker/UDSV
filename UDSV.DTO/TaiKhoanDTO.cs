namespace UDSV.DTO
{
    public class TaiKhoanDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string HoTen { get; set; }

        public TaiKhoanDTO() { }

        public TaiKhoanDTO(string username, string password, string hoTen)
        {
            Username = username;
            Password = password;
            HoTen = hoTen;
        }
    }
}