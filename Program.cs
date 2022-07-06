using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = "";

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    option.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern:"{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}