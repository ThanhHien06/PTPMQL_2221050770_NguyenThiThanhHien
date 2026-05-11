using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class PhieuXuat
    {
        public int PhieuXuatId { get; set; }
        public DateTime NgayXuat { get; set; }

        public ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
    }
}