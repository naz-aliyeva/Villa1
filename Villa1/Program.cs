using Microsoft.EntityFrameworkCore;
using Villa1.Contexts;

namespace Villa1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
            @"Server=DESKTOP-TIBL6BJ;Database=Villa;Trusted_Connection=True;TrustServerCertificate=True"));
            var app = builder.Build();
            app.UseStaticFiles();   
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Best}/{action=Index}/{id?}"
          );
            app.MapControllerRoute(
                name: "default",
                pattern: "{Controller=Home}/{Action=Index}");

            app.Run();
        }
    }
}
