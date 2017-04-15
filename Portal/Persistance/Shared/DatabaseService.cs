using Microsoft.EntityFrameworkCore;
using Portal.Persistance.Assets.Entities;
using Portal.Persistance.Members.Entities;

namespace Portal.Persistance.Shared
{
    public class DatabaseService : DbContext
    {
        #region Members
        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<ContactLinkEntity> ContactLinks { get; set; }
        public DbSet<PhoneEntity> Phones { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        #endregion

        #region Assets
        public DbSet<AssetTypeEntity> AssetTypes { get; set; }
        public DbSet<AssetEntity> Assets { get; set; }
        public DbSet<AssetTypePropertyEntity> AssetTypeProperties { get; set; }
        public DbSet<AssetPropertyValueEntity> AssetPropertyValues { get; set; } 
        #endregion

        public DatabaseService(DbContextOptions<DatabaseService> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Members
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
            builder.Entity<RoleEntity>().ForSqlServerToTable(name: "Roles", schema: "team");
            #endregion

            #region Assets
            builder.Entity<AssetTypeEntity>().ForSqlServerToTable(name: "AssetTypes", schema: "team");

            builder.Entity<AssetEntity>().ForSqlServerToTable(name: "Assets", schema: "team");

            builder.Entity<AssetTypePropertyEntity>()
                .HasKey(p => new { p.Name, p.AssetTypeEntityId });
            builder.Entity<AssetTypePropertyEntity>().ForSqlServerToTable(name: "AssetTypeProperties", schema: "team");

            builder.Entity<AssetPropertyValueEntity>()
                .HasKey(p => new { p.AssetEntityId, p.PropertyName });
            builder.Entity<AssetPropertyValueEntity>().ForSqlServerToTable(name: "AssetPropertyValues", schema: "team"); 
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
