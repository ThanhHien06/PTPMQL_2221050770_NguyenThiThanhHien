using System.Collections.Generic;
using DemoMVC.Models.Entities;
namespace DemoMVC.ViewModel
{
    public class NhapKhoVM
    {
        public PhieuNhap PhieuNhap { get; set; }
        public List<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}