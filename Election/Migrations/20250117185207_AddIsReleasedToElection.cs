using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.Migrations
{
    public partial class AddIsReleasedToElection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsResultsReleased",
                table: "Elections",
                newName: "IsResultReleased");

            migrationBuilder.AddColumn<DateTime>(
                name: "VoteTime",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsReleased",
                table: "Elections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteTime",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "IsReleased",
                table: "Elections");

            migrationBuilder.RenameColumn(
                name: "IsResultReleased",
                table: "Elections",
                newName: "IsResultsReleased");
        }
    }
}
