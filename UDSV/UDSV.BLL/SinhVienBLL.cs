using System;
using System.Data;
using Microsoft.Extensions.Logging;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Tầng nghiệp vụ của sinh viên: kiểm tra dữ liệu trước khi cho DAL chạm vào CSDL.
    /// Sinh viên thuộc đúng MỘT khóa; khóa quyết định chương trình nào áp cho người này.
    /// </summary>
    public class SinhVienBLL
    {
        private readonly ISinhVienDAL svDAL;
        private readonly IKhoaDAL khoaDAL;
        private static readonly ILogger log = AppLog.For<SinhVienBLL>();

        /// <summary>Dùng thật trong ứng dụng.</summary>
        public SinhVienBLL() : this(new SinhVienDAL(), new KhoaDAL()) { }

        /// <summary>Dùng cho Unit Test: truyền DAL giả, không cần CSDL.</summary>
        public SinhVienBLL(ISinhVienDAL sinhVienDal, IKhoaDAL khoaDal)
        {
            svDAL = sinhVienDal ?? throw new ArgumentNullException(nameof(sinhVienDal));
            khoaDAL = khoaDal ?? throw new ArgumentNullException(nameof(khoaDal));
        }

        public DataTable LayDanhSachSinhVien() => svDAL.LayDanhSachSinhVien();

        /// <summary>Một sinh viên theo mã (kèm tên khóa) — dùng cho màn hồ sơ của sinh viên.</summary>
        public DataTable LayTheoMa(string maSV) => svDAL.LaySinhVienTheoMa(maSV);

        public DataTable TimKiemSinhVien(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return svDAL.LayDanhSachSinhVien();

            return svDAL.TimKiemSinhVien(keyword.Trim());
        }

        public bool ThemSinhVien(SinhVienDTO sv, out string error)
        {
            if (!KiemTraDuLieu(sv, out error))
            {
                log.LogWarning("Từ chối thêm sinh viên {MaSV}: {Loi}", sv?.MaSV, error);
                return false;
            }
            if (!khoaDAL.KiemTraTonTai(sv.MaKhoa))
            {
                error = $"Khóa '{sv.MaKhoa}' không tồn tại.";
                log.LogWarning("Từ chối thêm sinh viên {MaSV}: khóa lạ", sv.MaSV);
                return false;
            }
            if (svDAL.KiemTraMaTonTai(sv.MaSV))
            {
                error = $"Mã sinh viên '{sv.MaSV}' đã tồn tại!";
                log.LogWarning("Từ chối thêm sinh viên {MaSV}: trùng mã", sv.MaSV);
                return false;
            }

            bool ok = svDAL.ThemSinhVien(sv);
            if (ok) log.LogInformation("Thêm sinh viên {MaSV} - {HoTen} ({Khoa})", sv.MaSV, sv.HoTen, sv.MaKhoa);
            return ok;
        }

        public bool SuaSinhVien(SinhVienDTO sv, out string error)
        {
            if (!KiemTraDuLieu(sv, out error))
            {
                log.LogWarning("Từ chối sửa sinh viên {MaSV}: {Loi}", sv?.MaSV, error);
                return false;
            }
            if (!khoaDAL.KiemTraTonTai(sv.MaKhoa))
            {
                error = $"Khóa '{sv.MaKhoa}' không tồn tại.";
                return false;
            }

            bool ok = svDAL.SuaSinhVien(sv);
            if (ok) log.LogInformation("Cập nhật sinh viên {MaSV}", sv.MaSV);
            return ok;
        }

        public bool XoaSinhVien(string maSV, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maSV))
            {
                error = "Hãy chọn sinh viên cần xoá!";
                return false;
            }

            bool ok = svDAL.XoaSinhVien(maSV.Trim());
            if (ok) log.LogInformation("Xoá sinh viên {MaSV}", maSV);
            return ok;
        }

        /// <summary>Chỉ kiểm tra quy tắc, không chạm CSDL — dùng cho Unit Test.</summary>
        public static bool KiemTraDuLieu(SinhVienDTO sv, out string error)
        {
            error = "";

            if (sv == null)
            {
                error = "Dữ liệu sinh viên rỗng.";
                return false;
            }

            sv.MaSV = (sv.MaSV ?? "").Trim();
            sv.HoTen = (sv.HoTen ?? "").Trim();
            sv.MaKhoa = (sv.MaKhoa ?? "").Trim();
            sv.SDT = (sv.SDT ?? "").Trim();
            sv.GioiTinh = (sv.GioiTinh ?? "").Trim();

            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                error = "Mã sinh viên không được để trống!";
                return false;
            }
            if (!sv.MaSV.StartsWith("SV", StringComparison.OrdinalIgnoreCase))
            {
                error = "Mã sinh viên phải bắt đầu bằng SV (ví dụ: SV001).";
                return false;
            }
            if (sv.MaSV.Length > 20)
            {
                error = "Mã sinh viên không được dài quá 20 ký tự.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(sv.HoTen))
            {
                error = "Họ tên sinh viên không được để trống!";
                return false;
            }
            if (sv.HoTen.Length > 100)
            {
                error = "Họ tên không được dài quá 100 ký tự.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(sv.MaKhoa))
            {
                error = "Sinh viên phải thuộc một khóa.";
                return false;
            }
            if (sv.NgaySinh.Date > DateTime.Today)
            {
                error = "Ngày sinh không được ở tương lai.";
                return false;
            }
            if (sv.NgaySinh.Year < 1900)
            {
                error = "Ngày sinh không hợp lệ.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(sv.GioiTinh))
            {
                error = "Hãy chọn giới tính.";
                return false;
            }
            if (sv.SDT.Length > 15)
            {
                error = "Số điện thoại không được dài quá 15 ký tự.";
                return false;
            }
            foreach (char c in sv.SDT)
            {
                if (!char.IsDigit(c))
                {
                    error = "Số điện thoại chỉ được chứa chữ số.";
                    return false;
                }
            }

            return true;
        }
    }
}
