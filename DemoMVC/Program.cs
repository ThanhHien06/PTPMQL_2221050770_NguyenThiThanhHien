using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using OfficeOpenXml;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDBContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddControllersWithViews();
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        var app = builder.Build();

        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.Run();
    }
}