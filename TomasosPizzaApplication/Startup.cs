using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.Services;

namespace TomasosPizzaApplication
{
    public class Startup
    {
        IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDishService, DishService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IAdminService, AdminService>();

            services.AddDbContext<TomasosContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("Tomasos")));

            services.AddDbContext<ApplicationDbContext>(
              options => options.UseSqlServer(configuration.GetConnectionString("Tomasos")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseSession();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "Default",
                template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });

            // Creating identity roles and admin account
            // CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await roleManager.RoleExistsAsync("Admin") == false)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));

                var admin = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@mail.com"
                };

                const string adminPassword = "password";
                var result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (await roleManager.RoleExistsAsync("PremiumUser") == false)
            {
                await roleManager.CreateAsync(new IdentityRole("PremiumUser"));
            }

            if (await roleManager.RoleExistsAsync("RegularUser") == false)
            {
                await roleManager.CreateAsync(new IdentityRole("RegularUser"));
            }
        }
    }
}
