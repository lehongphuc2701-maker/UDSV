using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV.Tests
{
    /// <summary>Quy tắc của sinh viên: mã, họ tên, khóa bắt buộc, và phép cộng tín chỉ của danh mục môn.</summary>
    [TestClass]
    public class SinhVienTests
    {
        private static SinhVienDTO HopLe() =>
            new SinhVienDTO("SV001", "Nguyễn Văn An", new DateTime(2006, 5, 12), "Nam", "K24", "0901234567");

        private static SinhVienBLL TaoBll(out SinhVienDALGia dal)
        {
            dal = new SinhVienDALGia();
            return new SinhVienBLL(dal, new KhoaDALGia(new KhoaDTO("K24", "K24", 2024, 2028, 27, 6)));
        }

        [TestMethod]
        public void SinhVien_HopLe_TraVeTrue() =>
            Assert.IsTrue(SinhVienBLL.KiemTraDuLieu(HopLe(), out string error), error);

        [TestMethod]
        public void SinhVien_HoTenRong_TraVeFalse()
        {
            SinhVienDTO sv = HopLe();
            sv.HoTen = "   ";
            Assert.IsFalse(SinhVienBLL.KiemTraDuLieu(sv, out string error));
            StringAssert.Contains(error, "Họ tên");
        }

        [TestMethod]
        public void SinhVien_MaKhongBatDauBangSV_TraVeFalse()
        {
            SinhVienDTO sv = HopLe();
            sv.MaSV = "001";
            Assert.IsFalse(SinhVienBLL.KiemTraDuLieu(sv, out string error));
            StringAssert.Contains(error, "SV");
        }

        [TestMethod]
        public void SinhVien_ChuaThuocKhoa_TraVeFalse()
        {
            SinhVienDTO sv = HopLe();
            sv.MaKhoa = "";
            Assert.IsFalse(SinhVienBLL.KiemTraDuLieu(sv, out string error));
            StringAssert.Contains(error, "khóa");
        }

        [TestMethod]
        public void SinhVien_NgaySinhTuongLai_TraVeFalse()
        {
            SinhVienDTO sv = HopLe();
            sv.NgaySinh = DateTime.Today.AddDays(1);
            Assert.IsFalse(SinhVienBLL.KiemTraDuLieu(sv, out string error));
        }

        [TestMethod]
        public void SinhVien_SoDienThoaiCoChu_TraVeFalse()
        {
            SinhVienDTO sv = HopLe();
            sv.SDT = "0901abc";
            Assert.IsFalse(SinhVienBLL.KiemTraDuLieu(sv, out string error));
        }

        [TestMethod]
        public void SinhVien_ThemHopLe_ThanhCong()
        {
            SinhVienBLL bll = TaoBll(out SinhVienDALGia dal);

            Assert.IsTrue(bll.ThemSinhVien(HopLe(), out string error), error);
            Assert.AreEqual(1, dal.LayDanhSachSinhVien().Rows.Count);
        }

        [TestMethod]
        public void SinhVien_ThemTrungMa_BiTuChoi()
        {
            SinhVienBLL bll = TaoBll(out SinhVienDALGia dal);
            dal.ThemSan(HopLe());

            Assert.IsFalse(bll.ThemSinhVien(HopLe(), out string error));
            StringAssert.Contains(error, "đã tồn tại");
        }

        [TestMethod]
        public void SinhVien_ThemVoiKhoaKhongTonTai_BiTuChoi()
        {
            SinhVienBLL bll = TaoBll(out _);
            SinhVienDTO sv = HopLe();
            sv.MaKhoa = "K99";

            Assert.IsFalse(bll.ThemSinhVien(sv, out string error));
            StringAssert.Contains(error, "không tồn tại");
        }

        [TestMethod]
        public void SinhVien_TimKiemTheoTen_TimThay()
        {
            SinhVienBLL bll = TaoBll(out SinhVienDALGia dal);
            dal.ThemSan(HopLe());
            dal.ThemSan(new SinhVienDTO("SV002", "Trần Thị Bình", new DateTime(2006, 10, 20), "Nữ", "K24", "0912"));

            DataTable ketQua = bll.TimKiemSinhVien("Bình");

            Assert.AreEqual(1, ketQua.Rows.Count);
            Assert.AreEqual("SV002", ketQua.Rows[0]["MaSV"]);
        }

        [TestMethod]
        public void TongTinChi_CongDungSoMon()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SoTinChi", typeof(int));
            dt.Rows.Add(3);
            dt.Rows.Add(3);
            dt.Rows.Add(4);

            Assert.AreEqual(10, MonHocBLL.TongTinChi(dt));
        }

        [TestMethod]
        public void TongTinChi_BangRong_TraVeKhong()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SoTinChi", typeof(int));

            Assert.AreEqual(0, MonHocBLL.TongTinChi(dt));
        }

        [TestMethod]
        public void DoiTrangThai_ThoiHoc_TraVeTrue()
        {
            SinhVienBLL bll = TaoBll(out _);
            bool kq = bll.DoiTrangThai("SV001", "Đã thôi học");
            Assert.IsTrue(kq);
        }
    }
}
