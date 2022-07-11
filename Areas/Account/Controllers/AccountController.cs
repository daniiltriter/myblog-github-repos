using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using System.Security.Claims;

using MyBlog.Models;
using MyBlog.Services;

namespace MyBlog.Areas.Account.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext _context;
        private AccountEntityService _accountService;
        public AccountController(ApplicationContext context, AccountEntityService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        //=> ModelState.IsValid && !await _accountService.IsUserExists(model) ? RedirectToAction("Profile", "Account") : View(model);
        {
            if (ModelState.IsValid)

                if (!await _accountService.IsUserExists(model))
                {
                    await _accountService.Register(model);
                    return RedirectToAction("Profile", "Account");
                }
                else
                    ModelState.AddModelError("", "Try again!");

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await _context.Users
                    .Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await _accountService.Authenticate(user);

                    return RedirectToAction("Profile", "Account");
                }
                ModelState.AddModelError("", "Try again");
            }
            return View(model);
        }

        [Authorize(Roles = "user, admin")]
        public IActionResult Profile()
        {
            string? role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
            string? name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
            ViewBag.UserRole = role;
            ViewBag.UserName = name;
            return View();
        }
    }
}
