using Azure;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult SanPham(int? page) 
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.Products.AsNoTracking().OrderBy(x => x.Title);
            PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);
            return View(lst); 
        }
    }
}
