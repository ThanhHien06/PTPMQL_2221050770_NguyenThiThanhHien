using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
   public class LoaiThietBi
    {
        public int LoaiThietBiId { get; set; }
        public string TenLoai { get; set; }

        public ICollection<ThietBi> ThietBis { get; set; }
    } 
}