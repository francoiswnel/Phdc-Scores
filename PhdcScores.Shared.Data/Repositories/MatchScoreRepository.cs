using Microsoft.EntityFrameworkCore;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.DataContext;

namespace PhdcScores.Shared.Data.Repositories;

public class MatchScoreRepository : RepositoryBase<MatchScore>
{
	public MatchScoreRepository(IDataContext dataContext) : base(dataContext)
	{
	}

	protected override DbSet<MatchScore> GetDbSet()
	{
		return DataContext.MatchScores;
	}
}
