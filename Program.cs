using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Services;

namespace MyBookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<MyBookStoreContext>(options =>
			{
				options.UseMySql(
					builder
						.Configuration
						.GetConnectionString("MyBookStoreContext"),
					ServerVersion
						.AutoDetect(
							builder
								.Configuration
								.GetConnectionString("MyBookStoreContext")
						)
				);
			});

            builder.Services.AddScoped<GenreService>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

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
