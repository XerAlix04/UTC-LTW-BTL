using Microsoft.AspNetCore.Mvc;
using UTC_TKW_BTL.Models;

namespace UTC_TKW_BTL.Controllers
{
    public class AccessController : Controller
    {
        QLBanThucPhamContext db = new QLBanThucPhamContext();
        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName")==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) 
                && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    if (u.LoaiUser == 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (u.LoaiUser == 1)
                    {
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}
