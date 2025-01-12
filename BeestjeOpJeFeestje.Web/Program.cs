using BeestjeOpJeFeestje.Business.Interfaces;
using BeestjeOpJeFeestje.Business.Services;
using BeestjeOpJeFeestje.Business.Services.DiscountRules;
using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Interfaces;
using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Data.Repositories;
using BeestjeOpJeFeestje.Data.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("MyConnection") ?? throw new InvalidOperationException("Connection string 'MyConnection' not found.");
            builder.Services.AddDbContext<BeestjeOpJeFeestjeDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<Customer>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BeestjeOpJeFeestjeDbContext>();
            
            builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
            builder.Services.AddScoped<IAnimalTypeRepository, AnimalTypeRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();
            builder.Services.AddScoped<SignInManager<Customer>, SignInManagerService>();
            builder.Services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();
            builder.Services.AddScoped<IDiscountRule, AnimalTypeDiscountService>();
            builder.Services.AddScoped<IDiscountRule, DuckDiscountService>();
            builder.Services.AddScoped<IDiscountRule, DayOfWeekDiscountService>();
            builder.Services.AddScoped<IDiscountRule, NameLetterDiscountService>();
            builder.Services.AddScoped<IDiscountRule, CustomerCardDiscountService>();
            builder.Services.AddScoped<DiscountService>();
            
            // Add session services.
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<BeestjeOpJeFeestjeDbContext>();
                var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                
                DatabaseSeeder.SeedDatabase(context, userManager, roleManager);
            }

            app.Run();
        }
    }
}
