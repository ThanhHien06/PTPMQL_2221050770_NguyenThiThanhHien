/*using System.Diagnostics;
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
*/
using DemoMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sinhvien.Models;
namespace DemoMVC.Controllers
{

    public class StudentController(ApplicationDBContext context) : Controller
    {
        private readonly ApplicationDBContext _context = context;
        public IActionResult Index()
        {
            // lấy danh sách sinh viên từ csdl
            var listStudents = _context.Students.ToList();
            // truyền danh sách sinh viên vào view
            return View(listStudents);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(std);
                _context.SaveChanges(); 
                return RedirectToAction("Index");
            }
            return View(std);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var std = await _context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, Student std)
        {
            if (id != std.StudentCode)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Entry(std).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");   
            }
            return View(std);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var std = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);

            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await _context.Students.FindAsync(id);

            if (std != null)
            {
                _context.Students.Remove(std);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var std = await _context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }

    }
}