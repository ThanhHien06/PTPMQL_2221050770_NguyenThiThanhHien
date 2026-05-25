/*
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sinhvien.ViewModels;
namespace DemoMVC.Controllers
{

    public class StudentController(ApplicationDBContext context) : Controller
    {
        private readonly ApplicationDBContext _context = context;
        public IActionResult Index()
        {
            var data = _context.Students
                .Include(s => s.Faculty)
                .Select(s => new StudentFacultyViewModel
                {
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    FacultyName = s.Faculty.FacultyName
                }).ToList();

            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(_context.Faculties, "FacultyId", "FacultyName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.FacultyId = new SelectList(_context.Faculties, "FacultyId", "FacultyName", std.FacultyID);

                return View(std);
            }

            _context.Students.Add(std);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var std = await _context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
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
                .Include(x => x.Faculty)
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
             var std = _context.Students
                .Include(x => x.Faculty)
                .FirstOrDefault(x => x.StudentCode == id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }

    }
}*/
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sinhvien.ViewModels;

namespace DemoMVC.Controllers
{
    public class StudentController(ApplicationDBContext context) : Controller
    {
        private readonly ApplicationDBContext _context = context;

        public IActionResult Index()
        {
            return View();
        }

        // Action mới dùng cho AJAX để load bảng dữ liệu
        public async Task<IActionResult> GetStudents(int page = 1, int pageSize = 10)
        {
            var query = _context.Students
                .Include(s => s.Faculty)
                .AsNoTracking();

            var totalItems = await query.CountAsync();

            var students = await query
                .OrderBy(s => s.StudentCode)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentFacultyViewModel
                {
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    Age = s.Age,
                    FacultyName = s.Faculty.FacultyName,
                    Email = s.Email 
                })
                .ToListAsync();

            var result = new PagedResult<StudentFacultyViewModel>
            {
                Items = students,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return PartialView("_StudentTable", result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(_context.Faculties, "FacultyId", "FacultyName");
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            ModelState.Remove("Faculty");

            if (!ModelState.IsValid)
            {
                ViewBag.FacultyId = new SelectList(_context.Faculties, "FacultyId", "FacultyName", std.FacultyID);
                return PartialView("_Create", std);
            }

            _context.Students.Add(std);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            // Tìm sinh viên có StudentCode trùng khớp
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentCode == id);
            
            if (student == null) return NotFound();

            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyID);
            return PartialView("_Edit", student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student std) 
        {
            // Bỏ qua kiểm tra lỗi của đối tượng Faculty
            ModelState.Remove("Faculty");

            if (ModelState.IsValid)
            {
                try
                {
                    // Báo cho Database biết là thông tin đã được chỉnh sửa
                    _context.Entry(std).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    
                    // Trả về thành công để JavaScript đóng Popup và load lại bảng
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi lúc lưu vào SQL, trả về lỗi để báo ra F12
                    return BadRequest(ex.Message);
                }
            }

            // Nếu form bị nhập sai/thiếu, nạp lại danh sách Khoa và trả về form để hiện lỗi đỏ
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyId", "FacultyName", std.FacultyID);
            return PartialView("_Edit", std);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var std = await _context.Students
                .Include(x => x.Faculty)
                .FirstOrDefaultAsync(m => m.StudentCode == id);

            if (std == null) return NotFound();

            return PartialView("_Delete", std);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string studentCode)
        {
            var std = await _context.Students.FirstOrDefaultAsync(s => s.StudentCode == studentCode);
            if (std == null)
            {
                return Json(new { success = false });
            }

            _context.Students.Remove(std);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}