using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class ChiTietPhieuXuat
    {
        public int ChiTietPhieuXuatId { get; set; }

        public int PhieuXuatId { get; set; }
        public PhieuXuat PhieuXuat { get; set; }

        public int ThietBiId { get; set; }
        public ThietBi ThietBi { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGiaXuat { get; set; }

        public decimal ThanhTien => SoLuong * DonGiaXuat;
    }
}