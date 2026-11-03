using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Logging;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Nghiệp vụ "chương trình khung" của từng khóa.
    ///
    /// Bốn luật (chốt 2026-11-03):
    ///  R1. Chưa khoá  → sửa tự do, KHÔNG có trần tín chỉ.
    ///  R2. Bấm KHOÁ   → chốt tổng tín chỉ vào Khoa, ghi người + giờ. Không mở khoá lại.
    ///  R3. Đã khoá    → vẫn LƯU được, nhưng tổng bắt buộc và tổng tự chọn phải giữ nguyên.
    ///  R4. Đã khoá    → không được bỏ môn mà sinh viên của khóa đã có điểm.
    /// </summary>
    public class ChuongTrinhBLL
    {
        private readonly IChuongTrinhDAL ctDAL;
        private readonly IKhoaDAL khoaDAL;
        private readonly IMonHocDAL mhDAL;
        private static readonly ILogger log = AppLog.For<ChuongTrinhBLL>();

        public ChuongTrinhBLL() : this(new ChuongTrinhDAL(), new KhoaDAL(), new MonHocDAL()) { }

        public ChuongTrinhBLL(IChuongTrinhDAL chuongTrinhDal, IKhoaDAL khoaDal, IMonHocDAL monHocDal)
        {
            ctDAL = chuongTrinhDal ?? throw new ArgumentNullException(nameof(chuongTrinhDal));
            khoaDAL = khoaDal ?? throw new ArgumentNullException(nameof(khoaDal));
            mhDAL = monHocDal ?? throw new ArgumentNullException(nameof(monHocDal));
        }

        public DataTable LayTheoKhoa(string maKhoa) => ctDAL.LayTheoKhoa(maKhoa);

        /// <summary>Các học kỳ có môn trong chương trình của khóa.</summary>
        public DataTable LayHocKy(string maKhoa) => ctDAL.LayHocKy(maKhoa);

        /// <summary>Danh sách môn của một khóa trong một học kỳ.</summary>
        public DataTable LayMonTheoKy(string maKhoa, int hocKy) => ctDAL.LayMonTheoKy(maKhoa, hocKy);

        public int TongTinChi(string maKhoa, bool batBuoc) => ctDAL.TongTinChi(maKhoa, batBuoc);

        /// <summary>Chương trình của khóa đã bị chốt chưa (đọc NgayKhoa, không dựa vào số sinh viên).</summary>
        public bool DaKhoa(string maKhoa) => khoaDAL.LayTheoMa(maKhoa)?.DaKhoa ?? false;

        /// <summary>Tổng tín chỉ theo loại, tính trên danh sách dòng đang sửa (chưa ghi xuống CSDL).</summary>
        public int TongTuDanhSach(IList<ChuongTrinhDTO> danhSach, bool batBuoc, DataTable danhMucMonHoc)
        {
            int tong = 0;
            foreach (ChuongTrinhDTO dong in danhSach)
            {
                if (dong.BatBuoc != batBuoc) continue;
                foreach (DataRow r in danhMucMonHoc.Rows)
                {
                    if (string.Equals(r["MaMH"]?.ToString(), dong.MaMH, StringComparison.OrdinalIgnoreCase))
                    {
                        tong += Convert.ToInt32(r["SoTinChi"]);
                        break;
                    }
                }
            }
            return tong;
        }

        /// <summary>
        /// Ghi toàn bộ lưới chương trình. Chưa khoá thì ghi vô điều kiện; đã khoá thì phải
        /// giữ nguyên tổng tín chỉ (R3) và không được bỏ môn đã có điểm (R4).
        /// </summary>
        public bool LuuChuongTrinh(string maKhoa, IList<ChuongTrinhDTO> danhSach, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maKhoa))
            {
                error = "Hãy chọn một khóa.";
                return false;
            }

            KhoaDTO? khoa = khoaDAL.LayTheoMa(maKhoa);
            if (khoa == null)
            {
                error = $"Khóa '{maKhoa}' không tồn tại.";
                return false;
            }

            if (!KiemTraDong(danhSach, out error))
                return false;

            if (khoa.DaKhoa)
            {
                DataTable monHoc = mhDAL.LayDanhSachMonHoc();
                int tongBB = TongTuDanhSach(danhSach, true, monHoc);
                int tongTC = TongTuDanhSach(danhSach, false, monHoc);

                if (tongBB != khoa.TongTCBatBuoc || tongTC != khoa.TongTCTuChon)
                {
                    error = $"Chương trình đã khoá ngày {khoa.NgayKhoa:dd/MM/yyyy}: tổng tín chỉ phải giữ nguyên " +
                            $"{khoa.TongTCBatBuoc} bắt buộc + {khoa.TongTCTuChon} tự chọn " +
                            $"(đang sửa thành {tongBB} + {tongTC}).\n" +
                            "Muốn đổi số tín chỉ thì phải tạo khóa mới.";
                    log.LogWarning("Từ chối lưu chương trình {MaKhoa}: tổng đổi {BB}/{TC}", maKhoa, tongBB, tongTC);
                    return false;
                }

                // R4 — không bỏ môn mà sinh viên của khóa đã có điểm
                List<string> monCu = new List<string>();
                foreach (DataRow r in ctDAL.LayTheoKhoa(maKhoa).Rows)
                    monCu.Add(r["MaMH"].ToString() ?? "");

                HashSet<string> monMoi = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (ChuongTrinhDTO d in danhSach) monMoi.Add(d.MaMH ?? "");

                foreach (string maMH in monCu)
                {
                    if (monMoi.Contains(maMH)) continue;

                    int soSV = ctDAL.DemSinhVienDaCoDiem(maKhoa, maMH);
                    if (soSV > 0)
                    {
                        error = $"Không bỏ được môn '{maMH}': đã có {soSV} sinh viên của khóa {maKhoa} " +
                                "có điểm môn này.\nBỏ môn sẽ làm mất tín chỉ các em đã tích luỹ.";
                        log.LogWarning("Từ chối bỏ môn {MaMH} khỏi {MaKhoa}: {SoSV} SV đã có điểm", maMH, maKhoa, soSV);
                        return false;
                    }
                }
            }

            bool ok = ctDAL.LuuCaChuongTrinh(maKhoa, danhSach);
            if (ok)
                log.LogInformation("Lưu chương trình {MaKhoa}: {SoDong} dòng (đã khoá: {DaKhoa})",
                                   maKhoa, danhSach.Count, khoa.DaKhoa);
            return ok;
        }

        /// <summary>R2 — chốt chương trình: ghi tổng tín chỉ hiện tại vào khóa, kèm người bấm.</summary>
        public bool KhoaChuongTrinh(string maKhoa, string nguoiKhoa, out string error)
        {
            error = "";
            KhoaDTO? khoa = khoaDAL.LayTheoMa(maKhoa);
            if (khoa == null)
            {
                error = $"Khóa '{maKhoa}' không tồn tại.";
                return false;
            }
            if (khoa.DaKhoa)
            {
                error = $"Chương trình của khóa {maKhoa} đã được khoá ngày {khoa.NgayKhoa:dd/MM/yyyy} " +
                        $"bởi {khoa.NguoiKhoa}. Không mở khoá lại.";
                return false;
            }

            int batBuoc = ctDAL.TongTinChi(maKhoa, true);
            int tuChon = ctDAL.TongTinChi(maKhoa, false);

            if (batBuoc + tuChon == 0)
            {
                error = "Chương trình đang trống — thêm môn trước khi khoá.";
                return false;
            }

            bool ok = khoaDAL.KhoaChuongTrinh(maKhoa, nguoiKhoa, batBuoc, tuChon);
            if (ok)
                log.LogInformation("KHOÁ chương trình {MaKhoa}: {BatBuoc} TC bắt buộc + {TuChon} TC tự chọn (bởi {Nguoi})",
                                   maKhoa, batBuoc, tuChon, nguoiKhoa);
            return ok;
        }

        /// <summary>R5 — tạo khóa mới bằng cách copy toàn bộ chương trình của khóa cũ (khóa mới chưa bị khoá).</summary>
        public bool NhanBan(string tuKhoa, string denKhoa, string tenKhoa, int namBatDau, int namKetThuc,
                            out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(tuKhoa) || string.IsNullOrWhiteSpace(denKhoa))
            {
                error = "Thiếu khóa nguồn hoặc khóa đích.";
                return false;
            }
            if (khoaDAL.KiemTraTonTai(denKhoa))
            {
                error = $"Mã khóa '{denKhoa}' đã tồn tại.";
                return false;
            }

            DataTable nguon = ctDAL.LayTheoKhoa(tuKhoa);
            if (nguon.Rows.Count == 0)
            {
                error = $"Khóa '{tuKhoa}' chưa có chương trình để nhân bản.";
                return false;
            }

            int batBuoc = ctDAL.TongTinChi(tuKhoa, true);
            int tuChon = ctDAL.TongTinChi(tuKhoa, false);

            KhoaDTO moi = new KhoaDTO(denKhoa, tenKhoa, namBatDau, namKetThuc, batBuoc, tuChon);
            if (!KhoaBLL.KiemTraDuLieu(moi, out error))
                return false;

            if (!khoaDAL.Them(moi))
            {
                error = "Không tạo được khóa mới.";
                return false;
            }

            List<ChuongTrinhDTO> dong = new List<ChuongTrinhDTO>();
            foreach (DataRow r in nguon.Rows)
            {
                dong.Add(new ChuongTrinhDTO(
                    denKhoa,
                    r["MaMH"].ToString() ?? "",
                    Convert.ToBoolean(r["BatBuoc"]),
                    Convert.ToInt32(r["HocKy"])));
            }

            if (!ctDAL.LuuCaChuongTrinh(denKhoa, dong))
            {
                error = "Đã tạo khóa nhưng copy chương trình thất bại.";
                return false;
            }

            log.LogInformation("Nhân bản chương trình {Tu} → {Den}: {SoDong} dòng", tuKhoa, denKhoa, dong.Count);
            return true;
        }

        /// <summary>Kiểm tra từng dòng của lưới trước khi ghi.</summary>
        public static bool KiemTraDong(IList<ChuongTrinhDTO> danhSach, out string error)
        {
            error = "";
            if (danhSach == null || danhSach.Count == 0)
            {
                error = "Chương trình phải có ít nhất một môn.";
                return false;
            }

            HashSet<string> daCo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < danhSach.Count; i++)
            {
                ChuongTrinhDTO d = danhSach[i];
                string stt = $"Dòng {i + 1}";

                if (string.IsNullOrWhiteSpace(d.MaMH))
                {
                    error = $"{stt}: chưa chọn môn học.";
                    return false;
                }
                if (d.HocKy < 1 || d.HocKy > 10)
                {
                    error = $"{stt} ({d.MaMH}): học kỳ phải từ 1 đến 10.";
                    return false;
                }
                if (!daCo.Add(d.MaMH.Trim()))
                {
                    error = $"{stt}: môn '{d.MaMH}' bị trùng trong chương trình.";
                    return false;
                }

                // Lịch học hằng tuần: hoặc để trống cả cụm, hoặc phải đủ và hợp lệ.
                if (d.Thu.HasValue)
                {
                    if (d.Thu < 2 || d.Thu > 8)
                    {
                        error = $"{stt} ({d.MaMH}): thứ phải từ 2 (Thứ 2) đến 8 (Chủ nhật).";
                        return false;
                    }
                    if (!d.TietBD.HasValue || !d.TietKT.HasValue)
                    {
                        error = $"{stt} ({d.MaMH}): đã chọn thứ thì phải nhập tiết bắt đầu và kết thúc.";
                        return false;
                    }
                    if (d.TietBD < 1 || d.TietKT > 12)
                    {
                        error = $"{stt} ({d.MaMH}): tiết phải nằm trong 1..12.";
                        return false;
                    }
                    if (d.TietKT < d.TietBD)
                    {
                        error = $"{stt} ({d.MaMH}): tiết kết thúc phải >= tiết bắt đầu.";
                        return false;
                    }
                }
                else if (d.TietBD.HasValue || d.TietKT.HasValue)
                {
                    error = $"{stt} ({d.MaMH}): đã nhập tiết thì phải chọn thứ.";
                    return false;
                }
            }

            return true;
        }
    }
}
