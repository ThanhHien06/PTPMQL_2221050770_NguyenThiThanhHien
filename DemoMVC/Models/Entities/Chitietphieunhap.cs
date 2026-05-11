using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class ChiTietPhieuNhap
    {
        public int ChiTietPhieuNhapId { get; set; }

        public int PhieuNhapId { get; set; }
        public PhieuNhap PhieuNhap { get; set; }

        public int ThietBiId { get; set; }
        public ThietBi ThietBi { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }

        public decimal ThanhTien => SoLuong * DonGiaNhap;
    }
}