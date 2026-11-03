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

            KhoaDTO? hienTai = khoaDAL.LayTheoMa(khoa.MaKhoa);
            if (hienTai == null)
            {
                error = $"Khóa '{khoa.MaKhoa}' không tồn tại.";
                return false;
            }

            // Chương trình đã chốt thì số tín chỉ là bất biến — sửa được tên/năm, không sửa được TC.
            if (hienTai.DaKhoa &&
                (khoa.TongTCBatBuoc != hienTai.TongTCBatBuoc || khoa.TongTCTuChon != hienTai.TongTCTuChon))
            {
                error = "Không thể sửa vì số tín chỉ sau khi chỉnh sửa khác với số tín chỉ đã khoá " +
                        $"({hienTai.TongTCBatBuoc} bắt buộc + {hienTai.TongTCTuChon} tự chọn).\n" +
                        "Muốn đổi số tín chỉ thì phải tạo khóa mới.";
                log.LogWarning("Từ chối sửa TC khóa {MaKhoa}: đã khoá {BB}/{TC}, sửa thành {NewBB}/{NewTC}",
                               khoa.MaKhoa, hienTai.TongTCBatBuoc, hienTai.TongTCTuChon,
                               khoa.TongTCBatBuoc, khoa.TongTCTuChon);
                return false;
            }

            bool ok = khoaDAL.Sua(khoa);
            if (ok) log.LogInformation("Cập nhật khóa {MaKhoa}", khoa.MaKhoa);
            return ok;
        }

        /// <summary>
        /// Tạo khóa kế tiếp: mã = "K" + (số lớn nhất hiện có + 1), tên và năm suy ra từ số đó.
        /// Mã khóa là khoá chính nên người dùng không gõ tay — chỉ tăng, không sửa.
        /// </summary>
        public bool TaoKhoaKeTiep(out string maKhoaMoi, out string error)
        {
            maKhoaMoi = "";
            error = "";

            int lonNhat = 0;
            foreach (System.Data.DataRow r in khoaDAL.LayDanhSach().Rows)
            {
                string maHienCo = r["MaKhoa"].ToString() ?? "";
                if (maHienCo.Length > 1 && (maHienCo[0] == 'K' || maHienCo[0] == 'k')
                    && int.TryParse(maHienCo.Substring(1), out int so) && so > lonNhat)
                {
                    lonNhat = so;
                }
            }

            int tiepTheo = lonNhat + 1;
            string maMoi = "K" + tiepTheo;
            KhoaDTO moi = new KhoaDTO(maMoi,
                                      "Công nghệ thông tin K" + tiepTheo,
                                      2000 + tiepTheo,
                                      2000 + tiepTheo + 4,
                                      0, 0);

            if (!khoaDAL.Them(moi))
            {
                error = "Không tạo được khóa mới.";
                return false;
            }

            maKhoaMoi = maMoi;
            log.LogInformation("Thêm khóa {MaKhoa} (tự sinh, khóa trước lớn nhất là K{Cu})", maMoi, lonNhat);
            return true;
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

            KhoaDTO? khoa = khoaDAL.LayTheoMa(maKhoa);
            if (khoa != null && khoa.DaKhoa)
            {
                error = $"Không thể xoá: chương trình của khóa '{maKhoa}' đã được khoá " +
                        $"ngày {khoa.NgayKhoa:dd/MM/yyyy} bởi {khoa.NguoiKhoa}.";
                log.LogWarning("Từ chối xoá khóa {MaKhoa}: chương trình đã khoá", maKhoa);
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
