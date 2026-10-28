using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace UDSV.DAL
{
    /// <summary>
    /// Cấu hình logging dùng chung cho cả DAL và BLL (Chương 8).
    /// Ghi ra Console và ra file. WinForms không có cửa sổ console nên file là nơi xem được.
    ///
    /// Thư mục log được chọn theo thứ tự: %LOCALAPPDATA%\UDSV\Logs → cạnh file exe → %TEMP% → tắt ghi file.
    /// Phải có chuỗi dự phòng vì môi trường chạy có thể chặn ghi ra ngoài thư mục ứng dụng.
    ///
    /// Đổi mức log mà không sửa code: biến môi trường UDSV_LOG_LEVEL
    /// (Trace | Debug | Information | Warning | Error | Critical | None).
    /// </summary>
    public static class AppLog
    {
        private static readonly Lazy<ILoggerFactory> factory = new Lazy<ILoggerFactory>(TaoFactory);

        public static ILoggerFactory Factory => factory.Value;

        public static ILogger<T> For<T>() => Factory.CreateLogger<T>();

        /// <summary>Thư mục đang thực sự được ghi log (rỗng nếu không ghi được ở đâu cả).</summary>
        public static string ThuMucLog { get; private set; } = "";

        private static ILoggerFactory TaoFactory()
        {
            LogLevel muc = LogLevel.Debug;
            string? tuBienMoiTruong = Environment.GetEnvironmentVariable("UDSV_LOG_LEVEL");
            if (!string.IsNullOrWhiteSpace(tuBienMoiTruong) &&
                Enum.TryParse(tuBienMoiTruong, true, out LogLevel mucDat))
            {
                muc = mucDat;
            }

            ThuMucLog = ChonThuMucGhiDuoc();

            return LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(muc);
                builder.AddConsole();
                if (!string.IsNullOrEmpty(ThuMucLog))
                    builder.AddProvider(new FileLoggerProvider(ThuMucLog, muc));
            });
        }

        /// <summary>Thử lần lượt các vị trí; trả về thư mục ghi được đầu tiên, hoặc rỗng.</summary>
        private static string ChonThuMucGhiDuoc()
        {
            var ungVien = new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UDSV", "Logs"),
                Path.Combine(AppContext.BaseDirectory, "Logs"),
                Path.Combine(Path.GetTempPath(), "UDSV", "Logs")
            };

            foreach (string thuMuc in ungVien)
            {
                try
                {
                    Directory.CreateDirectory(thuMuc);

                    // CreateDirectory có thể thành công mà vẫn không ghi được — thử ghi thật một file.
                    string thu = Path.Combine(thuMuc, ".write-test");
                    File.WriteAllText(thu, "ok");
                    File.Delete(thu);

                    return thuMuc;
                }
                catch (UnauthorizedAccessException) { }
                catch (IOException) { }
                catch (NotSupportedException) { }
            }

            return "";
        }
    }

    /// <summary>Ghi log ra file, mỗi ngày một file UDSV-yyyyMMdd.log.</summary>
    internal sealed class FileLoggerProvider : ILoggerProvider
    {
        private readonly string thuMuc;
        private readonly LogLevel mucToiThieu;
        private readonly object khoa = new object();

        public FileLoggerProvider(string thuMuc, LogLevel mucToiThieu)
        {
            this.thuMuc = thuMuc;
            this.mucToiThieu = mucToiThieu;
        }

        public ILogger CreateLogger(string categoryName) =>
            new FileLogger(thuMuc, categoryName, mucToiThieu, khoa);

        public void Dispose() { }
    }

    internal sealed class FileLogger : ILogger
    {
        private readonly string thuMuc;
        private readonly string danhMuc;
        private readonly LogLevel mucToiThieu;
        private readonly object khoa;

        public FileLogger(string thuMuc, string danhMuc, LogLevel mucToiThieu, object khoa)
        {
            this.thuMuc = thuMuc;
            this.danhMuc = danhMuc;
            this.mucToiThieu = mucToiThieu;
            this.khoa = khoa;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= mucToiThieu && logLevel != LogLevel.None;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
                                Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            string dong = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{logLevel,-11}] {danhMuc}: {formatter(state, exception)}";
            if (exception != null)
                dong += Environment.NewLine + exception;

            string duongDan = Path.Combine(thuMuc, $"UDSV-{DateTime.Now:yyyyMMdd}.log");
            lock (khoa)
            {
                try { File.AppendAllText(duongDan, dong + Environment.NewLine); }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
                catch (NotSupportedException) { }
            }
        }
    }
}
