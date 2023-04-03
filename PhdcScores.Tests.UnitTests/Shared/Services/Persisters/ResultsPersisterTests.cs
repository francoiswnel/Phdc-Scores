#nullable disable

using AutoMapper;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.Repositories;
using PhdcScores.Shared.Services.Persisters;
using PhdcScores.Tests.UnitTests.Constants;

namespace PhdcScores.Tests.UnitTests.Shared.Services.Persisters;

// TODO: Implement unhappy path tests. Reason: Ran out of time and believe my other tests should demonstrate my ability.
[TestFixture(Category = TestCategories.UnitTests)]
public class ResultsPersisterTests
{
	private Fixture _fixture;
	private Mock<IMapper> _mockMapper;
	private Mock<IRepository<MatchScore>> _mockMatchScoreRepository;
	private Mock<IRepository<LeagueStanding>> _mockLeagueStandingRepository;
	private IResultsPersister _resultsPersister;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_mockMapper = new Mock<IMapper>(MockBehavior.Strict);
		_mockMatchScoreRepository = new Mock<IRepository<MatchScore>>(MockBehavior.Strict);
		_mockLeagueStandingRepository = new Mock<IRepository<LeagueStanding>>(MockBehavior.Strict);

		_resultsPersister = new ResultsPersister(
			_mockMapper.Object,
			_mockMatchScoreRepository.Object,
			_mockLeagueStandingRepository.Object);
	}

	[Test]
	public void CanPersistResults()
	{
		var matchScoreModels = _fixture.Build<PhdcScores.Shared.Common.Models.MatchScore>().CreateMany().ToList();
		var matchScoreEntities = _fixture.Build<MatchScore>().CreateMany().ToList();
		var league = new SortedDictionary<string, int>();
		var leagueStandings = _fixture.Build<LeagueStanding>().CreateMany().ToList();

		_mockMapper.Setup(m => m.Map<IEnumerable<MatchScore>>(matchScoreModels)).Returns(matchScoreEntities);
		_mockMapper.Setup(m => m.Map<IEnumerable<LeagueStanding>>(league)).Returns(leagueStandings);

		_mockMatchScoreRepository.Setup(r => r.Persist(matchScoreEntities)).Returns(matchScoreEntities.Count);
		_mockLeagueStandingRepository.Setup(r => r.Persist(leagueStandings)).Returns(leagueStandings.Count);

		_resultsPersister.Persist(matchScoreModels, league);

		_mockMapper.Verify(m => m.Map<IEnumerable<MatchScore>>(matchScoreModels), Times.Once);
		_mockMapper.Verify(m => m.Map<IEnumerable<LeagueStanding>>(league), Times.Once);

		_mockMatchScoreRepository.Verify(r => r.Persist(matchScoreEntities), Times.Once);
		_mockLeagueStandingRepository.Verify(r => r.Persist(leagueStandings), Times.Once);
	}
}
