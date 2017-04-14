using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Interfaces;
using Portal.Application.Shared;
using Portal.Persistance.Shared;
using Portal.Presentation.Identity.Data;
using Portal.Presentation.Identity.Users;

namespace Portal.Shared
{
    public static class ServiceCollectionExtentions
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseService>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LocalConnection")));

            #region Identity configuration
            services.AddDbContext<IdentityDatabaseService>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("LocalConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDatabaseService>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(o =>
            {
                // Redirect settings
                o.Cookies.ApplicationCookie.LoginPath = new PathString("/Users/Login");

                // Password settings
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 7;
            });
            services.AddScoped<IdentityManager>();
            services.AddScoped<IEmailService, YandexEmailService>();

            #endregion
        }
    }
}
