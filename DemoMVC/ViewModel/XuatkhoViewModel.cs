using System.Collections.Generic;
using DemoMVC.Models.Entities;

namespace DemoMVC.ViewModel
{
    public class XuatKhoVM
    {
        public PhieuXuat PhieuXuat { get; set; }
        public List<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
    }
}