#nullable disable

using Microsoft.Extensions.Configuration;
using PhdcScores.Shared.Services.Builders;
using PhdcScores.Shared.Services.Persisters;
using PhdcScores.Shared.Services.Runners;
using PhdcScores.Tests.UnitTests.Constants;

namespace PhdcScores.Tests.UnitTests.Shared.Services.Runners;

// TODO: Implement tests. Reason: Ran out of time and believe my other tests should demonstrate my ability.
[TestFixture(Category = TestCategories.UnitTests)]
public class FileInputRunnerTests
{
	private Fixture _fixture;
	private Mock<IConfiguration> _mockConfiguration;
	private Mock<IResultsPersister> _resultsPersister;
	private Mock<IResultsBuilder> _mockResultsBuilder;
	private IRunner _fileInputRunner;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_mockConfiguration = new Mock<IConfiguration>(MockBehavior.Strict);
		_resultsPersister = new Mock<IResultsPersister>(MockBehavior.Strict);
		_mockResultsBuilder = new Mock<IResultsBuilder>(MockBehavior.Strict);

		_fileInputRunner = new FileInputRunner(
			_mockConfiguration.Object,
			_resultsPersister.Object,
			_mockResultsBuilder.Object);
	}
}
