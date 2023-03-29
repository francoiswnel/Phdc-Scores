namespace PhdcScores.Shared.Common.Entities;

public abstract class Entity
{
	public Guid Id { get; set; }

	public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

	public string CreatedBy { get; set; } = "PhdcScores";

	public DateTimeOffset DateModified { get; set; } = DateTimeOffset.UtcNow;

	public string ModifiedBy { get; set; } = "PhdcScores";
}
