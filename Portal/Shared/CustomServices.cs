using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Shared;
using Portal.Application.Users;
using Portal.Domain.Users;
using Portal.Persistance.Shared;
using Portal.Persistance.Users;
using System;

namespace Portal.Shared
{
    public static class ServiceCollectionExtentions
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseService>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LocalConnection")));

            services.AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 7;
            })
                .AddEntityFrameworkStores<DatabaseService, Guid>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUsersQueryService, UsersQueryService>();
            services.AddScoped<UserInitializer>();
        }
    }
}
