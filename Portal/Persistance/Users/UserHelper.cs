using Portal.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Portal.Persistance.Users
{
    static class UserHelper
    {
        public static void Map(ModelBuilder builder)
        {
            builder.Entity<User>().ForSqlServerToTable(name: "Users", schema: "secure");
            builder.Entity<IdentityRole<Guid>>().ForSqlServerToTable(name: "Roles", schema: "secure");
            builder.Entity<IdentityUserRole<Guid>>().ForSqlServerToTable(name: "UserRoles", schema: "secure");
            builder.Entity<IdentityUserClaim<Guid>>().ForSqlServerToTable(name: "UserClaims", schema: "secure");
            builder.Entity<IdentityUserLogin<Guid>>().ForSqlServerToTable(name: "UserLogins", schema: "secure");
            builder.Entity<IdentityUserToken<Guid>>().ForSqlServerToTable(name: "UserTokens", schema: "secure");
            builder.Entity<IdentityRoleClaim<Guid>>().ForSqlServerToTable(name: "RoleClaims", schema: "secure");
        }
    }
}
