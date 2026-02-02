using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sinhvien.Models;

public class StudentController : Controller
{
    // Hiển thị form
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    // Nhận dữ liệu từ form
    [HttpPost]
    public ActionResult Index(Student std)
    {
        // Gửi lại object Student sang View
        ViewBag.Thongbao = "Xin chào: " + std.FullName + " - Mã sinh viên: " + std.StudentCode;
        return View();
    }
}
