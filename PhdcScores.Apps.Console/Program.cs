using PhdcScores.Shared.Common.Constants;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Common.Mapping;
using PhdcScores.Shared.Data.DataContext;
using PhdcScores.Shared.Data.Repositories;
using PhdcScores.Shared.Services.Builders;
using PhdcScores.Shared.Services.Runners;

namespace PhdcScores.Apps.Console;

public static class Program
{
	public static void Main(string[] args)
	{
		var host = Host.CreateDefaultBuilder(args)
			.ConfigureServices(services => ConfigureServices(services, args))
			.Build();

		host.Run();
	}

	private static void ConfigureServices(IServiceCollection services, IReadOnlyList<string> args)
	{
		services.AddHostedService<Worker>();

		if (args.Any() && args[0] == $"--{Arguments.FileName}")
			services.AddTransient<IRunner, FileInputRunner>();
		else
			services.AddTransient<IRunner, ConsoleInputRunner>();

		services.AddTransient<IDataContext, DataContext>();
		services.AddTransient<IRepository<MatchScore>, MatchScoreRepository>();
		services.AddTransient<IRepository<LeagueStanding>, LeagueStandingRepository>();

		services.AddTransient<IResultsBuilder, ResultsBuilder>();

		services.AddAutoMapper(typeof(ModelToEntityMappingProfile));
	}
}
