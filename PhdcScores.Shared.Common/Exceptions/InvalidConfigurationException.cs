using System.Runtime.Serialization;

namespace PhdcScores.Shared.Common.Exceptions;

[Serializable]
public class InvalidConfigurationException : Exception
{
	public InvalidConfigurationException(string configurationName) : base(
		$"Invalid or missing configuration: {configurationName}")
	{
	}

	protected InvalidConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
