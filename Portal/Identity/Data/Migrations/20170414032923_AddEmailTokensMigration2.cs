using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Presentation.Identity.Data.Migrations
{
    public partial class AddEmailTokensMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                schema: "identity",
                table: "EmailTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 17,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                schema: "identity",
                table: "EmailTokens",
                maxLength: 17,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
