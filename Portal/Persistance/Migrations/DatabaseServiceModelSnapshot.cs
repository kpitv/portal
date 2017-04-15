using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Portal.Persistance.Shared;

namespace Portal.Persistance.Migrations
{
    [DbContext(typeof(DatabaseService))]
    partial class DatabaseServiceModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Portal.Persistance.Assets.Entities.AssetEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssetTypeEntityId");

                    b.HasKey("Id");

                    b.HasIndex("AssetTypeEntityId");

                    b.ToTable("Assets");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "Assets");
                });

            modelBuilder.Entity("Portal.Persistance.Assets.Entities.AssetPropertyValueEntity", b =>
                {
                    b.Property<string>("AssetEntityId");

                    b.Property<string>("PropertyName");

                    b.Property<string>("PropertyAssetTypeEntityId");

                    b.Property<string>("Value");

                    b.HasKey("AssetEntityId", "PropertyName");

                    b.HasIndex("PropertyName", "PropertyAssetTypeEntityId");

                    b.ToTable("AssetPropertyValues");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "AssetPropertyValues");
                });

            modelBuilder.Entity("Portal.Persistance.Assets.Entities.AssetTypeEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AssetTypes");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "AssetTypes");
                });

            modelBuilder.Entity("Portal.Persistance.Assets.Entities.AssetTypePropertyEntity", b =>
                {
                    b.Property<string>("Name");

                    b.Property<string>("AssetTypeEntityId");

                    b.HasKey("Name", "AssetTypeEntityId");

                    b.HasIndex("AssetTypeEntityId");

                    b.ToTable("AssetTypeProperties");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "AssetTypeProperties");
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.ContactLinkEntity", b =>
                {
                    b.Property<string>("Contact");

                    b.Property<string>("Link");

                    b.Property<string>("MemberId")
                        .IsRequired();

                    b.HasKey("Contact", "Link");

                    b.HasAlternateKey("Contact", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("ContactLinks");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "ContactLinks");
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.MemberEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("Email");

                    b.Property<string>("FirstNameInEnglish");

                    b.Property<string>("FirstNameInRussian");

                    b.Property<string>("FirstNameInUkrainian");

                    b.Property<string>("LastNameInEnglish");

                    b.Property<string>("LastNameInRussian");

                    b.Property<string>("LastNameInUkrainian");

                    b.Property<string>("SecondNameInEnglish");

                    b.Property<string>("SecondNameInRussian");

                    b.Property<string>("SecondNameInUkrainian");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "Members");
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.PhoneEntity", b =>
                {
                    b.Property<string>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MemberId");

                    b.HasKey("Number");

                    b.HasIndex("MemberId");

                    b.ToTable("Phones");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "Phones");
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.RoleEntity", b =>
                {
                    b.Property<string>("Name");

                    b.Property<string>("MemberId");

                    b.HasKey("Name", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("Roles");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "Roles");
                });

            modelBuilder.Entity("Portal.Persistance.Assets.Entities.AssetEntity", b =>
                {
                    b.HasOne("Portal.Persistance.Assets.Entities.AssetTypeEntity", "AssetType")
                        .WithMany()
                        .HasForeignKey("AssetTypeEntityId");
                });

            modelBuilder.Entity("Portal.Persistance.Assets.Entities.AssetPropertyValueEntity", b =>
                {
                    b.HasOne("Portal.Persistance.Assets.Entities.AssetEntity", "Asset")
                        .WithMany("Values")
                        .HasForeignKey("AssetEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Portal.Persistance.Assets.Entities.AssetTypePropertyEntity", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyName", "PropertyAssetTypeEntityId");
                });

            modelBuilder.Entity("Portal.Persistance.Assets.Entities.AssetTypePropertyEntity", b =>
                {
                    b.HasOne("Portal.Persistance.Assets.Entities.AssetTypeEntity")
                        .WithMany("Properties")
                        .HasForeignKey("AssetTypeEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.ContactLinkEntity", b =>
                {
                    b.HasOne("Portal.Persistance.Members.Entities.MemberEntity", "Member")
                        .WithMany("ContactLinks")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.PhoneEntity", b =>
                {
                    b.HasOne("Portal.Persistance.Members.Entities.MemberEntity", "Member")
                        .WithMany("Phones")
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.RoleEntity", b =>
                {
                    b.HasOne("Portal.Persistance.Members.Entities.MemberEntity", "Member")
                        .WithMany("Roles")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
