namespace MyBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // подключаем MVC к проекту
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //добавляем паттерн маршрута по-умолчанию
            app.MapControllerRoute(
                name: "default",
                pattern:"{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}