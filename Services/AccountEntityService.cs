using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;
using System.Security.Claims;

namespace MyBlog.Services
{
    public class AccountEntityService
    {
        private ApplicationContext _dbcontext;
        private IHttpContextAccessor _httpContextAccessor;
        public AccountEntityService(IHttpContextAccessor httpContextAccessor, ApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> IsUserExists(RegisterModel model)
        {
            User? user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                //await Register(model, user);
                return false;
            }
            return true;
        }

        public async Task Register(RegisterModel model)
        {
            var user = new User { Email = model.Email, Password = model.Password };
            Role? userRole = await _dbcontext.Roles.FirstOrDefaultAsync(r => r.Name == "user");

            if (userRole != null)
                user.Role = userRole;

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            await Authenticate(user);
        }

        public async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}
