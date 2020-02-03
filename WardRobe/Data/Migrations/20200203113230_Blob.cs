using Microsoft.EntityFrameworkCore.Migrations;

namespace WardRobe.Data.Migrations
{
    public partial class Blob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Wardrobe",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Wardrobe");
        }
    }
}
