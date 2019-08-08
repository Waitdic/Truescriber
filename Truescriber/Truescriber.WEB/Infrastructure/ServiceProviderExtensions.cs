using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Truescriber.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Truescriber.BLL.Interfaces;
using Truescriber.BLL.Services.Task;
using Truescriber.BLL.Services.User;
using Truescriber.DAL.EFContext;
using Truescriber.DAL.Entities.Tasks;
using Truescriber.DAL.Interfaces;
using Truescriber.DAL.Repositories;

namespace Truescriber.WEB.Infrastructure
{
    public static class ServiceProviderExtensions
    {
        public static void AddRegistry(this IServiceCollection services, string connection)
        {
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Users/Login");
            services.AddDbContext<TruescriberContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("Truescriber.DAL")));
            services.AddIdentity<User, IdentityRole>(opts =>
                {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<TruescriberContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
        }   

        public static void AddMvcService(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public static void AddInterfaces(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Task>, TaskRepository>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
