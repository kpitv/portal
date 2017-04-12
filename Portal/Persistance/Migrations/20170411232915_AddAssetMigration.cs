using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Persistance.Migrations
{
    public partial class AddAssetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Roles",
                newSchema: "team");

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
                name: "AssetPropertyValues",
                schema: "team",
                columns: table => new
                {
                    AssetEntityId = table.Column<string>(nullable: false),
                    AssetTypePropertyEntityId = table.Column<string>(nullable: false),
                    PropertyAssetTypeEntityId = table.Column<string>(nullable: true),
                    PropertyName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPropertyValues", x => new { x.AssetEntityId, x.AssetTypePropertyEntityId });
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetPropertyValues",
                schema: "team");

            migrationBuilder.DropTable(
                name: "Assets",
                schema: "team");

            migrationBuilder.DropTable(
                name: "AssetTypeProperties",
                schema: "team");

            migrationBuilder.DropTable(
                name: "AssetTypes",
                schema: "team");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "team");
        }
    }
}
