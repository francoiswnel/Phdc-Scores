using PhdcScores.Shared.Common.Models;

namespace PhdcScores.Shared.Services.Persisters;

public interface IResultsPersister
{
	void Persist(List<MatchScore> matchScores, SortedDictionary<string, int> league);
}
