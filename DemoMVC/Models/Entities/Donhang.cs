using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities {
    public class DonHang 
    {
        [Key]
        public int MaDonHang { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayDat { get; set; }

        public string MaKhachHang { get; set; } = default!;
        [ForeignKey("MaKhachHang")]
        public virtual KhachHang? KhachHang { get; set; }

        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}