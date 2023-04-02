using AutoMapper;
using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Common.Constants;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Common.Exceptions;
using PhdcScores.Shared.Data.Repositories;

namespace PhdcScores.Shared.Services.Runners;

public class FileInputRunner : RunnerBase
{
	private readonly string _path;

	public FileInputRunner(
		IConfiguration config,
		IMapper mapper,
		IRepository<MatchScore> matchScoreRepository,
		IRepository<LeagueStanding> leagueStandingRepository) : base(
		config,
		mapper,
		matchScoreRepository,
		leagueStandingRepository)
	{
		_path = Config[Arguments.FileName] ?? throw new InvalidConfigurationException(Arguments.FileName);
	}

	protected override void GetInput(
		List<Common.Models.MatchScore> matchScores,
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
