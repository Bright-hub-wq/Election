using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.Migrations
{
    public partial class AddGenderColumnToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Ballot_BallotId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Candidate_CandidateId",
                table: "Vote");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VoteDate",
                table: "Vote",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "Vote",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BallotId",
                table: "Vote",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Ballot_BallotId",
                table: "Vote",
                column: "BallotId",
                principalTable: "Ballot",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Candidate_CandidateId",
                table: "Vote",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Ballot_BallotId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Candidate_CandidateId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VoteDate",
                table: "Vote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BallotId",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Ballot_BallotId",
                table: "Vote",
                column: "BallotId",
                principalTable: "Ballot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Candidate_CandidateId",
                table: "Vote",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
