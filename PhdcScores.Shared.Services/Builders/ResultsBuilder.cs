using System.Text;

namespace PhdcScores.Shared.Services.Builders;

public class ResultsBuilder : IResultsBuilder
{
	// FWN 2023-04-02: The requirements are also slightly ambiguous around the display of tied teams in the log.
	// "If two or more teams have the same number of points then they should have the same ranking and be
	// ordered alphabetically." In other words, for the example input, Portugal and South Africa should have
	// the same ranking of 3, but in the example output they are 3 and 4. I went with the interpretation that
	// matches the example output.
	public string Build(SortedDictionary<string, int> league)
	{
		var sb = new StringBuilder();

		var position = 1;
		var leagueOrderedByScoreDescending = league.OrderByDescending(l => l.Value);

		foreach (var entry in leagueOrderedByScoreDescending)
		{
			sb.AppendLine($"{position}. {entry.Key}, {entry.Value} pts");
			position++;
		}

		return sb.ToString();
	}
}
