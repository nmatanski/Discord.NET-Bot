using DiscordWebApplication.Extensions;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Models;

namespace DiscordWebApplication.Controllers
{
    public class ManageController : Controller
    {
        private string _userId;
        private string _username;
        private Role _role;

        public IActionResult Index()
        {
            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            if (_userId == null) return RedirectToAction("AccessDenied", "Home");
            _username = HttpContext.Session.GetObjectFromJson<string>("UserName");
            _role = HttpContext.Session.GetObjectFromJson<Role>("Role");
            ViewData["UserId"] = _userId;
            ViewData["UserName"] = _username;
            ViewData["Role"] = _role;
            return View();
        }
    }
}