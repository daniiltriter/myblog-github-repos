using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyBlog.Services;
using MyBlog.Models;
using System.Security.Claims;

namespace MyBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // ���������� MVC � �������

            string connection = "Data Source=(local)\\SQLEXPRESS; Database=userstore; Persist Security Info=false; User ID='sa'; Password='sa'; MultipleActiveResultSets=True; Trusted_Connection=False;";

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Profile/Account/Login");
                    option.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Profile/Account/Login");
                });

            builder.Services.AddTransient<ChatsEntityService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            // ������� ��� ������� ��������
            app.MapAreaControllerRoute(
                name:"account_area",
                areaName:"Account",
                pattern: "Profile/{controller=Account}/{action=Login}/{id?}"
                );

            // ������� ��� ������� ����
            app.MapAreaControllerRoute(
                name:"chat_area",
                areaName:"Chat",
                pattern:"Chat/{controller=Messanger}/{action=Messanger}/{id?}"
                );

            // ��������� �������
            app.MapControllerRoute(
                name: "default",
                pattern:"{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}