using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Common.Constants;
using PhdcScores.Shared.Common.Models;
using PhdcScores.Shared.Services.Builders;
using PhdcScores.Shared.Services.Persisters;

namespace PhdcScores.Shared.Services.Runners;

public class ConsoleInputRunner : RunnerBase
{
	public ConsoleInputRunner(IConfiguration config, IResultsPersister resultsPersister, IResultsBuilder resultsBuilder)
		: base(config, resultsPersister, resultsBuilder)
	{
	}

	protected override void GetInput(
		List<MatchScore> matchScores,
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
