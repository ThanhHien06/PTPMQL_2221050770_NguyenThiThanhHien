using DemoMVC.Models;
using DemoMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        //sinh vien
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        
        //khach hang
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set;}
        public DbSet<KhachHang> KhachHangs { get; set;}
        public DbSet<DonHang> DonHangs { get; set;}
        public DbSet<SanPham> SanPhams { get; set;}
    }
}


