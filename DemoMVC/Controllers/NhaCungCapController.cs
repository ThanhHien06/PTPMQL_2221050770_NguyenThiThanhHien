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
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDBContext _context;

        public NhaCungCapController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: NhaCungCap
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhaCungCaps.ToListAsync());
        }

        // GET: NhaCungCap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCaps
                .FirstOrDefaultAsync(m => m.NhaCungCapId == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        // GET: NhaCungCap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhaCungCap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhaCungCapId,TenNhaCungCap,DienThoai,DiaChi")] NhaCungCap nhaCungCap)
        {
            _context.Add(nhaCungCap);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        // GET: NhaCungCap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCaps.FindAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            return View(nhaCungCap);
        }

        // POST: NhaCungCap/Edit/5
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("NhaCungCapId,TenNhaCungCap,DienThoai,DiaChi")] NhaCungCap nhaCungCap)
{
    if (id != nhaCungCap.NhaCungCapId)
    {
        return NotFound();
    }

    ModelState.Remove("PhieuNhaps"); 

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(nhaCungCap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Lưu xong nhảy về trang chủ
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NhaCungCapExists(nhaCungCap.NhaCungCapId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }
    
    // Nếu vẫn lỗi ModelState, nó sẽ trả lại View kèm thông báo lỗi
    return View(nhaCungCap);
}
        // GET: NhaCungCap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCaps
                .FirstOrDefaultAsync(m => m.NhaCungCapId == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        // POST: NhaCungCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhaCungCap = await _context.NhaCungCaps.FindAsync(id);
            if (nhaCungCap != null)
            {
                _context.NhaCungCaps.Remove(nhaCungCap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhaCungCapExists(int id)
        {
            return _context.NhaCungCaps.Any(e => e.NhaCungCapId == id);
        }
    }
}
