using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Portal.Persistance.Shared;

namespace Portal.Persistance.Migrations
{
    [DbContext(typeof(DatabaseService))]
    [Migration("20170209181231_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Portal.Domain.Members.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasAnnotation("SqlServer:Schema", "team");

                    b.HasAnnotation("SqlServer:TableName", "Members");
                });
        }
    }
}
