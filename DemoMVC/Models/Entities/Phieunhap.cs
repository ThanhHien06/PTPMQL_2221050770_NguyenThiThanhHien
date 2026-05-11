using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class PhieuNhap
    {
        public int PhieuNhapId { get; set; }
        public DateTime NgayNhap { get; set; }

        public int NhaCungCapId { get; set; }
        public NhaCungCap NhaCungCap { get; set; }

        public ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}