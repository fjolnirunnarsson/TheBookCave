using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TheBookCave.Data;
using TheBookCave.Models;
using Microsoft.AspNetCore.Http;
using TheBookCave.Repositories;
using TheBookCave.Models.ViewModels;
using System.Threading.Tasks;

namespace TheBookCave
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider) 
        { 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(); 
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>(); 
 
            IdentityResult roleResult; 
            //Adding Admin Role 
            var roleCheck = await RoleManager.RoleExistsAsync("Admin"); 
            if (!roleCheck) 
            { 
                //create the roles and seed them to the database 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); 
            } 
            //Assign Admin role to the main User here we have given our newly registered  
            //login id for Admin management 
            ApplicationUser user = await UserManager.FindByEmailAsync("stefansdottir8@gmail.com"); 
            var User = new ApplicationUser();  
            await UserManager.AddToRoleAsync(user, "Admin"); 
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthenticationConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(config => {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            });

            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(3);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddDistributedMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped(s => ShoppingCart.GetCart(s));

            services.AddMvc();

            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateUserRoles(services).Wait();
        }
    }
}
