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


            // подключаем MVC к проекту

            string connection = "Data Source=(local)\\SQLEXPRESS; Database=userstore; Persist Security Info=false; User ID='sa'; Password='sa'; MultipleActiveResultSets=True; Trusted_Connection=False;";

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Profile/Account/Login");
                    option.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Profile/Account/Login");
                });


            builder.Services.AddControllersWithViews();

            var app = builder.Build();


            //добавляем паттерн маршрута по-умолчанию

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name:"account_area",
                areaName:"Account",
                pattern: "Profile/{controller=Account}/{action=Login}/{id?}"
                );

            app.MapControllerRoute(
                name: "default",
                pattern:"{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}