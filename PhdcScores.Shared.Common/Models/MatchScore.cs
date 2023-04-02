using PhdcScores.Shared.Common.Enums;

namespace PhdcScores.Shared.Common.Models;

public class MatchScore
{
	public TeamScore HomeTeam { get; }

	public TeamScore AwayTeam { get; }

	public MatchScore(IReadOnlyList<TeamScore> teamScores)
	{
		HomeTeam = teamScores[0];
		AwayTeam = teamScores[1];
	}

	// FWN 2023-04-02: Normally not a big fan of putting logic in models, but in this case I felt a calculated property
	// is a good fit since the properties it depends on are init only and the calculation should rarely, if ever,
	// need to be updated.
	public Result Result =>
		HomeTeam.Goals > AwayTeam.Goals
			? Result.HomeTeamWin
			: HomeTeam.Goals < AwayTeam.Goals
				? Result.AwayTeamWin
				: Result.Draw;
}
