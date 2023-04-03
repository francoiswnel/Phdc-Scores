using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Common.Constants;
using PhdcScores.Shared.Common.Exceptions;
using PhdcScores.Shared.Common.Models;
using PhdcScores.Shared.Services.Builders;
using PhdcScores.Shared.Services.Persisters;

namespace PhdcScores.Shared.Services.Runners;

public class FileInputRunner : RunnerBase
{
	private readonly string _path;

	public FileInputRunner(IConfiguration config, IResultsPersister resultsPersister, IResultsBuilder resultsBuilder) :
		base(config, resultsPersister, resultsBuilder)
	{
		_path = Config[Arguments.FileName] ?? throw new InvalidConfigurationException(Arguments.FileName);
	}

	protected override void GetInput(
		List<MatchScore> matchScores,
		SortedDictionary<string, int> league,
		CancellationToken cancellationToken)
	{
		Console.WriteLine($"{ConsoleMessages.FileProcessing} {_path}");

		using var streamReader = new StreamReader(_path);

		do
		{
			var input = streamReader.ReadLine();

			if (string.IsNullOrWhiteSpace(input))
				break;

			ProcessInput(matchScores, league, input);
		} while (!cancellationToken.IsCancellationRequested);
	}
}
