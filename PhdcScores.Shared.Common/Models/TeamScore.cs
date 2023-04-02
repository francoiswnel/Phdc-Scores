namespace PhdcScores.Shared.Common.Models;

public class TeamScore
{
	public string Name { get; }

	public int Goals { get; }

	public TeamScore(string name, int goals)
	{
		Name = name;
		Goals = goals;
	}
}
