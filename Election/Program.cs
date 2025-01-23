//using Election.Db;
//using Election.Helper;
//using Election.IHelper;
//using Election.Models;
//using Election.Services;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace Election
//{
//    public class Program
//    {
//        static void UpdateDatabase(IApplicationBuilder app)
//        {
//            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
//            {
//                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
//                {
//                    //context?.Database.Migrate();
//                    context?.Database.EnsureCreated();

//                }
//            }
//        }



//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container
//            builder.Services.AddControllersWithViews();

//            builder.Services.AddScoped<IUserHelper, UserHelper>();
//            builder.Services.AddScoped<IVoteCountingService, VoteCountingService>();

//            builder.Services.AddDbContext<AppDbContext>(opt =>  opt.UseSqlServer(builder.Configuration.GetConnectionString("ElectionTestDB")));
//            // Register the session services
//            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//            {
//                options.Password.RequireDigit = false;
//                options.Password.RequiredLength = 3;
//                options.Password.RequireLowercase = false;
//                options.Password.RequireNonAlphanumeric = false;
//                options.Password.RequireUppercase = false;
//            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//            builder.Services.AddAuthentication(options =>
//            {
//                options.DefaultScheme = IdentityConstants.ApplicationScheme;
//                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
//            }).AddCookie("Cookies", options =>
//            {
//                options.LoginPath = "/Account/Login";
//                options.AccessDeniedPath = "/Account/AccessDenied";
//            });

//            builder.Services.ConfigureApplicationCookie(options =>
//            {
//                options.LoginPath = "/Account/Login";
//                options.AccessDeniedPath = "/Account/AccessDenied";
//                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//                options.SlidingExpiration = true;
//            });

//            var app = builder.Build();

//            // Configure the HTTP request pipeline
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();
//            UpdateDatabase(app);
//            app.UseAuthentication(); // Add authentication middleware
//            app.UseAuthorization();

//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");

//            app.Run();
//        }
//    }
//}

using Election.Db;
using Election.Helper;
using Election.IHelper;
using Election.Models;
using Election.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Election
{
    public class Program
    {
        static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    //context?.Database.Migrate();
                    context?.Database.EnsureCreated();
                }
            }
        }

        // Ensure both "Admin" and "Voter" roles are created
        static async Task SeedRolesAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Ensure the Admin role exists
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Ensure the Voter role exists
                if (!await roleManager.RoleExistsAsync("Voter"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Voter"));
                }
            }
        }

        // Seed the admin user if it does not already exist
        static async Task SeedAdminUserAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Ensure the Admin role exists
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Seed the admin user
                var adminEmail = "admin@example.com"; // Change to your desired admin email
                var adminPassword = "Admin123!";      // Change to a secure password

                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
                else
                {
                    // If admin already exists but doesn't have the "Admin" role, add it
                    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }

                    // Optional: Ensure admin can reset their password if necessary
                    if (await userManager.CheckPasswordAsync(adminUser, adminPassword) == false)
                    {
                        await userManager.RemovePasswordAsync(adminUser);
                        await userManager.AddPasswordAsync(adminUser, adminPassword);
                    }
                }
            }
        }

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUserHelper, UserHelper>();
            builder.Services.AddScoped<IVoteCountingService, VoteCountingService>();

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ElectionTestDB")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            }).AddCookie("Cookies", options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            UpdateDatabase(app);
            app.UseAuthentication(); // Add authentication middleware
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Seed roles and admin user
            await SeedRolesAsync(app); // Ensure roles are created first
            await SeedAdminUserAsync(app); // Ensure admin user is created

            app.Run();
        }
    }
}

