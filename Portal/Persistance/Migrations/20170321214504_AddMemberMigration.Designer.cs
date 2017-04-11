using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Portal.Persistance.Shared;

namespace Portal.Persistance.Migrations
{
    [DbContext(typeof(DatabaseService))]
    [Migration("20170321214504_AddMemberMigration")]
    partial class AddMemberMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Portal.Persistance.Members.Entities.ContactLink", b =>
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

            modelBuilder.Entity("Portal.Persistance.Members.Entities.Member", b =>
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

            modelBuilder.Entity("Portal.Persistance.Members.Entities.Phone", b =>
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

            modelBuilder.Entity("Portal.Persistance.Members.Entities.Role", b =>
                {
                    b.Property<string>("Name");

                    b.Property<string>("MemberId");

                    b.HasKey("Name", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.ContactLink", b =>
                {
                    b.HasOne("Portal.Persistance.Members.Entities.Member", "Member")
                        .WithMany("ContactLinks")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.Phone", b =>
                {
                    b.HasOne("Portal.Persistance.Members.Entities.Member", "Member")
                        .WithMany("Phones")
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("Portal.Persistance.Members.Entities.Role", b =>
                {
                    b.HasOne("Portal.Persistance.Members.Entities.Member", "Member")
                        .WithMany("Roles")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
