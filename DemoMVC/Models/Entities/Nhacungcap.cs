using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities 
{
    public class NhaCungCap
    {
        public int NhaCungCapId { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }

        public ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}