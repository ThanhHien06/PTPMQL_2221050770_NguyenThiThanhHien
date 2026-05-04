using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Services;
using Microsoft.AspNetCore.Mvc;

public class ImportController : Controller
{
    private readonly ApplicationDBContext _context;
    private readonly ExcelService _excelService;

    public ImportController(ApplicationDBContext context)
    {
        _context = context;
        _excelService = new ExcelService(); 
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ImportStudent(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return View("Index");
        
        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        var filePath = Path.Combine("Uploads", file.FileName);

        // lưu file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // đọc excel
        var students = _excelService.ReadStudentsFromExcel(filePath);

        // lưu DB
        _context.Students.AddRange(students);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Student");
    }
}