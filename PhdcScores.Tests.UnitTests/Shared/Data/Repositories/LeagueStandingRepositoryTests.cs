#nullable disable

using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.DataContext;
using PhdcScores.Shared.Data.Repositories;

namespace PhdcScores.Tests.UnitTests.Shared.Data.Repositories;

// TODO: Add tests for retrieving data. Reason: Out of scope.
public class LeagueStandingRepositoryTests : RepositoryTestsBase
{
	private Fixture _fixture;
	private Mock<IDataContext> _mockDataContext;
	private IRepository<LeagueStanding> _leagueStandingRepository;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_mockDataContext = new Mock<IDataContext>(MockBehavior.Strict);

		_leagueStandingRepository = new LeagueStandingRepository(_mockDataContext.Object);
	}

	[Test]
	public void CanPersistLeagueStanding()
	{
		var leagueStandings = _fixture.Build<LeagueStanding>().CreateMany(1).ToList();

		var mockLeagueStandingsDbSet = GetMockDbSet(leagueStandings);
		_mockDataContext.Setup(c => c.LeagueStandings).Returns(mockLeagueStandingsDbSet.Object);
		_mockDataContext.Setup(c => c.SaveChanges()).Returns(1);

		var result = _leagueStandingRepository.Persist(leagueStandings.First());

		result.Should().Be(1);
		mockLeagueStandingsDbSet.Verify(s => s.Add(leagueStandings.First()), Times.Once);
		_mockDataContext.Verify(c => c.LeagueStandings, Times.Once);
		_mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
	}

	[Test]
	public void CanPersistLeagueStandings()
	{
		var leagueStandings = _fixture.Build<LeagueStanding>().CreateMany().ToList();

		var mockLeagueStandingsDbSet = GetMockDbSet(leagueStandings);
		_mockDataContext.Setup(c => c.LeagueStandings).Returns(mockLeagueStandingsDbSet.Object);
		_mockDataContext.Setup(c => c.SaveChanges()).Returns(3);

		var result = _leagueStandingRepository.Persist(leagueStandings);

		result.Should().Be(3);
		mockLeagueStandingsDbSet.Verify(s => s.AddRange(leagueStandings), Times.Once);
		_mockDataContext.Verify(c => c.LeagueStandings, Times.Once);
		_mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
	}
}
