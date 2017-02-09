﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Portal.Presentation.Identity.Data
{
    public class IdentityDatabaseService : IdentityDbContext
    {
        public IdentityDatabaseService(DbContextOptions<IdentityDatabaseService> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUser>().ForSqlServerToTable(name: "Users", schema: "identity");
            builder.Entity<IdentityRole>().ForSqlServerToTable(name: "Roles", schema: "identity");
            builder.Entity<IdentityUserRole<string>>().ForSqlServerToTable(name: "UserRoles", schema: "identity");
            builder.Entity<IdentityUserClaim<string>>().ForSqlServerToTable(name: "UserClaims", schema: "identity");
            builder.Entity<IdentityUserLogin<string>>().ForSqlServerToTable(name: "UserLogins", schema: "identity");
            builder.Entity<IdentityUserToken<string>>().ForSqlServerToTable(name: "UserTokens", schema: "identity");
            builder.Entity<IdentityRoleClaim<string>>().ForSqlServerToTable(name: "RoleClaims", schema: "identity");
            base.OnModelCreating(builder);
        }
    }
}
