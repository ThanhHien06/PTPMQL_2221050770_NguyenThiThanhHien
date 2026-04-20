using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities 
{
    public class KhachHang 
    {
        [Key]
        public string MaKhachHang { get; set; } = default!;

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [Display(Name = "Tên khách hàng")]
        public string TenKhachHang { get; set; } = default!;

        [Display(Name = "Số điện thoại")]
        public string? SoDienThoai { get; set; }

        public virtual ICollection<DonHang>? DonHangs { get; set; }
    }
}