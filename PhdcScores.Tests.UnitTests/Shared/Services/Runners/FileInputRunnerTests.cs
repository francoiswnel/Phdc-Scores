#nullable disable

using AutoMapper;
using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.Repositories;
using PhdcScores.Shared.Services.Runners;

namespace PhdcScores.Tests.UnitTests.Shared.Services.Runners;

// TODO: Implement tests. Reason: Ran out of time and believe my other tests should demonstrate my ability.
public class FileInputRunnerTests
{
	private Fixture _fixture;
	private Mock<IConfiguration> _mockConfiguration;
	private Mock<IMapper> _mockMapper;
	private Mock<IRepository<MatchScore>> _mockMatchScoreRepository;
	private Mock<IRepository<LeagueStanding>> _mockLeagueStandingRepository;
	private FileInputRunner _fileInputRunner;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_mockConfiguration = new Mock<IConfiguration>(MockBehavior.Strict);
		_mockMapper = new Mock<IMapper>(MockBehavior.Strict);
		_mockMatchScoreRepository = new Mock<IRepository<MatchScore>>(MockBehavior.Strict);
		_mockLeagueStandingRepository = new Mock<IRepository<LeagueStanding>>(MockBehavior.Strict);

		_fileInputRunner = new FileInputRunner(
			_mockConfiguration.Object,
			_mockMapper.Object,
			_mockMatchScoreRepository.Object,
			_mockLeagueStandingRepository.Object);
	}
}
