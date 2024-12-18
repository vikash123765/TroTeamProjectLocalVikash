using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MovieShopTrio.Database;
using MovieShopTrio.Services.Interfaces;
using MovieShopTrio.Services;

namespace MovieShopTrio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Get connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
            }

            // Register DbContext with dependency injection
            builder.Services.AddDbContext<MovieDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register application services
            builder.Services.AddScoped<IMovieService, MovieService>();

            // Add IHttpContextAccessor if needed
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
