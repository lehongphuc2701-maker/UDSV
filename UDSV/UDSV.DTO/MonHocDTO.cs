namespace UDSV.DTO
{
    public class MonHocDTO
    {
        public string MaMH { get; set; }
        public string TenMH { get; set; }
        public int SoTinChi { get; set; }
        public string GiangVien { get; set; }

        public MonHocDTO() { }

        public MonHocDTO(string maMH, string tenMH, int soTinChi, string giangVien)
        {
            MaMH = maMH;
            TenMH = tenMH;
            SoTinChi = soTinChi;
            GiangVien = giangVien;
        }
    }
}