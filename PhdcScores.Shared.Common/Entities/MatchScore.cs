namespace PhdcScores.Shared.Common.Entities;

public class MatchScore : Entity
{
	public string HomeTeamName { get; set; }

	public int HomeGoals { get; set; }

	public string AwayTeamName { get; set; }

	public int AwayGoals { get; set; }
}
