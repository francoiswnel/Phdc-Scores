using PhdcScores.Shared.Common.Constants;
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
			services.AddSingleton<IRunner, FileInputRunner>();
		else
			services.AddSingleton<IRunner, ConsoleInputRunner>();
	}
}
