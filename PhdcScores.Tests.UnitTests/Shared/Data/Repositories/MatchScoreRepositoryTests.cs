#nullable disable

using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.DataContext;
using PhdcScores.Shared.Data.Repositories;
using PhdcScores.Tests.UnitTests.Constants;

namespace PhdcScores.Tests.UnitTests.Shared.Data.Repositories;

// TODO: Add tests for retrieving data. Reason: Out of scope.
[TestFixture(Category = TestCategories.UnitTests)]
public class MatchScoreRepositoryTests : RepositoryTestsBase
{
	private Fixture _fixture;
	private Mock<IDataContext> _mockDataContext;
	private IRepository<MatchScore> _matchScoreRepository;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_mockDataContext = new Mock<IDataContext>(MockBehavior.Strict);

		_matchScoreRepository = new MatchScoreRepository(_mockDataContext.Object);
	}

	[Test]
	public void CanPersistMatchScore()
	{
		var matchScores = _fixture.Build<MatchScore>().CreateMany(1).ToList();

		var mockMatchScoresDbSet = GetMockDbSet(matchScores);
		_mockDataContext.Setup(c => c.MatchScores).Returns(mockMatchScoresDbSet.Object);
		_mockDataContext.Setup(c => c.SaveChanges()).Returns(1);

		var result = _matchScoreRepository.Persist(matchScores.First());

		result.Should().Be(1);
		mockMatchScoresDbSet.Verify(s => s.Add(matchScores.First()), Times.Once);
		_mockDataContext.Verify(c => c.MatchScores, Times.Once);
		_mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
	}

	[Test]
	public void CanPersistMatchScores()
	{
		var matchScores = _fixture.Build<MatchScore>().CreateMany().ToList();

		var mockMatchScoresDbSet = GetMockDbSet(matchScores);
		_mockDataContext.Setup(c => c.MatchScores).Returns(mockMatchScoresDbSet.Object);
		_mockDataContext.Setup(c => c.SaveChanges()).Returns(3);

		var result = _matchScoreRepository.Persist(matchScores);

		result.Should().Be(3);
		mockMatchScoresDbSet.Verify(s => s.AddRange(matchScores), Times.Once);
		_mockDataContext.Verify(c => c.MatchScores, Times.Once);
		_mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
	}
}
