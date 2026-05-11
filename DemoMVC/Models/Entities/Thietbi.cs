using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class ThietBi
    {
        public int ThietBiId { get; set; }
        public string TenThietBi { get; set; }

        public int LoaiThietBiId { get; set; }
        public LoaiThietBi LoaiThietBi { get; set; }

        public int SoLuongTon { get; set; }
    }
}