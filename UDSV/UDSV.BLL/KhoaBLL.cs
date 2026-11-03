using System;
using System.Data;
using Microsoft.Extensions.Logging;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Nghiệp vụ khóa (niên khóa). Khóa là "quyền" của sinh viên: nó mang số tín chỉ cố định
    /// mà sinh viên của khóa đó phải đạt, nên không được xoá khi đã có sinh viên.
    /// </summary>
    public class KhoaBLL
    {
        private readonly IKhoaDAL khoaDAL;
        private static readonly ILogger log = AppLog.For<KhoaBLL>();

        public KhoaBLL() : this(new KhoaDAL()) { }

        public KhoaBLL(IKhoaDAL dal) => khoaDAL = dal ?? throw new ArgumentNullException(nameof(dal));

        public DataTable LayDanhSach() => khoaDAL.LayDanhSach();

        public KhoaDTO? LayTheoMa(string maKhoa) => khoaDAL.LayTheoMa(maKhoa);

        public int DemSinhVien(string maKhoa) => khoaDAL.DemSinhVien(maKhoa);

        public bool Them(KhoaDTO khoa, out string error)
        {
            if (!KiemTraDuLieu(khoa, out error))
            {
                log.LogWarning("Từ chối thêm khóa {MaKhoa}: {Loi}", khoa?.MaKhoa, error);
                return false;
            }
            if (khoaDAL.KiemTraTonTai(khoa.MaKhoa))
            {
                error = $"Mã khóa '{khoa.MaKhoa}' đã tồn tại!";
                return false;
            }

            bool ok = khoaDAL.Them(khoa);
            if (ok) log.LogInformation("Thêm khóa {MaKhoa} ({BatBuoc}+{TuChon} TC)",
                                       khoa.MaKhoa, khoa.TongTCBatBuoc, khoa.TongTCTuChon);
            return ok;
        }

        public bool Sua(KhoaDTO khoa, out string error)
        {
            if (!KiemTraDuLieu(khoa, out error))
            {
                log.LogWarning("Từ chối sửa khóa {MaKhoa}: {Loi}", khoa?.MaKhoa, error);
                return false;
            }

            bool ok = khoaDAL.Sua(khoa);
            if (ok) log.LogInformation("Cập nhật khóa {MaKhoa}", khoa.MaKhoa);
            return ok;
        }

        public bool Xoa(string maKhoa, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maKhoa))
            {
                error = "Hãy chọn khóa cần xoá!";
                return false;
            }

            int soSv = khoaDAL.DemSinhVien(maKhoa);
            if (soSv > 0)
            {
                error = $"Không thể xoá: khóa '{maKhoa}' đang có {soSv} sinh viên.";
                log.LogWarning("Từ chối xoá khóa {MaKhoa}: còn {SoSv} sinh viên", maKhoa, soSv);
                return false;
            }

            bool xoaOk = khoaDAL.Xoa(maKhoa);
            if (xoaOk) log.LogInformation("Xoá khóa {MaKhoa}", maKhoa);
            return xoaOk;
        }

        /// <summary>Quy tắc thuần, không chạm CSDL — dùng cho Unit Test.</summary>
        public static bool KiemTraDuLieu(KhoaDTO khoa, out string error)
        {
            error = "";
            if (khoa == null)
            {
                error = "Dữ liệu khóa rỗng.";
                return false;
            }

            khoa.MaKhoa = (khoa.MaKhoa ?? "").Trim();
            khoa.TenKhoa = (khoa.TenKhoa ?? "").Trim();

            if (string.IsNullOrWhiteSpace(khoa.MaKhoa))
            {
                error = "Mã khóa không được để trống!";
                return false;
            }
            if (khoa.MaKhoa.Length > 10)
            {
                error = "Mã khóa không được dài quá 10 ký tự.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(khoa.TenKhoa))
            {
                error = "Tên khóa không được để trống!";
                return false;
            }
            if (khoa.TenKhoa.Length > 100)
            {
                error = "Tên khóa không được dài quá 100 ký tự.";
                return false;
            }
            if (khoa.NamBatDau < 2000 || khoa.NamBatDau > 2100)
            {
                error = "Năm bắt đầu không hợp lệ.";
                return false;
            }
            if (khoa.NamKetThuc <= khoa.NamBatDau)
            {
                error = "Năm kết thúc phải sau năm bắt đầu.";
                return false;
            }
            if (khoa.TongTCBatBuoc <= 0)
            {
                error = "Tổng tín chỉ bắt buộc phải lớn hơn 0.";
                return false;
            }
            if (khoa.TongTCTuChon < 0)
            {
                error = "Tổng tín chỉ tự chọn không được âm.";
                return false;
            }

            return true;
        }
    }
}
