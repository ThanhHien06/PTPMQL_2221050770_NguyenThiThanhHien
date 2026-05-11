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
    public class ThietBiController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ThietBiController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ThietBi/Index
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            // Phải có Include để lấy được tên Loại Thiết Bị
            var query = _context.ThietBis.Include(t => t.LoaiThietBi).AsQueryable();

            // Tìm kiếm theo Tên Thiết Bị
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(t => t.TenThietBi.Contains(searchString));
            }

            return View(await query.ToListAsync());
        }

        // GET: ThietBi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietBi = await _context.ThietBis
                .Include(t => t.LoaiThietBi)
                .FirstOrDefaultAsync(m => m.ThietBiId == id);
            if (thietBi == null)
            {
                return NotFound();
            }

            return View(thietBi);
        }

        // GET: ThietBi/Create
        public IActionResult Create()
        {
            ViewData["LoaiThietBiId"] = new SelectList(_context.LoaiThietBis, "LoaiThietBiId", "TenLoai");
            return View();
        }
        // POST: ThietBi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThietBiId,TenThietBi,LoaiThietBiId,SoLuongTon")] ThietBi thietBi)
        {
            ViewData["LoaiThietBiId"] = new SelectList(_context.LoaiThietBis, "LoaiThietBiId", "TenLoai", thietBi.LoaiThietBiId);
            
                _context.Add(thietBi);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
        }
    
        

        // GET: ThietBi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietBi = await _context.ThietBis.FindAsync(id);
            if (thietBi == null)
            {
                return NotFound();
            }
            ViewData["LoaiThietBiId"] = new SelectList(_context.LoaiThietBis, "LoaiThietBiId", "TenLoai", thietBi.LoaiThietBiId);
            return View(thietBi);
        }

        // POST: ThietBi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThietBiId,TenThietBi,LoaiThietBiId,SoLuongTon")] ThietBi thietBi)
        {
            ViewData["LoaiThietBiId"] = new SelectList(_context.LoaiThietBis, "LoaiThietBiId", "TenLoai", thietBi.LoaiThietBiId);
            if (id != thietBi.ThietBiId)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(thietBi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThietBiExists(thietBi.ThietBiId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

        

        // GET: ThietBi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietBi = await _context.ThietBis
                .Include(t => t.LoaiThietBi)
                .FirstOrDefaultAsync(m => m.ThietBiId == id);
            if (thietBi == null)
            {
                return NotFound();
            }

            return View(thietBi);
        }

        // POST: ThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thietBi = await _context.ThietBis.FindAsync(id);
            if (thietBi != null)
            {
                _context.ThietBis.Remove(thietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThietBiExists(int id)
        {
            return _context.ThietBis.Any(e => e.ThietBiId == id);
        }
    }
}
