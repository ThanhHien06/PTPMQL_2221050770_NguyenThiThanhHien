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

        //quan ly kho
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<LoaiThietBi> LoaiThietBis { get; set; }
        public DbSet<ThietBi> ThietBis { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<PhieuXuat> PhieuXuats { get; set; }
        public DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }

        // book
        public DbSet<Book> Books { get; set; }
    }
}


