#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace PhdcScores.Shared.Data.Migrations
{
	/// <inheritdoc />
	public partial class InitialMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				"LeagueStandings",
				table => new
				{
					Id = table.Column<Guid>("TEXT", nullable: false),
					TeamName = table.Column<string>("TEXT", nullable: false),
					Points = table.Column<int>("INTEGER", nullable: false),
					DateCreated = table.Column<DateTimeOffset>("TEXT", nullable: false),
					CreatedBy = table.Column<string>("TEXT", nullable: false),
					DateModified = table.Column<DateTimeOffset>("TEXT", nullable: false),
					ModifiedBy = table.Column<string>("TEXT", nullable: false),
					Deleted = table.Column<bool>("INTEGER", nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_LeagueStandings", x => x.Id); });

			migrationBuilder.CreateTable(
				"MatchScores",
				table => new
				{
					Id = table.Column<Guid>("TEXT", nullable: false),
					HomeTeamName = table.Column<string>("TEXT", nullable: false),
					HomeGoals = table.Column<int>("INTEGER", nullable: false),
					AwayTeamName = table.Column<string>("TEXT", nullable: false),
					AwayGoals = table.Column<int>("INTEGER", nullable: false),
					DateCreated = table.Column<DateTimeOffset>("TEXT", nullable: false),
					CreatedBy = table.Column<string>("TEXT", nullable: false),
					DateModified = table.Column<DateTimeOffset>("TEXT", nullable: false),
					ModifiedBy = table.Column<string>("TEXT", nullable: false),
					Deleted = table.Column<bool>("INTEGER", nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_MatchScores", x => x.Id); });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable("LeagueStandings");

			migrationBuilder.DropTable("MatchScores");
		}
	}
}
