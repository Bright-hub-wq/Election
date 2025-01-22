using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.Migrations
{
    public partial class AddVotesCountedToElection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Elections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalVotes",
                table: "Elections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "VotesCounted",
                table: "Elections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WinnerName",
                table: "Elections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WinnerParty",
                table: "Elections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WinnerPhotoPath",
                table: "Elections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinnerVotes",
                table: "Elections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "TotalVotes",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "VotesCounted",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "WinnerName",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "WinnerParty",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "WinnerPhotoPath",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "WinnerVotes",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "Candidates");
        }
    }
}
