using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Truescriber.WEB.Models;
using Truescriber.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Truescriber.WEB.Infrastructure
{
    public static class ServiceProviderExtensions
    {
        public static void AddServices(this IServiceCollection services, string connection)
        {
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Users/Login");

            services.AddDbContext<TruescriberContext>(options => options.UseSqlServer(connection));
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}
