using System;
using System.Collections.Generic;
using System.Data;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.Tests
{
    // =====================================================================
    //  Các DAL giả: giữ dữ liệu trong bộ nhớ nên test chạy được mà không cần
    //  SQL Server (máy chấm bài cũng chạy được). Chỉ mô phỏng đúng những
    //  phương thức mà BLL gọi tới.
    // =====================================================================

    internal sealed class SinhVienDALGia : ISinhVienDAL
    {
        private readonly List<SinhVienDTO> ds = new List<SinhVienDTO>();

        public void ThemSan(params SinhVienDTO[] items) => ds.AddRange(items);

        private static DataTable Bang(IEnumerable<SinhVienDTO> items)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSV", typeof(string));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("NgaySinh", typeof(DateTime));
            dt.Columns.Add("GioiTinh", typeof(string));
            dt.Columns.Add("MaKhoa", typeof(string));
            dt.Columns.Add("TenKhoa", typeof(string));
            dt.Columns.Add("SDT", typeof(string));
            foreach (SinhVienDTO sv in items)
                dt.Rows.Add(sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.MaKhoa, "Khóa " + sv.MaKhoa, sv.SDT);
            return dt;
        }

        public DataTable LayDanhSachSinhVien() => Bang(ds);

        public DataTable TimKiemSinhVien(string keyword) =>
            Bang(ds.FindAll(s => s.MaSV.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                              || s.HoTen.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

        public DataTable LaySinhVienTheoMa(string maSV) => Bang(ds.FindAll(s => s.MaSV == maSV));

        public bool KiemTraMaTonTai(string maSV) => ds.Exists(s => s.MaSV == maSV);

        public bool ThemSinhVien(SinhVienDTO sv) { ds.Add(sv); return true; }

        public bool SuaSinhVien(SinhVienDTO sv)
        {
            int i = ds.FindIndex(s => s.MaSV == sv.MaSV);
            if (i < 0) return false;
            ds[i] = sv;
            return true;
        }

        public bool XoaSinhVien(string maSV) => ds.RemoveAll(s => s.MaSV == maSV) == 1;
    }

    internal sealed class KhoaDALGia : IKhoaDAL
    {
        private readonly List<KhoaDTO> ds = new List<KhoaDTO>();
        private readonly Dictionary<string, int> soSinhVien = new Dictionary<string, int>();

        public KhoaDALGia(params KhoaDTO[] khoas)
        {
            foreach (KhoaDTO k in khoas)
            {
                ds.Add(k);
                soSinhVien[k.MaKhoa] = 0;
            }
        }

        /// <summary>Số sinh viên > 0 nghĩa là chương trình của khóa đã bị khoá.</summary>
        public void DatSoSinhVien(string maKhoa, int soLuong) => soSinhVien[maKhoa] = soLuong;

        public DataTable LayDanhSach()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaKhoa", typeof(string));
            foreach (KhoaDTO k in ds) dt.Rows.Add(k.MaKhoa);
            return dt;
        }

        public KhoaDTO? LayTheoMa(string maKhoa) => ds.Find(k => k.MaKhoa == maKhoa);

        public bool KiemTraTonTai(string maKhoa) => ds.Exists(k => k.MaKhoa == maKhoa);

        public int DemSinhVien(string maKhoa) =>
            soSinhVien.TryGetValue(maKhoa ?? "", out int n) ? n : 0;

        public bool Them(KhoaDTO khoa) { ds.Add(khoa); soSinhVien[khoa.MaKhoa] = 0; return true; }

        public bool Sua(KhoaDTO khoa) => true;

        public bool Xoa(string maKhoa) => ds.RemoveAll(k => k.MaKhoa == maKhoa) == 1;

        /// <summary>Chốt chương trình: mô phỏng UPDATE ... WHERE NgayKhoa IS NULL.</summary>
        public bool KhoaChuongTrinh(string maKhoa, string nguoiKhoa, int tongTCBatBuoc, int tongTCTuChon)
        {
            KhoaDTO? k = ds.Find(x => x.MaKhoa == maKhoa);
            if (k == null || k.NgayKhoa.HasValue) return false;

            k.NgayKhoa = new DateTime(2026, 9, 1, 8, 0, 0);
            k.NguoiKhoa = nguoiKhoa;
            k.TongTCBatBuoc = tongTCBatBuoc;
            k.TongTCTuChon = tongTCTuChon;
            return true;
        }
    }

    internal sealed class MonHocDALGia : IMonHocDAL
    {
        private readonly List<MonHocDTO> ds = new List<MonHocDTO>();

        public MonHocDALGia(params (string MaMH, int SoTinChi)[] mons)
        {
            foreach ((string ma, int tc) in mons)
                ds.Add(new MonHocDTO { MaMH = ma, TenMH = "Môn " + ma, SoTinChi = tc, GiangVien = "GV" });
        }

        public DataTable LayDanhSachMonHoc()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaMH", typeof(string));
            dt.Columns.Add("TenMH", typeof(string));
            dt.Columns.Add("SoTinChi", typeof(int));
            foreach (MonHocDTO m in ds) dt.Rows.Add(m.MaMH, m.TenMH, m.SoTinChi);
            return dt;
        }

        public bool KiemTraMaTonTai(string maMH) => ds.Exists(m => m.MaMH == maMH);
        public bool ThemMonHoc(MonHocDTO mh) { ds.Add(mh); return true; }
        public bool SuaMonHoc(MonHocDTO mh) => true;
        public bool XoaMonHoc(string maMH) => ds.RemoveAll(m => m.MaMH == maMH) == 1;
        public int DemLichHocTheoMon(string maMH) => 0;

        /// <summary>Tra số tín chỉ — dùng cho luật "tổng TC không vượt mức của khóa".</summary>
        public int? TinChiCua(string maMH)
        {
            MonHocDTO? m = ds.Find(x => x.MaMH == maMH);
            return m == null ? (int?)null : m.SoTinChi;
        }
    }

    internal sealed class ChuongTrinhDALGia : IChuongTrinhDAL
    {
        private readonly List<ChuongTrinhDTO> ds = new List<ChuongTrinhDTO>();
        private readonly MonHocDALGia monHoc;

        public ChuongTrinhDALGia(MonHocDALGia monHocDal) => monHoc = monHocDal;

        public void ThemSan(params ChuongTrinhDTO[] rows) => ds.AddRange(rows);

        public DataTable LayTheoKhoa(string maKhoa)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaKhoa", typeof(string));
            dt.Columns.Add("MaMH", typeof(string));
            dt.Columns.Add("TenMH", typeof(string));
            dt.Columns.Add("SoTinChi", typeof(int));
            dt.Columns.Add("BatBuoc", typeof(bool));
            dt.Columns.Add("HocKy", typeof(byte));

            foreach (ChuongTrinhDTO r in ds.FindAll(x => x.MaKhoa == maKhoa))
                dt.Rows.Add(r.MaKhoa, r.MaMH, "Môn " + r.MaMH, monHoc.TinChiCua(r.MaMH) ?? 0, r.BatBuoc, (byte)r.HocKy);

            return dt;
        }

        public bool KiemTraMonDaCo(string maKhoa, string maMH) =>
            ds.Exists(x => x.MaKhoa == maKhoa && x.MaMH == maMH);

        public bool Them(ChuongTrinhDTO dong) { ds.Add(dong); return true; }

        public bool Xoa(string maKhoa, string maMH) =>
            ds.RemoveAll(x => x.MaKhoa == maKhoa && x.MaMH == maMH) == 1;

        public int TongTinChi(string maKhoa, bool batBuoc)
        {
            int tong = 0;
            foreach (ChuongTrinhDTO r in ds.FindAll(x => x.MaKhoa == maKhoa && x.BatBuoc == batBuoc))
                tong += monHoc.TinChiCua(r.MaMH) ?? 0;
            return tong;
        }

        /// <summary>Thay toàn bộ chương trình của khóa bằng danh sách mới (giống transaction thật).</summary>
        public bool LuuCaChuongTrinh(string maKhoa, IList<ChuongTrinhDTO> danhSach)
        {
            ds.RemoveAll(x => x.MaKhoa == maKhoa);
            foreach (ChuongTrinhDTO d in danhSach)
                ds.Add(new ChuongTrinhDTO(maKhoa, d.MaMH, d.BatBuoc, d.HocKy));
            return true;
        }

        /// <summary>Số sinh viên của khóa đã có điểm ở môn này — đặt sẵn khi dựng test.</summary>
        public Dictionary<string, int> SoSinhVienCoDiem { get; } = new Dictionary<string, int>();

        public int DemSinhVienDaCoDiem(string maKhoa, string maMH) =>
            SoSinhVienCoDiem.TryGetValue(maKhoa + "|" + maMH, out int n) ? n : 0;

        public DataTable LayHocKy(string maKhoa)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("HocKy", typeof(int));

            List<int> cacKy = new List<int>();
            foreach (ChuongTrinhDTO r in ds.FindAll(x => x.MaKhoa == maKhoa))
                if (!cacKy.Contains(r.HocKy)) cacKy.Add(r.HocKy);

            cacKy.Sort();
            foreach (int ky in cacKy) dt.Rows.Add(ky);
            return dt;
        }

        public DataTable LayMonTheoKy(string maKhoa, int hocKy)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaMH", typeof(string));
            dt.Columns.Add("TenMH", typeof(string));
            dt.Columns.Add("SoTinChi", typeof(int));

            foreach (ChuongTrinhDTO r in ds.FindAll(x => x.MaKhoa == maKhoa && x.HocKy == hocKy))
                dt.Rows.Add(r.MaMH, "Môn " + r.MaMH, monHoc.TinChiCua(r.MaMH) ?? 0);

            return dt;
        }
    }

    internal sealed class KetQuaDALGia : IKetQuaDAL
    {
        private readonly Dictionary<string, decimal?> ds = new Dictionary<string, decimal?>();
        private readonly Dictionary<string, int> tinChi = new Dictionary<string, int>();

        public KetQuaDALGia(params (string MaMH, int SoTinChi)[] mons)
        {
            foreach ((string ma, int tc) in mons) tinChi[ma] = tc;
        }

        private static string Khoa(string sv, string mh) => sv + "|" + mh;

        public void DatDiem(string sv, string mh, decimal? diem) => ds[Khoa(sv, mh)] = diem;

        public DataTable LayTheoSinhVien(string maSV)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaMH", typeof(string));
            dt.Columns.Add("TenMH", typeof(string));
            dt.Columns.Add("SoTinChi", typeof(int));
            dt.Columns.Add("Diem", typeof(decimal));
            dt.Columns.Add("LanNhap", typeof(DateTime));

            foreach (KeyValuePair<string, decimal?> kv in ds)
            {
                string[] parts = kv.Key.Split('|');
                if (parts[0] != maSV) continue;
                dt.Rows.Add(parts[1], "Môn " + parts[1],
                            tinChi.TryGetValue(parts[1], out int tc) ? tc : 0,
                            kv.Value.HasValue ? (object)kv.Value.Value : DBNull.Value,
                            DateTime.Now);
            }
            return dt;
        }

        public DataTable LayTheoMon(string maMH)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSV", typeof(string));
            foreach (string k in ds.Keys) if (k.EndsWith("|" + maMH)) dt.Rows.Add(k.Split('|')[0]);
            return dt;
        }

        public bool KiemTraDaCo(string maSV, string maMH) => ds.ContainsKey(Khoa(maSV, maMH));

        public bool Them(KetQuaDTO kq) { ds[Khoa(kq.MaSV, kq.MaMH)] = kq.Diem; return true; }

        public bool Sua(KetQuaDTO kq) { ds[Khoa(kq.MaSV, kq.MaMH)] = kq.Diem; return true; }

        public bool Xoa(string maSV, string maMH) => ds.Remove(Khoa(maSV, maMH));

        public int TongSoDong => ds.Count;

        public int TinhTinChiTichLuy(string maSV)
        {
            int tong = 0;
            foreach (KeyValuePair<string, decimal?> kv in ds)
            {
                if (!kv.Key.StartsWith(maSV + "|")) continue;
                if (kv.Value == null || kv.Value < 2.0m) continue;
                string mh = kv.Key.Split('|')[1];
                tong += tinChi.TryGetValue(mh, out int tc) ? tc : 0;
            }
            return tong;
        }

        public DataTable LayBangDiem(string maKhoa, string maMH)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSV", typeof(string));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("Diem", typeof(decimal));

            foreach (KeyValuePair<string, decimal?> kv in ds)
            {
                string[] p = kv.Key.Split('|');
                if (p[1] != maMH) continue;
                dt.Rows.Add(p[0], "Sinh viên " + p[0],
                            kv.Value.HasValue ? (object)kv.Value.Value : DBNull.Value);
            }
            return dt;
        }

        /// <summary>Ghi cả bảng: có điểm thì đặt, để trống thì xoá dòng.</summary>
        public bool LuuBangDiem(string maMH, IList<DongDiem> danhSach)
        {
            foreach (DongDiem d in danhSach)
            {
                string k = Khoa(d.MaSV, maMH);
                if (d.Diem == null) ds.Remove(k);
                else ds[k] = d.Diem;
            }
            return true;
        }
    }

    internal sealed class LichHocDALGia : ILichHocDAL
    {
        private readonly List<LichHocDTO> ds = new List<LichHocDTO>();

        public DataTable LayDanhSachLichHoc() => new DataTable();
        public DataTable LayTheoMon(string maMH) => new DataTable();
        public DataTable LayTheoSinhVien(string maSV) => new DataTable();

        public DataTable LayTheoTuan(string maKhoa, int hocKy, DateTime tuNgay, DateTime denNgay)
            => new DataTable();

        public bool ThemLichHoc(LichHocDTO lh) { ds.Add(lh); return true; }
        public bool SuaLichHoc(LichHocDTO lh) => true;
        public bool XoaLichHoc(int maLich) => true;
        public int SoBuoi => ds.Count;
    }
}
