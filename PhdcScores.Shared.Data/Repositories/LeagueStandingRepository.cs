using Microsoft.EntityFrameworkCore;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.DataContext;

namespace PhdcScores.Shared.Data.Repositories;

public class LeagueStandingRepository : RepositoryBase<LeagueStanding>
{
	public LeagueStandingRepository(IDataContext dataContext) : base(dataContext)
	{
	}

	protected override DbSet<LeagueStanding> GetDbSet()
	{
		return DataContext.LeagueStandings;
	}
}
