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
        private AccountEntityService _accountService;
        public AccountController(AccountEntityService accountService)
        {
            _accountService = accountService;
        }

        #region get requests region

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        #endregion

        #region post requests region

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        //=> ModelState.IsValid && !await _accountService.IsUserExists(model) ? RedirectToAction("Profile", "Account") : View(model);
        {
            if (ModelState.IsValid)

                if (!await _accountService.IsUserExists(model.Email))
                {
                    await _accountService.Register(model.Email, model.Password);
                    return RedirectToAction("Profile", "Account");
                }
                else
                    ModelState.AddModelError("", "Try again!");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)

                if (await _accountService.IsUserExists(model.Email, model.Password))
                {
                    await _accountService.Login(model.Email, model.Password);

                    return RedirectToAction("Profile", "Account");
                }

                ModelState.AddModelError("", "Try again");

            return View(model);
        }
        #endregion

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
