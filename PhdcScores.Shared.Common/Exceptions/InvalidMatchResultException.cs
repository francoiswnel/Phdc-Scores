using System.Runtime.Serialization;

namespace PhdcScores.Shared.Common.Exceptions;

[Serializable]
public class InvalidMatchResultException : Exception
{
	public InvalidMatchResultException() : base(
		"Invalid match result provided. Values can only be HomeTeamWin = 0, AwayTeamWin = 1, Draw = 2.")
	{
	}

	protected InvalidMatchResultException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
