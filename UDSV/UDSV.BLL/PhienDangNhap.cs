namespace UDSV.BLL
{
    using UDSV.DTO;

    /// <summary>
    /// Phiên đăng nhập hiện tại — một chỗ duy nhất để các màn hình biết "ai đang dùng".
    /// Đặt ngay sau khi đăng nhập thành công; xoá khi đăng xuất.
    /// </summary>
    public static class PhienDangNhap
    {
        public static TaiKhoanDTO? HienTai { get; private set; }

        public static void Mo(TaiKhoanDTO tk) => HienTai = tk;
        public static void Dong() => HienTai = null;

        public static string Username => HienTai?.Username ?? "";
        public static string HoTen => HienTai?.HoTen ?? "";
        public static string VaiTro => HienTai?.VaiTro ?? "Admin";
        public static string? MaSV => HienTai?.MaSV;

        /// <summary>true nếu là tài khoản sinh viên.</summary>
        public static bool LaSinhVien => HienTai?.LaSinhVien ?? false;
    }
}
