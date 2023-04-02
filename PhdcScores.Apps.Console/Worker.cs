using PhdcScores.Shared.Services.Runners;

namespace PhdcScores.Apps.Console;

public class Worker : BackgroundService
{
	private readonly IRunner _runner;

	public Worker(IRunner runner)
	{
		_runner = runner;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await _runner.RunAsync(stoppingToken);
		await StopAsync(stoppingToken);
	}
}
