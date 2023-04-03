using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Common.Constants;
using PhdcScores.Shared.Common.Models;
using PhdcScores.Shared.Services.Builders;
using PhdcScores.Shared.Services.Extensions;
using PhdcScores.Shared.Services.Persisters;

namespace PhdcScores.Shared.Services.Runners;

// FWN 2023-04-03: Not very happy with the complexity of this class, but I've run out of the time needed to decompose
// it even further. Next steps would be to extract the input into its own service for better testability.
public abstract class RunnerBase : IRunner
{
	protected readonly IConfiguration Config;
	private readonly IResultsPersister _resultsPersister;
	private readonly IResultsBuilder _resultsBuilder;

	protected RunnerBase(IConfiguration config, IResultsPersister resultsPersister, IResultsBuilder resultsBuilder)
	{
		Config = config;
		_resultsPersister = resultsPersister;
		_resultsBuilder = resultsBuilder;
	}

	public Task RunAsync(CancellationToken cancellationToken)
	{
		Console.WriteLine(ConsoleMessages.ApplicationTitle);
		var matchScores = new List<MatchScore>();
		var league = new SortedDictionary<string, int>();

		GetInput(matchScores, league, cancellationToken);
		PersistResults(matchScores, league);
		OutputResultsToConsole(league);

		return Task.CompletedTask;
	}

	protected abstract void GetInput(
		List<MatchScore> matchScores,
		SortedDictionary<string, int> league,
		CancellationToken cancellationToken);

	protected static void ProcessInput(List<MatchScore> matchScores, SortedDictionary<string, int> league, string input)
	{
		var matchScore = ParseInputToMatchScore(input);

		matchScores.Add(matchScore);
		league.UpdateLeague(matchScore);
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

	private void PersistResults(List<MatchScore> matchScores, SortedDictionary<string, int> league)
	{
		_resultsPersister.Persist(matchScores, league);
	}

	private void OutputResultsToConsole(SortedDictionary<string, int> league)
	{
		Console.WriteLine(ConsoleMessages.OutputHeader);
		Console.Write(_resultsBuilder.Build(league));
	}
}
