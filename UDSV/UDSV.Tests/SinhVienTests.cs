using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDSV.BLL;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.Tests
{
    /// <summary>DAL giả cho sinh viên: giữ dữ liệu trong bộ nhớ, không cần SQL Server.</summary>
    internal sealed class SinhVienDALGia : ISinhVienDAL
    {
        private readonly List<SinhVienDTO> duLieu = new List<SinhVienDTO>();

        public void ThemSan(params SinhVienDTO[] ds) => duLieu.AddRange(ds);

        private static DataTable ThanhBang(IEnumerable<SinhVienDTO> ds)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSV", typeof(string));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("NgaySinh", typeof(DateTime));
            dt.Columns.Add("GioiTinh", typeof(string));
            dt.Columns.Add("MaKhoa", typeof(string));
            dt.Columns.Add("TenKhoa", typeof(string));
            dt.Columns.Add("SDT", typeof(string));

            foreach (SinhVienDTO sv in ds)
                dt.Rows.Add(sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.MaKhoa, "Khóa " + sv.MaKhoa, sv.SDT);

            return dt;
        }

        public DataTable LayDanhSachSinhVien() => ThanhBang(duLieu);

        public DataTable TimKiemSinhVien(string keyword) =>
            ThanhBang(duLieu.FindAll(s =>
                s.MaSV.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.HoTen.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

        public DataTable LaySinhVienTheoMa(string maSV) => ThanhBang(duLieu.FindAll(s => s.MaSV == maSV));

        public bool KiemTraMaTonTai(string maSV) => duLieu.Exists(s => s.MaSV == maSV);

        public bool ThemSinhVien(SinhVienDTO sv) { duLieu.Add(sv); return true; }

        public bool SuaSinhVien(SinhVienDTO sv)
        {
            int i = duLieu.FindIndex(s => s.MaSV == sv.MaSV);
            if (i < 0) return false;
            duLieu[i] = sv;
            return true;
        }

        public bool XoaSinhVien(string maSV) => duLieu.RemoveAll(s => s.MaSV == maSV) == 1;
    }

    /// <summary>DAL giả cho khóa — chỉ cần biết khóa có tồn tại hay không.</summary>
    internal sealed class KhoaDALGia : IKhoaDAL
    {
        private readonly List<KhoaDTO> duLieu = new List<KhoaDTO>();

        public KhoaDALGia(params string[] maKhoa)
        {
            foreach (string m in maKhoa)
                duLieu.Add(new KhoaDTO(m, "Khóa " + m, 2024, 2028, 27, 6));
        }

        public DataTable LayDanhSach() => new DataTable();

        public KhoaDTO? LayTheoMa(string maKhoa) => duLieu.Find(k => k.MaKhoa == maKhoa);

        public bool KiemTraTonTai(string maKhoa) => duLieu.Exists(k => k.MaKhoa == maKhoa);

        public int DemSinhVien(string maKhoa) => 0;

        public bool Them(KhoaDTO khoa) { duLieu.Add(khoa); return true; }

        public bool Sua(KhoaDTO khoa) => true;

        public bool Xoa(string maKhoa) => duLieu.RemoveAll(k => k.MaKhoa == maKhoa) == 1;
    }

    /// <summary>
    /// Bộ test nền của mô hình mới: quy tắc sinh viên, điểm, và phép cộng tín chỉ.
    /// Không test nào cần SQL Server — quy tắc thuần kiểm tra trực tiếp, phần cần ghi dữ liệu
    /// dùng DAL giả.
    /// </summary>
    [TestClass]
    public class SinhVienTests
    {
        private static SinhVienDTO HopLe() => new SinhVienDTO(
            "SV001", "Nguyễn Văn An", new DateTime(2006, 5, 12), "Nam", "K24", "0901234567");

        private static SinhVienBLL TaoBLL(out SinhVienDALGia svDal)
        {
            svDal = new SinhVienDALGia();
            return new SinhVienBLL(svDal, new KhoaDALGia("K24", "K25"));
        }

        // 1. Phép cộng tín chỉ của chương trình
        [TestMethod]
        public void Add_ReturnsCorrectSum()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SoTinChi", typeof(int));
            dt.Rows.Add(3);
            dt.Rows.Add(3);
            dt.Rows.Add(4);

            Assert.AreEqual(10, MonHocBLL.TongTinChi(dt));
        }

        // 2 — biên dưới
        [TestMethod]
        public void Diem_Zero_ReturnsTrue()
        {
            Assert.IsTrue(KetQuaBLL.KiemTraDiem(0m, out string error), error);
        }

        // 3 — biên trên
        [TestMethod]
        public void Diem_Four_ReturnsTrue()
        {
            Assert.IsTrue(KetQuaBLL.KiemTraDiem(4m, out string error), error);
        }

        // 4 — âm
        [TestMethod]
        public void Diem_Negative_ReturnsFalse()
        {
            Assert.IsFalse(KetQuaBLL.KiemTraDiem(-0.1m, out _));
        }

        // 5 — vượt biên
        [TestMethod]
        public void Diem_AboveFour_ReturnsFalse()
        {
            Assert.IsFalse(KetQuaBLL.KiemTraDiem(4.01m, out _));
        }

        // 6
        [TestMethod]
        public void FullName_Empty_ReturnsFalse()
        {
            SinhVienDTO sv = HopLe();
            sv.HoTen = "   ";

            Assert.IsFalse(SinhVienBLL.KiemTraDuLieu(sv, out string error));
            StringAssert.Contains(error, "Họ tên");
        }

        // 7
        [TestMethod]
        public void FullName_Normal_ReturnsTrue()
        {
            SinhVienDTO sv = HopLe();
            Assert.IsTrue(SinhVienBLL.KiemTraDuLieu(sv, out string error), error);
        }

        // 8 — sinh viên phải thuộc một khóa
        [TestMethod]
        public void Khoa_Empty_ReturnsFalse()
        {
            SinhVienDTO sv = HopLe();
            sv.MaKhoa = "";

            Assert.IsFalse(SinhVienBLL.KiemTraDuLieu(sv, out string error));
            StringAssert.Contains(error, "khóa");
        }

        // 9
        [TestMethod]
        public void CreateStudent_Valid_ReturnsTrue()
        {
            SinhVienBLL bll = TaoBLL(out SinhVienDALGia dal);

            Assert.IsTrue(bll.ThemSinhVien(HopLe(), out string error), error);
            Assert.AreEqual(1, dal.LayDanhSachSinhVien().Rows.Count);
        }

        // 10
        [TestMethod]
        public void SearchStudent_Found_ReturnsStudent()
        {
            SinhVienBLL bll = TaoBLL(out SinhVienDALGia dal);
            dal.ThemSan(HopLe());
            dal.ThemSan(new SinhVienDTO("SV002", "Trần Thị Bình", new DateTime(2006, 10, 20), "Nữ", "K24", "0912345678"));

            DataTable ketQua = bll.TimKiemSinhVien("SV001");

            Assert.AreEqual(1, ketQua.Rows.Count);
            Assert.AreEqual("Nguyễn Văn An", ketQua.Rows[0]["HoTen"]);
        }
    }
}
