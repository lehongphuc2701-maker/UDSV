namespace UDSV
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Ứng dụng bắt đầu ở màn hình đăng nhập; sau khi đăng nhập mới vào trang chủ.
            Application.Run(new FrmDangNhap());
        }
    }
}

