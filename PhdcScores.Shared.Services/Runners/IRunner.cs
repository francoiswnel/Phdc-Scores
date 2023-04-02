namespace PhdcScores.Shared.Services.Runners;

public interface IRunner
{
	Task RunAsync(CancellationToken cancellationToken);
}
