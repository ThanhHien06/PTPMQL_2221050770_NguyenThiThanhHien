using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities {
    public class SanPham 
    {
        [Key]
        public string MaSanPham { get; set; } = default!;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string TenSanPham { get; set; } = default!;

        [Range(0, double.MaxValue)]
        public decimal Gia { get; set; }

        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}