using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Persistance.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "team");

            migrationBuilder.CreateTable(
                name: "AssetTypes",
                schema: "team",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "team",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstNameInEnglish = table.Column<string>(nullable: true),
                    FirstNameInRussian = table.Column<string>(nullable: true),
                    FirstNameInUkrainian = table.Column<string>(nullable: true),
                    LastNameInEnglish = table.Column<string>(nullable: true),
                    LastNameInRussian = table.Column<string>(nullable: true),
                    LastNameInUkrainian = table.Column<string>(nullable: true),
                    SecondNameInEnglish = table.Column<string>(nullable: true),
                    SecondNameInRussian = table.Column<string>(nullable: true),
                    SecondNameInUkrainian = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                schema: "team",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AssetTypeEntityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetTypes_AssetTypeEntityId",
                        column: x => x.AssetTypeEntityId,
                        principalSchema: "team",
                        principalTable: "AssetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetTypeProperties",
                schema: "team",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    AssetTypeEntityId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypeProperties", x => new { x.Name, x.AssetTypeEntityId });
                    table.ForeignKey(
                        name: "FK_AssetTypeProperties_AssetTypes_AssetTypeEntityId",
                        column: x => x.AssetTypeEntityId,
                        principalSchema: "team",
                        principalTable: "AssetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactLinks",
                schema: "team",
                columns: table => new
                {
                    Contact = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    MemberId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactLinks", x => new { x.Contact, x.Link });
                    table.UniqueConstraint("AK_ContactLinks_Contact_MemberId", x => new { x.Contact, x.MemberId });
                    table.ForeignKey(
                        name: "FK_ContactLinks_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "team",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                schema: "team",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    MemberId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Phones_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "team",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "team",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    MemberId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => new { x.Name, x.MemberId });
                    table.ForeignKey(
                        name: "FK_Roles_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "team",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetPropertyValues",
                schema: "team",
                columns: table => new
                {
                    AssetEntityId = table.Column<string>(nullable: false),
                    PropertyName = table.Column<string>(nullable: false),
                    PropertyAssetTypeEntityId = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPropertyValues", x => new { x.AssetEntityId, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_AssetPropertyValues_Assets_AssetEntityId",
                        column: x => x.AssetEntityId,
                        principalSchema: "team",
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetPropertyValues_AssetTypeProperties_PropertyName_PropertyAssetTypeEntityId",
                        columns: x => new { x.PropertyName, x.PropertyAssetTypeEntityId },
                        principalSchema: "team",
                        principalTable: "AssetTypeProperties",
                        principalColumns: new[] { "Name", "AssetTypeEntityId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetTypeEntityId",
                schema: "team",
                table: "Assets",
                column: "AssetTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPropertyValues_PropertyName_PropertyAssetTypeEntityId",
                schema: "team",
                table: "AssetPropertyValues",
                columns: new[] { "PropertyName", "PropertyAssetTypeEntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypeProperties_AssetTypeEntityId",
                schema: "team",
                table: "AssetTypeProperties",
                column: "AssetTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactLinks_MemberId",
                schema: "team",
                table: "ContactLinks",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_MemberId",
                schema: "team",
                table: "Phones",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_MemberId",
                schema: "team",
                table: "Roles",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetPropertyValues",
                schema: "team");

            migrationBuilder.DropTable(
                name: "ContactLinks",
                schema: "team");

            migrationBuilder.DropTable(
                name: "Phones",
                schema: "team");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "team");

            migrationBuilder.DropTable(
                name: "Assets",
                schema: "team");

            migrationBuilder.DropTable(
                name: "AssetTypeProperties",
                schema: "team");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "team");

            migrationBuilder.DropTable(
                name: "AssetTypes",
                schema: "team");
        }
    }
}
