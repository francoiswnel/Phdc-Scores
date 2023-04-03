namespace PhdcScores.Shared.Services.Builders;

public interface IResultsBuilder
{
	string Build(SortedDictionary<string, int> league);
}
