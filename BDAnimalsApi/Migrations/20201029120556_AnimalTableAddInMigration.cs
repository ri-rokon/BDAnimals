using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BDAnimalsApi.Migrations
{
    public partial class AnimalTableAddInMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Kingdom = table.Column<string>(nullable: false),
                    Phylum = table.Column<string>(nullable: false),
                    Order = table.Column<string>(nullable: false),
                    Family = table.Column<string>(nullable: false),
                    Genus = table.Column<string>(nullable: false),
                    ScientificName = table.Column<string>(nullable: false),
                    ScientificClassId = table.Column<int>(nullable: false),
                    Picture = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animal_ScientificClass_ScientificClassId",
                        column: x => x.ScientificClassId,
                        principalTable: "ScientificClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_ScientificClassId",
                table: "Animal",
                column: "ScientificClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animal");
        }
    }
}
