using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities {
    public class ChiTietDonHang 
    {
        [Key]
        public int Id { get; set; }

        public int MaDonHang { get; set; }
        [ForeignKey("MaDonHang")]
        public virtual DonHang? DonHang { get; set; }

        public string MaSanPham { get; set; } = default!;
        [ForeignKey("MaSanPham")]
        public virtual SanPham? SanPham { get; set; }

        public int SoLuong { get; set; }
    }
}