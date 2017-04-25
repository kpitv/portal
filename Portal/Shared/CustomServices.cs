using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Interfaces;
using Portal.Application.Members.Commands;
using Portal.Application.Members.Commands.Factory;
using Portal.Application.Members.Queries;
using Portal.Application.Shared;
using Portal.Domain.Members;
using Portal.Persistance.Members;
using Portal.Persistance.Shared;
using Portal.Presentation.Identity.Data;
using Portal.Presentation.Identity.Users;
using Portal.Presentation.Identity.Users.Models;
using System.Globalization;

namespace Portal.Shared
{
    public static class ServiceCollectionExtentions
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseService>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LocalConnection")));

            services.AddScoped<IMemberCommands, MemberCommands>();
            services.AddScoped<IMemberFactory, MemberFactory>();
            services.AddScoped<IMemberQueries, MemberQueries>();
            services.AddScoped<IRepository<Member>, MemberRepository>();
            services.AddScoped<IEmailService, YandexEmailService>();

            #region Identity configuration
            services.AddDbContext<IdentityDatabaseService>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("LocalConnection")));

            services.AddIdentity<User, IdentityRole>()
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
                o.Password.RequiredLength = 6;
            });
            services.AddScoped<IdentityManager>();

            #endregion
        }
    }
}
