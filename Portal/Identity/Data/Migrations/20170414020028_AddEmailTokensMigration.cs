using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Presentation.Identity.Data.Migrations
{
    public partial class AddEmailTokensMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTokens",
                schema: "identity",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    TokenHash = table.Column<string>(maxLength: 17, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTokens", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTokens",
                schema: "identity");
        }
    }
}
