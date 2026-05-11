using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;

namespace DemoMVC.Controllers
{
    public class LoaiThietBiController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LoaiThietBiController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: LoaiThietBi/Index
        public async Task<IActionResult> Index(string searchString)
        {
            // Lưu lại từ khóa để hiển thị lại trên ô tìm kiếm sau khi load trang
            ViewData["CurrentFilter"] = searchString;

            // Lấy toàn bộ dữ liệu ra dưới dạng Query
            var query = _context.LoaiThietBis.AsQueryable();

            // Nếu người dùng có nhập từ khóa -> Lọc dữ liệu theo Tên Loại
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(l => l.TenLoai.Contains(searchString));
            }

            return View(await query.ToListAsync());
        }

        // GET: LoaiThietBi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiThietBi = await _context.LoaiThietBis
                .FirstOrDefaultAsync(m => m.LoaiThietBiId == id);
            if (loaiThietBi == null)
            {
                return NotFound();
            }

            return View(loaiThietBi);
        }

        // GET: LoaiThietBi/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiThietBiId,TenLoai")] LoaiThietBi loaiThietBi)
        {
        
                _context.Add(loaiThietBi);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
        }
        

        // GET: LoaiThietBi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiThietBi = await _context.LoaiThietBis.FindAsync(id);
            if (loaiThietBi == null)
            {
                return NotFound();
            }
            return View(loaiThietBi);
        }

        // POST: LoaiThietBi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiThietBiId,TenLoai")] LoaiThietBi loaiThietBi)
        {
            if (id != loaiThietBi.LoaiThietBiId)
            {
                return NotFound();
            }
            ModelState.Remove("ThietBis"); 

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiThietBi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Lưu xong về trang danh sách
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiThietBiExists(loaiThietBi.LoaiThietBiId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(loaiThietBi);
        }

        // GET: LoaiThietBi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiThietBi = await _context.LoaiThietBis
                .FirstOrDefaultAsync(m => m.LoaiThietBiId == id);
            if (loaiThietBi == null)
            {
                return NotFound();
            }

            return View(loaiThietBi);
        }

        // POST: LoaiThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiThietBi = await _context.LoaiThietBis.FindAsync(id);
            if (loaiThietBi != null)
            {
                _context.LoaiThietBis.Remove(loaiThietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiThietBiExists(int id)
        {
            return _context.LoaiThietBis.Any(e => e.LoaiThietBiId == id);
        }
    }
}
