using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WardRobe.Data.Migrations
{
    public partial class trip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Backpack");

            migrationBuilder.DropColumn(
                name: "WardrobeId",
                table: "Backpack");

            migrationBuilder.AddColumn<string>(
                name: "Wardrobe",
                table: "Backpack",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropColumn(
                name: "Wardrobe",
                table: "Backpack");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Backpack",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WardrobeId",
                table: "Backpack",
                nullable: false,
                defaultValue: 0);
        }
    }
}
