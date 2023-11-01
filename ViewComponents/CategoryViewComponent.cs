using Microsoft.AspNetCore.Mvc;
using shop.Models;

namespace shop.ViewComponents
{
    public class CategoryViewComponent : ViewComponent 
    {
        ShopContext db;
        List<Category> categories;

        public CategoryViewComponent(ShopContext db)
        {
            this.db = db;
            this.categories = db.Categories.ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderCategory", categories);
        }
    }
}
