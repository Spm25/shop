using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using shop.Models;
using X.PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/Home")]
    public class HomeController : Controller
    {
        ShopContext db = new ShopContext();
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("SanPham")]
        public IActionResult SanPham(int? page, int? loai) 
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if(loai == null)
            {
                var lstSanPham = db.Products.AsNoTracking().OrderBy(x => x.Title);
                PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);
                return View(lst);
            }
            else
            {
                var lstSanPham = db.Products.Where(l=>l.CategoryId == loai).AsNoTracking().OrderBy(x => x.Title);
                PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);
                return View(lst);
            }
            
        }
        [Route("AddSP")]
        [HttpGet]
        public IActionResult ThemSanPham() 
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "Type");
            return View();
        }
        [Route("AddSP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(Product sanPham) 
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("SanPham");
            }
            return View(sanPham);
        }
        [Route("EditSP")]
        [HttpGet]
        public IActionResult SuaSanPham(int maSanPham)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "Type");
            var sanPham = db.Products.Find(maSanPham);
            return View(sanPham);
        }
        [Route("EditSP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Product sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Update(sanPham);
                db.SaveChanges();
                return RedirectToAction("SanPham");
            }
            return View(sanPham);
        }
        [Route("XoaSP")]
        [HttpGet]
        public IActionResult XoaSanPham(int maSanPham)
        {
            TempData["Message"] = "";
            db.Remove(db.Products.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] =  "Sản phẩm đã xóa";
            return RedirectToAction("SanPham", "Home");
        }
    }
}
