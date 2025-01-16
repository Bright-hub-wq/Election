using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.Migrations
{
    public partial class IsResultsReleasedcolumntoelection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultsReleased",
                table: "Elections");

            migrationBuilder.AddColumn<bool>(
                name: "IsResultsReleased",
                table: "Elections",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsResultsReleased",
                table: "Elections");

            migrationBuilder.AddColumn<bool>(
                name: "ResultsReleased",
                table: "Elections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
