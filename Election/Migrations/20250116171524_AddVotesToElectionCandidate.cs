using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.Migrations
{
    public partial class AddVotesToElectionCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteTime",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "HasVoted",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "VoteCount",
                table: "Candidates",
                newName: "Votes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Votes",
                table: "Candidates",
                newName: "VoteCount");

            migrationBuilder.AddColumn<DateTime>(
                name: "VoteTime",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "HasVoted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
