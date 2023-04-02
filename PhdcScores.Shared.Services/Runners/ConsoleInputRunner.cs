using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Common.Constants;
using PhdcScores.Shared.Common.Models;

namespace PhdcScores.Shared.Services.Runners;

public class ConsoleInputRunner : RunnerBase
{
	public ConsoleInputRunner(IConfiguration config) : base(config)
	{
	}

	protected override void GetInput(
		CancellationToken cancellationToken,
		List<MatchScore> matchScores,
		SortedDictionary<string, int> league)
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
