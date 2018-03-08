using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Database;
using UserService.Domain.Models;
using DiscordWebApplication.Extensions;
using PagedList.Core;

namespace DiscordWebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        private string _userId;
        private string _username;
        private Role _role;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageMaxNumber = 5;

            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            if (_userId == null)
            {
                ViewBag["NoPermissions"] = "Log in first.";
                return RedirectToAction("Login", "Account");
            }
            _username = HttpContext.Session.GetObjectFromJson<string>("UserName");
            _role = HttpContext.Session.GetObjectFromJson<Role>("Role");
            ViewData["UserId"] = _userId;
            ViewData["UserName"] = _username;
            ViewData["Role"] = _role;

            //return View((await _context.Users.ToListAsync()).ToPagedList(pageNumber, pageMaxNumber));
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            if (_userId == null) return RedirectToAction("AccessDenied", "Home");
            _username = HttpContext.Session.GetObjectFromJson<string>("UserName");
            _role = HttpContext.Session.GetObjectFromJson<Role>("Role");
            ViewData["UserId"] = _userId;
            ViewData["UserName"] = _username;
            ViewData["Role"] = _role;

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,ImgUrl,Email,Password,UserRole,ValidationCode,EmailConfirmed,DateCreated,DateChanged")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            if (_userId == null) return RedirectToAction("AccessDenied", "Home");
            _username = HttpContext.Session.GetObjectFromJson<string>("UserName");
            _role = HttpContext.Session.GetObjectFromJson<Role>("Role");
            ViewData["UserId"] = _userId;
            ViewData["UserName"] = _username;
            ViewData["Role"] = _role;

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            if (_userId == null) return RedirectToAction("AccessDenied", "Home");
            _username = HttpContext.Session.GetObjectFromJson<string>("UserName");
            _role = HttpContext.Session.GetObjectFromJson<Role>("Role");
            ViewData["UserId"] = _userId;
            ViewData["UserName"] = _username;
            ViewData["Role"] = _role;

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Username,ImgUrl,Email,Password,UserRole,ValidationCode,EmailConfirmed,DateCreated,DateChanged")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            if (_userId == null) return RedirectToAction("AccessDenied", "Home");
            _username = HttpContext.Session.GetObjectFromJson<string>("UserName");
            _role = HttpContext.Session.GetObjectFromJson<Role>("Role");
            ViewData["UserId"] = _userId;
            ViewData["UserName"] = _username;
            ViewData["Role"] = _role;

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _userId = HttpContext.Session.GetObjectFromJson<string>("UserId");
            if (_userId == null) return RedirectToAction("AccessDenied", "Home");
            _username = HttpContext.Session.GetObjectFromJson<string>("UserName");
            _role = HttpContext.Session.GetObjectFromJson<Role>("Role");
            ViewData["UserId"] = _userId;
            ViewData["UserName"] = _username;
            ViewData["Role"] = _role;

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
