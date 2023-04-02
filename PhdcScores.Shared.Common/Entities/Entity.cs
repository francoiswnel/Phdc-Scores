using PhdcScores.Shared.Common.Constants;

namespace PhdcScores.Shared.Common.Entities;

public abstract class Entity
{
	public Guid Id { get; set; }

	public DateTimeOffset DateCreated { get; init; } = DateTimeOffset.UtcNow;

	public string CreatedBy { get; init; } = AuditConstants.PhdcScoresSystem;

	public DateTimeOffset DateModified { get; set; } = DateTimeOffset.UtcNow;

	public string ModifiedBy { get; set; } = AuditConstants.PhdcScoresSystem;

	public bool Deleted { get; set; } = false;
}
