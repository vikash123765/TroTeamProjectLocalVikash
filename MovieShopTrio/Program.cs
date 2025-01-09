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
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); ; // Remove session after30 min
                options.Cookie.IsEssential = true; // Ensure session cookies are sent with each request
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidCastException("Default Connection not found");
            
            // Register DbContext with dependency injection
            builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));

            // Register application services
            builder.Services.AddScoped<MovieShopTrio.Services.OrderService>();

            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            //builder.Services.AddScoped<ImageService>(); // Service that updates posters path in database


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
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //// Part of ImageService - when WebShop is loaded, update poster path in database (image for movie)
            //app.Lifetime.ApplicationStarted.Register(async () =>
            //{
            //    using var scope = app.Services.CreateScope();
            //    var tmdbService = scope.ServiceProvider.GetRequiredService<ImageService>();
            //    await tmdbService.UpdatePosterPathsAsync();
            //    Console.WriteLine("Download success.");
            //});

            app.Run();
        }
    }
}
