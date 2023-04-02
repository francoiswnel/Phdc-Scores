using AutoMapper;
using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Common.Constants;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.Repositories;

namespace PhdcScores.Shared.Services.Runners;

public class ConsoleInputRunner : RunnerBase
{
	public ConsoleInputRunner(
		IConfiguration config,
		IMapper mapper,
		IRepository<MatchScore> matchScoreRepository,
		IRepository<LeagueStanding> leagueStandingRepository) : base(
		config,
		mapper,
		matchScoreRepository,
		leagueStandingRepository)
	{
	}

	protected override void GetInput(
		List<Common.Models.MatchScore> matchScores,
		SortedDictionary<string, int> league,
		CancellationToken cancellationToken)
	{
		Console.WriteLine(ConsoleMessages.InputPrompt);

		do
		{
			var input = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(input))
				break;

			ProcessInput(matchScores, league, input);
		} while (!cancellationToken.IsCancellationRequested);
	}
}
