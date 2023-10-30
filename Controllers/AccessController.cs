using Microsoft.AspNetCore.Mvc;
using shop.Models;

namespace shop.Controllers
{
    public class AccessController : Controller
    {
        ShopContext db = new ShopContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(User user) 
        {
            if(HttpContext.Session.GetString("UserName")==null)
            {
                var u = db.Users.Where(x=>x.UserAccount.Equals(user.UserAccount)&&x.UserPassword.Equals(user.UserPassword)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserAccount.ToString());
                    return RedirectToAction("Index", "Home");
                }
               
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login","Access");
        }
    }
}
