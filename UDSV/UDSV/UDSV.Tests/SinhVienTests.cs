using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDSV.BLL;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.Tests
{
    /// <summary>
    /// DAL giả: giữ sinh viên trong bộ nhớ thay vì gọi SQL Server.
    /// Nhờ có interface ISinhVienDAL, các test dưới đây chạy được trên máy không có CSDL.
    /// </summary>
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
            dt.Columns.Add("Lop", typeof(string));
            dt.Columns.Add("SDT", typeof(string));
            dt.Columns.Add("DiemTB", typeof(decimal));

            foreach (SinhVienDTO sv in ds)
            {
                dt.Rows.Add(sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Lop, sv.SDT,
                            sv.DiemTB.HasValue ? (object)sv.DiemTB.Value : DBNull.Value);
            }
            return dt;
        }

        public DataTable LayDanhSachSinhVien() => ThanhBang(duLieu);

        public DataTable TimKiemSinhVien(string keyword) =>
            ThanhBang(duLieu.FindAll(s =>
                s.MaSV.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.HoTen.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

        public DataTable LaySinhVienTheoMa(string maSV) =>
            ThanhBang(duLieu.FindAll(s => s.MaSV == maSV));

        public bool KiemTraMaTonTai(string maSV) => duLieu.Exists(s => s.MaSV == maSV);

        public bool ThemSinhVien(SinhVienDTO sv) { duLieu.Add(sv); return true; }

        public bool SuaSinhVien(SinhVienDTO sv)
        {
            int viTri = duLieu.FindIndex(s => s.MaSV == sv.MaSV);
            if (viTri < 0) return false;
            duLieu[viTri] = sv;
            return true;
        }

        public bool XoaSinhVien(string maSV) => duLieu.RemoveAll(s => s.MaSV == maSV) == 1;
    }

    /// <summary>
    /// Mười test theo danh sách Chương 7 của giảng viên.
    /// Không test nào cần SQL Server: quy tắc thuần kiểm tra trực tiếp, phần cần ghi dữ liệu
    /// dùng SinhVienDALGia.
    /// </summary>
    [TestClass]
    public class SinhVienTests
    {
        /// <summary>Một sinh viên hợp lệ làm mốc cho các test.</summary>
        private static SinhVienDTO HopLe() => new SinhVienDTO(
            "SV001", "Nguyễn Văn An", new DateTime(2005, 5, 12), "Nam", "CNTT01", "0901234567", 3.45m);

        // 1
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

        // 2 — dữ liệu biên dưới
        [TestMethod]
        public void IsValidGPA_Zero_ReturnsTrue()
        {
            Assert.IsTrue(SinhVienBLL.IsValidGPA(0m));
        }

        // 3 — dữ liệu biên trên
        [TestMethod]
        public void IsValidGPA_Four_ReturnsTrue()
        {
            Assert.IsTrue(SinhVienBLL.IsValidGPA(4m));
        }

        // 4 — dữ liệu không hợp lệ
        [TestMethod]
        public void IsValidGPA_Negative_ReturnsFalse()
        {
            Assert.IsFalse(SinhVienBLL.IsValidGPA(-0.1m));
        }

        // 5 — vượt biên
        [TestMethod]
        public void IsValidGPA_AboveFour_ReturnsFalse()
        {
            Assert.IsFalse(SinhVienBLL.IsValidGPA(4.01m));
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

        // 8
        [TestMethod]
        public void ClassName_Empty_ReturnsFalse()
        {
            SinhVienDTO sv = HopLe();
            sv.Lop = "";

            Assert.IsFalse(SinhVienBLL.KiemTraDuLieu(sv, out string error));
            StringAssert.Contains(error, "Lớp");
        }

        // 9 — ghi qua DAL giả, không cần CSDL
        [TestMethod]
        public void CreateStudent_Valid_ReturnsTrue()
        {
            SinhVienDALGia dal = new SinhVienDALGia();
            SinhVienBLL bll = new SinhVienBLL(dal);

            Assert.IsTrue(bll.ThemSinhVien(HopLe(), out string error), error);
            Assert.AreEqual(1, dal.LayDanhSachSinhVien().Rows.Count);
        }

        // 10
        [TestMethod]
        public void SearchStudent_Found_ReturnsStudent()
        {
            SinhVienDALGia dal = new SinhVienDALGia();
            dal.ThemSan(HopLe());
            dal.ThemSan(new SinhVienDTO("SV002", "Trần Thị Bình", new DateTime(2005, 10, 20), "Nữ", "CNTT01", "0912345678", 3.72m));

            SinhVienBLL bll = new SinhVienBLL(dal);
            DataTable ketQua = bll.TimKiemSinhVien("SV001");

            Assert.AreEqual(1, ketQua.Rows.Count);
            Assert.AreEqual("Nguyễn Văn An", ketQua.Rows[0]["HoTen"]);
        }
    }
}
