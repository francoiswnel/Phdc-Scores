using PhdcScores.Shared.Services.Runners;

namespace PhdcScores.Apps.Console;

public class Worker : BackgroundService
{
	private readonly IRunner _runner;
	private readonly ILogger<Worker> _logger;

	public Worker(IRunner runner, ILogger<Worker> logger)
	{
		_runner = runner;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await _runner.RunAsync(stoppingToken);
		await StopAsync(stoppingToken);
	}
}
