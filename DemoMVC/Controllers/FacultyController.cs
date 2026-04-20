/*using DemoMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sinhvien.Models;

namespace DemoMVC.Controllers
{
    public class FacultyController : Controller
    {
        private readonly ApplicationDBContext _context;

        public FacultyController(ApplicationDBContext context)
        {
            _context = context;
        }

        // LIST (INDEX)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Faculties.ToListAsync());
        }

        // DETAILS
        public async Task<IActionResult> Details(string id)
        {
            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(f => f.FacultyId == id);

            if (faculty == null)
                return NotFound();

            return View(faculty);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty faculty)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);

            if (faculty == null)
                return NotFound();

            return View(faculty);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Faculty faculty)
        {
            if (id != faculty.FacultyId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(faculty);
        }

        // DELETE (GET)
        public async Task<IActionResult> Delete(string id)
        {
            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(f => f.FacultyId == id);

            if (faculty == null)
                return NotFound();

            return View(faculty);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var faculty = await _context.Faculties.FindAsync(id);

            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}*/
using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DemoMVC.Controllers;

public class FacultyController : Controller
{
    private readonly ApplicationDBContext _context;

    public FacultyController(ApplicationDBContext context)
    {
        _context = context;
    }

    // INDEX 
    public async Task<IActionResult> Index()
    {
        return View(await _context.Faculties.ToListAsync());
    }

    // DETAILS 
    public async Task<IActionResult> Details(string id)
    {
        if (id == null) 
        {
            return NotFound();
        }

        var faculty = await _context.Faculties.FindAsync(id);
        if (faculty == null) 
        {
            return NotFound();
        }
        
        return View(faculty);
    }

    // CREATE
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Faculty faculty)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("LỖI: " + error.ErrorMessage);
            }
        }

        if (ModelState.IsValid)
        {
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View(faculty);
    }

    // EDIT 
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null) 
        {
            return NotFound();
        }

        var faculty = await _context.Faculties.FindAsync(id);
        if (faculty == null) 
        {
            return NotFound();
        }

        return View(faculty);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Faculty faculty)
    {
        if (id != faculty.FacultyId) 
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(faculty);
        }
        _context.Update(faculty);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index)); 
    }

    // DELETE 
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null) 
        {
            return NotFound();
        }

        var faculty = await _context.Faculties.FindAsync(id);
        if (faculty == null) 
        {
            return NotFound();
        }

        return View(faculty);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var faculty = await _context.Faculties.FindAsync(id);

        if (faculty != null)
        {
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index)); 
    }
}