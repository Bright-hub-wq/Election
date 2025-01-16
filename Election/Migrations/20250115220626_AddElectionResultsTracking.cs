using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.Migrations
{
    public partial class AddElectionResultsTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Ballot_BallotId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_BallotId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "BallotId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "HasVoted",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "IsOngoing",
                table: "Vote");

            migrationBuilder.AddColumn<bool>(
                name: "ResultsReleased",
                table: "Elections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Elections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "ElectionCandidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Votes",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ElectionId",
                table: "Vote",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Elections_ElectionId",
                table: "Vote",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Elections_ElectionId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_ElectionId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ResultsReleased",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "Votes",
                table: "ElectionCandidates");

            migrationBuilder.AddColumn<int>(
                name: "BallotId",
                table: "Vote",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasVoted",
                table: "Vote",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOngoing",
                table: "Vote",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Votes",
                table: "Candidates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_BallotId",
                table: "Vote",
                column: "BallotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Ballot_BallotId",
                table: "Vote",
                column: "BallotId",
                principalTable: "Ballot",
                principalColumn: "Id");
        }
    }
}
