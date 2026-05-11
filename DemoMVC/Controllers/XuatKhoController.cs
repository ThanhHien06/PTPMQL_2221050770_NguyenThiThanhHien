using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DemoMVC.Controllers
{
    public class XuatKhoController : Controller
    {
        private readonly ApplicationDBContext _context;

        public XuatKhoController(ApplicationDBContext context)
        {
            _context = context;
        }

        // danh sách phiếu xuất
        public IActionResult Index()
        {
            var data = _context.PhieuXuats.ToList(); 
            return View(data);
        }

        // GET: GIAO DIỆN TẠO PHIẾU
        public IActionResult Create()
        {
            ViewBag.ThietBi = _context.ThietBis.ToList();

            return View(new XuatKhoVM
            {
                PhieuXuat = new PhieuXuat(),
                ChiTietPhieuXuats = new List<ChiTietPhieuXuat>()
            });
        }

        // POST: XỬ LÝ LƯU & TRỪ TỒN KHO
        [HttpPost]
        public IActionResult Create(XuatKhoVM model)
        {
            try
            {
                // KIỂM TRA SỐ LƯỢNG TỒN TRƯỚC (BẮT BUỘC)
                if (model.ChiTietPhieuXuats != null)
                {
                    foreach (var item in model.ChiTietPhieuXuats)
                    {
                        if (item.SoLuong <= 0) continue;

                        var tb = _context.ThietBis.Find(item.ThietBiId);
                        // Chặn nếu trong kho ít hơn số lượng định xuất
                        if (tb != null && tb.SoLuongTon < item.SoLuong)
                        {
                            ModelState.AddModelError("", $"LỖI: Thiết bị '{tb.TenThietBi}' chỉ còn {tb.SoLuongTon} cái trong kho, không đủ xuất {item.SoLuong} cái!");
                            ViewBag.ThietBi = _context.ThietBis.ToList();
                            return View(model);
                        }
                    }
                }

                // Đủ hàng -> Lưu phiếu xuất
                _context.PhieuXuats.Add(model.PhieuXuat);
                _context.SaveChanges();

                // Lưu chi tiết + Trừ tồn kho
                if (model.ChiTietPhieuXuats != null)
                {
                    foreach (var item in model.ChiTietPhieuXuats)
                    {
                        if (item.SoLuong <= 0) continue;

                        item.PhieuXuatId = model.PhieuXuat.PhieuXuatId;
                        _context.ChiTietPhieuXuats.Add(item);

                        var tb = _context.ThietBis.Find(item.ThietBiId);
                        if (tb != null)
                        {
                            tb.SoLuongTon -= item.SoLuong; // Trừ kho
                        }
                    }
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                return Content("PHÁT HIỆN LỖI RỒI: " + ex.Message);
            }
        }
        // GET: XuatKho/Details/1
        public IActionResult Details(int id)
        {
            // Tìm phiếu xuất kèm theo danh sách thiết bị bên trong
            var data = _context.PhieuXuats
                .Include(x => x.ChiTietPhieuXuats)
                    .ThenInclude(ct => ct.ThietBi)
                .FirstOrDefault(x => x.PhieuXuatId == id);

            // Nếu không tìm thấy ID này trong Database, nó sẽ trả về 404
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }
    }
}