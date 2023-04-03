#nullable disable

using PhdcScores.Shared.Common.Models;
using PhdcScores.Shared.Services.Extensions;
using PhdcScores.Tests.UnitTests.Constants;

namespace PhdcScores.Tests.UnitTests.Shared.Services.Extensions;

[TestFixture(Category = TestCategories.UnitTests)]
public class LeagueExtensionsTests
{
	private Fixture _fixture;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
	}

	[Test]
	public void CanUpdateLeagueWithMatchScores()
	{
		var matchScores = _fixture.Build<MatchScore>().CreateMany().ToList();
		var league = new SortedDictionary<string, int>();

		foreach (var matchScore in matchScores)
			league.UpdateLeague(matchScore);

		league.Should().NotBeNullOrEmpty();
		league.Should().HaveCount(6);
	}
}
