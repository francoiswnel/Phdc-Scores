#nullable disable

using Microsoft.EntityFrameworkCore;
using PhdcScores.Shared.Common.Entities;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PhdcScores.Shared.Data.DataContext;

public class DataContext : DbContext, IDataContext
{
	private const string DatabaseName = "PhdcScores.db";
	public DbSet<MatchScore> MatchScores { get; set; }

	public DbSet<LeagueStanding> LeagueStandings { get; set; }

	private string DbPath { get; }

	public DataContext()
	{
		var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		DbPath = Path.Join(path, DatabaseName);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite($"Data Source={DbPath}");
	}
}
