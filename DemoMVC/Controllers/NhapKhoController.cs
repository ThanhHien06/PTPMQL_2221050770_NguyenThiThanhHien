using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class NhapKhoController : Controller
{
    private readonly ApplicationDBContext _context;

    public NhapKhoController(ApplicationDBContext context)
    {
        _context = context;
    }

    // DANH SÁCH
    public IActionResult Index()
    {
        var data = _context.PhieuNhaps
            .Include(x => x.NhaCungCap)
            .ToList();

        return View(data);
    }

    // CREATE GET
    public IActionResult Create()
    {
        ViewBag.NhaCungCapId = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap");
        ViewBag.ThietBi = _context.ThietBis.ToList();

        return View(new NhapKhoVM
        {
            PhieuNhap = new PhieuNhap(),
            ChiTietPhieuNhaps = new List<ChiTietPhieuNhap>()
        });
    }

    // CREATE POST
[HttpPost]
public IActionResult Create(NhapKhoVM model)
{
    // 1. TẠM THỜI XÓA (HOẶC COMMENT BỎ) ĐOẠN KIỂM TRA MODELSTATE NÀY
    /*
    if (!ModelState.IsValid)
    {
        ViewBag.NhaCungCapId = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap");
        ViewBag.ThietBi = _context.ThietBis.ToList();
        return View(model);
    }
    */

    // 2. ÉP CHẠY THẲNG VÀO DATABASE VÀ BẮT LỖI
    try
    {
        // Lưu phiếu nhập
        _context.PhieuNhaps.Add(model.PhieuNhap);
        _context.SaveChanges();

        // Lưu chi tiết + cập nhật tồn kho
        if (model.ChiTietPhieuNhaps != null)
        {
            foreach (var item in model.ChiTietPhieuNhaps)
            {
                if (item.SoLuong <= 0) continue;

                // Lấy ID phiếu vừa tạo gán cho chi tiết
                item.PhieuNhapId = model.PhieuNhap.PhieuNhapId;
                _context.ChiTietPhieuNhaps.Add(item);

                // Cộng tồn kho
                var tb = _context.ThietBis.Find(item.ThietBiId);
                if (tb != null)
                {
                    tb.SoLuongTon += item.SoLuong;
                }
            }
            _context.SaveChanges();
        }

        // Lưu thành công thì nhảy về danh sách
        return RedirectToAction("Index");
    }
    catch (System.Exception ex)
    {
        // NẾU DATABASE CHẶN LẠI, ÉP HIỂN THỊ LỖI RA MÀN HÌNH
        var errorMsg = ex.Message;
        if (ex.InnerException != null) 
        {
            errorMsg += " | Chi tiết sâu hơn: " + ex.InnerException.Message;
        }
        return Content("🚨 PHÁT HIỆN LỖI RỒI: " + errorMsg);
    }
}
    // CHI TIẾT
    public IActionResult Details(int id)
    {
        var data = _context.PhieuNhaps
            .Include(x => x.NhaCungCap)
            .Include(x => x.ChiTietPhieuNhaps)
                .ThenInclude(ct => ct.ThietBi)
            .FirstOrDefault(x => x.PhieuNhapId == id);

        return View(data);
    }

    // XÓA
    public IActionResult Delete(int id)
    {
        var data = _context.PhieuNhaps.Find(id);
        return View(data);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var phieu = _context.PhieuNhaps
            .Include(x => x.ChiTietPhieuNhaps)
            .FirstOrDefault(x => x.PhieuNhapId == id);

        // Trừ lại tồn kho khi xóa
        foreach (var item in phieu.ChiTietPhieuNhaps)
        {
            var tb = _context.ThietBis.Find(item.ThietBiId);
            tb.SoLuongTon -= item.SoLuong;
        }

        _context.Remove(phieu);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}