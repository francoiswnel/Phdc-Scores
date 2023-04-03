using PhdcScores.Shared.Common.Enums;
using PhdcScores.Shared.Common.Exceptions;
using PhdcScores.Shared.Common.Models;

namespace PhdcScores.Shared.Services.Extensions;

public static class LeagueExtensions
{
	// FWN 2023-04-02: These values should ideally be extracted to a configuration file, but it's beyond the scope.
	private const int WinPoints = 3;
	private const int DrawPoints = 1;

	public static void UpdateLeague(this SortedDictionary<string, int> league, MatchScore matchScore)
	{
		// FWN 2023-04-02: If the teams are not already in the dictionary, insert initial records for them so that
		// they are accounted for even if they lose all games.
		// Assumption: A loss is always worth 0 points.
		league.TryAdd(matchScore.HomeTeam.Name, 0);
		league.TryAdd(matchScore.AwayTeam.Name, 0);

		switch (matchScore.Result)
		{
			case Result.HomeTeamWin:
				league[matchScore.HomeTeam.Name] += WinPoints;
				break;
			case Result.AwayTeamWin:
				league[matchScore.AwayTeam.Name] += WinPoints;
				break;
			case Result.Draw:
				league[matchScore.HomeTeam.Name] += DrawPoints;
				league[matchScore.AwayTeam.Name] += DrawPoints;
				break;
			default:
				throw new InvalidMatchResultException();
		}
	}
}
