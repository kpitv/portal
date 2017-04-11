using Microsoft.EntityFrameworkCore;
using Portal.Persistance.Members.Entities;

namespace Portal.Persistance.Shared
{
    public class DatabaseService : DbContext
    {
        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<ContactLinkEntity> ContactLinks { get; set; }
        public DbSet<PhoneEntity> Phones { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public DatabaseService(DbContextOptions<DatabaseService> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MemberEntity>().ForSqlServerToTable(name: "Members", schema: "team");

            builder.Entity<ContactLinkEntity>()
                .HasKey(c => new { c.Contact, c.Link });
            builder.Entity<ContactLinkEntity>()
               .HasAlternateKey(c => new { c.Contact, c.MemberId });
            builder.Entity<ContactLinkEntity>().ForSqlServerToTable(name: "ContactLinks", schema: "team");

            builder.Entity<PhoneEntity>()
                .ForSqlServerToTable(name: "Phones", schema: "team").HasKey(p => p.Number);

            builder.Entity<RoleEntity>()
               .HasKey(r => new { r.Name, r.MemberId });

            base.OnModelCreating(builder);
        }
    }
}
