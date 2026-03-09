using Microsoft.EntityFrameworkCore;
using Sinhvien.Models;

namespace DemoMVC.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}