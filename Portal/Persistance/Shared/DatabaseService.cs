using Microsoft.EntityFrameworkCore;
using Portal.Domain.Members;

namespace Portal.Persistance.Shared
{
    public class DatabaseService : DbContext
    {
        public DbSet<Member> Members { get; set; }

        public DatabaseService(DbContextOptions<DatabaseService> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ForSqlServerUseSequenceHiLo();
            builder.Entity<Member>().ForSqlServerToTable(name: "Members", schema: "team");
            base.OnModelCreating(builder);
        }
    }
}
