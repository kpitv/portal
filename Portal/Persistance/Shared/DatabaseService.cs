using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.Users;
using Portal.Persistance.Users;
using System;

namespace Portal.Persistance.Shared
{
    public class DatabaseService : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Member> Members { get; set; }

        public DatabaseService(DbContextOptions<DatabaseService> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ForSqlServerUseSequenceHiLo();
            UserHelper.Map(builder);
            base.OnModelCreating(builder);
        }
    }
}
