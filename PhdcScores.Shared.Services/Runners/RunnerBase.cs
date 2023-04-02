using AutoMapper;
using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Common.Constants;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Common.Enums;
using PhdcScores.Shared.Common.Exceptions;
using PhdcScores.Shared.Common.Models;
using PhdcScores.Shared.Data.Repositories;
using MatchScore = PhdcScores.Shared.Common.Entities.MatchScore;

namespace PhdcScores.Shared.Services.Runners;

// FWN 2023-04-02: Not very happy with the complexity of this class, but I've run out of the time needed to decompose
// it even further. Next steps would be to extract the input and persistence methods into their own services,
// and further extract the league building into its own service for testability.
public abstract class RunnerBase : IRunner
{
	// FWN 2023-04-02: These values should ideally be extracted to a configuration file, but it's beyond the scope.
	private const int WinPoints = 3;
	private const int DrawPoints = 1;

	protected readonly IConfiguration Config;
	private readonly IMapper _mapper;
	private readonly IRepository<MatchScore> _matchScoreRepository;
	private readonly IRepository<LeagueStanding> _leagueStandingRepository;

	protected RunnerBase(
		IConfiguration config,
		IMapper mapper,
		IRepository<MatchScore> matchScoreRepository,
		IRepository<LeagueStanding> leagueStandingRepository)
	{
		Config = config;
		_mapper = mapper;
		_matchScoreRepository = matchScoreRepository;
		_leagueStandingRepository = leagueStandingRepository;
	}

	public Task RunAsync(CancellationToken cancellationToken)
	{
		Console.WriteLine(ConsoleMessages.ApplicationTitle);
		var matchScores = new List<Common.Models.MatchScore>();
		var league = new SortedDictionary<string, int>();

		GetInput(matchScores, league, cancellationToken);
		PersistResults(matchScores, league);
		OutputResultsToConsole(league);

		return Task.CompletedTask;
	}

	protected abstract void GetInput(
		List<Common.Models.MatchScore> matchScores,
		SortedDictionary<string, int> league,
		CancellationToken cancellationToken);

	protected static void ProcessInput(
		List<Common.Models.MatchScore> matchScores,
		SortedDictionary<string, int> league,
		string input)
	{
		var matchScore = ParseInputToMatchScore(input);

		matchScores.Add(matchScore);
		UpdateLeague(league, matchScore);
	}

	private static Common.Models.MatchScore ParseInputToMatchScore(string input)
	{
		var teamScores = input.Split(",").Select(CreateTeamScore).ToList();

		return new Common.Models.MatchScore(teamScores);
	}

	private static TeamScore CreateTeamScore(string nameAndGoals)
	{
		var name = nameAndGoals[..^1].Trim();
		var goals = int.Parse(nameAndGoals[^1..]);

		return new TeamScore(name, goals);
	}

	private static void UpdateLeague(SortedDictionary<string, int> league, Common.Models.MatchScore matchScore)
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

	// FWN 2023-04-02: The requirements specify "Persist the results to a db schema". There's some ambiguity around
	// the word results for me. I assume that this refers to the raw input results as mentioned by
	// "The input contains the results of a game". In case it refers to the results of the application processing,
	// I've also persisted the league standings.
	private void PersistResults(List<Common.Models.MatchScore> matchScores, SortedDictionary<string, int> league)
	{
		_matchScoreRepository.Persist(_mapper.Map<IEnumerable<MatchScore>>(matchScores));
		_leagueStandingRepository.Persist(_mapper.Map<IEnumerable<LeagueStanding>>(league));
	}

	// FWN 2023-04-02: The requirements are also slightly ambiguous around the display of tied teams in the log.
	// "If two or more teams have the same number of points then they should have the same ranking and be
	// ordered alphabetically." In other words, for the example input, Portugal and South Africa should have
	// the same ranking of 3, but in the example output they are 3 and 4. I went with the interpretation that
	// matches the example output.
	private static void OutputResultsToConsole(SortedDictionary<string, int> league)
	{
		Console.WriteLine(ConsoleMessages.OutputHeader);

		var position = 1;
		var leagueOrderedByScoreDescending = league.OrderByDescending(l => l.Value);

		foreach (var entry in leagueOrderedByScoreDescending)
		{
			Console.WriteLine($"{position}. {entry.Key}, {entry.Value} pts");
			position++;
		}
	}
}
