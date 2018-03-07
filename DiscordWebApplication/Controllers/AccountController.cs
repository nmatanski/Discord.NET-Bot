using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Domain;
using UserService;
using NotificationService;
using UserService.Domain;
using DAL.Database;
using UserService.Domain.Models;
using CommonHelpers.Helpers;
using Microsoft.AspNetCore.Authorization;
using CommonHelpers.Extensions;
using Microsoft.AspNetCore.Http;
using DiscordWebApplication.Extensions;
using DiscordWebApplication.DTOs;

namespace DiscordWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private IUser _userManager = new UserManager();
        //private ILog _logger = Logger.GetInstance;
        private INotificationActor _notificationManager = new NotificationManager();
        private AppDbContext _ctx = new AppDbContext();

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public ActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginEntry entry)
        {
            ViewData["WrongLogin"] = null;
            if (entry.Email == null || entry.Password == null)
            {
                ViewData["WrongLogin"] = "Incorrect form!";
                return View();
            }

            try
            {
                var user = await _userManager.GetUserByEmailAsync(entry.Email);
                if (user != null)
                {
                    if (!user.IsEmailConfirmed)
                    {
                        ViewData["WrongLogin"] = "Email is not confirmed!";
                        return View(entry);
                    }
                    if (!HashUtils.VerifyPassword(entry.Password, user.Password))
                    {
                        /* Don't reveal which one is incorrect.  */
                        ViewData["WrongLogin"] = "Incorrect username or password!";
                        return View(entry);

                    }
                }
                
                HttpContext.Session.SetObjectAsJson<string>("UserId", user.Id);
                HttpContext.Session.SetObjectAsJson<string>("UserName", user.Username);
                HttpContext.Session.SetObjectAsJson("TypeOfUser", user.UserRole);
            }
            catch (Exception ex)
            {
                //await _logger.LogCustomExceptionAsync(ex, null);
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterEntry entry)
        {
            if (!entry.Email.Contains("@gmail.com"))
            {
                ViewData["WrongRegister"] = "Only gmail.com accounts are allowed!";
                return View(entry);
            }

            try
            {
                User user = await _userManager.GetUserByEmailAsync(entry.Email);
                if (user != null)
                {
                    ViewData["WrongRegister"] = "Email already taken!";
                    return View(entry);
                }
                string password = HashUtils.CreateHashCode(entry.Password);
                string validationCode = HashUtils.CreateReferralCode();
                User newUser = new User(entry.Username, entry.Email, password, Role.User, validationCode, false);

                await _userManager.RegisterAsync(newUser);
                await _notificationManager.SendConfirmationEmailAsync(newUser);
            }
            catch (Exception ex)
            {
                //await _logger.LogCustomExceptionAsync(ex, null);
                return RedirectToAction("Error", "Home");
            }

            return View("Welcome");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ValidateEmail(string userId, string validationCode)
        {
            if (userId == null || validationCode == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            try
            {
                User user = await _userManager.GetUserByIdAsync(userId);
                if (user == null || validationCode != user.ValidationCode)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                user = UpdateUserEmailConfirmation(user);
                _ctx.Update(user);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //await _logger.LogCustomExceptionAsync(ex, null);
                return RedirectToAction("Error", "Home");
            }
            return View("ConfirmEmail");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.SetObjectAsJson<string>("UserId", null);
            HttpContext.Session.SetObjectAsJson<string>("UserName", null);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        #region PrivateMethods

        private User UpdateUserEmailConfirmation(User user)
        {
            var updatedUser = new User(
                user.Username,
                user.Email,
                user.Password,
                user.UserRole,
                user.ValidationCode,
                true,
                new CustomId(new Guid(user.Id)),
                user.ImgUrl,
                DateTime.Now
            );
            return updatedUser;
        }

        #endregion
    }
}