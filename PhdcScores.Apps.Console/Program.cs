using PhdcScores.Shared.Common.Enums;
using PhdcScores.Shared.Common.Exceptions;
using PhdcScores.Shared.Common.Models;

namespace PhdcScores.Apps.Console;

internal static class Program
{
	// FWN 2023-04-02: These values should ideally be extracted to a configuration file, but it's beyond the scope.
	private const int WinPoints = 3;
	private const int DrawPoints = 1;

	// TODO: Extract logic to services.
	// TODO: Add unit tests for services.
	public static void Main(string[] args)
	{
		// TODO: Handle filename as input argument.

		var matchScores = new List<MatchScore>();
		var league = new SortedDictionary<string, int>();

		System.Console.WriteLine("PHDC Scores");
		System.Console.WriteLine("Enter match results followed by a blank line:");

		do
		{
			var input = System.Console.ReadLine();

			if (string.IsNullOrWhiteSpace(input))
				break;

			var matchScore = ParseInputToMatchScore(input);

			matchScores.Add(matchScore);
			league.UpdateLeague(matchScore);
		} while (true);

		// TODO: Persist input results and league.

		System.Console.WriteLine("League:");

		var position = 1;
		foreach (var entry in league.OrderByDescending(l => l.Value))
		{
			System.Console.WriteLine($"{position}. {entry.Key}, {entry.Value} pts");
			position++;
		}
	}

	private static void UpdateLeague(this SortedDictionary<string, int> league, MatchScore matchScore)
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

	private static MatchScore ParseInputToMatchScore(string input)
	{
		var teamScores = input.Split(",").Select(CreateTeamScore).ToList();

		return new MatchScore(teamScores);
	}

	private static TeamScore CreateTeamScore(string nameAndGoals)
	{
		var name = nameAndGoals[..^1].Trim();
		var goals = int.Parse(nameAndGoals[^1..]);

		return new TeamScore(name, goals);
	}
}
