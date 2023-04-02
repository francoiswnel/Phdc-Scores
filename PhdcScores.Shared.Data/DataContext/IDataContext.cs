using Microsoft.EntityFrameworkCore;
using PhdcScores.Shared.Common.Entities;

namespace PhdcScores.Shared.Data.DataContext;

public interface IDataContext
{
	DbSet<MatchScore> MatchScores { get; }

	DbSet<LeagueStanding> LeagueStandings { get; }

	int SaveChanges();
}
