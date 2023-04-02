#nullable disable

using AutoMapper;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Common.Mapping;
using MatchScore = PhdcScores.Shared.Common.Models.MatchScore;

namespace PhdcScores.Tests.UnitTests.Shared.Common.Mapping;

public class ModelToEntityMappingProfileTests
{
	private Fixture _fixture;
	private MapperConfiguration _config;
	private IMapper _mapper;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_config = new MapperConfiguration(cfg => { cfg.AddProfile(new ModelToEntityMappingProfile()); });
		_mapper = _config.CreateMapper();
	}

	[Test]
	public void AssertMappingConfigurationIsValid()
	{
		_config.AssertConfigurationIsValid();
	}

	[Test]
	public void CanMapMatchScoreModelToEntity()
	{
		var model = _fixture.Build<MatchScore>().Create();

		var entity = _mapper.Map<PhdcScores.Shared.Common.Entities.MatchScore>(model);

		entity.Should().NotBeNull();
		entity.HomeTeamName.Should().Be(model.HomeTeam.Name);
		entity.HomeGoals.Should().Be(model.HomeTeam.Goals);
		entity.AwayTeamName.Should().Be(model.AwayTeam.Name);
		entity.AwayGoals.Should().Be(model.AwayTeam.Goals);
	}

	[Test]
	public void CanMapMatchScoreModelsToEntities()
	{
		var models = _fixture.Build<MatchScore>().CreateMany();

		var entities = _mapper.Map<IEnumerable<PhdcScores.Shared.Common.Entities.MatchScore>>(models).ToList();

		entities.Should().NotBeNullOrEmpty();
		entities.Should().HaveCount(3);
	}

	[Test]
	public void CanMapLeagueStandingKeyValuePairToEntity()
	{
		var model = _fixture.Build<KeyValuePair<string, int>>().Create();

		var entity = _mapper.Map<LeagueStanding>(model);

		entity.Should().NotBeNull();
		entity.TeamName.Should().Be(model.Key);
		entity.Points.Should().Be(model.Value);
	}

	[Test]
	public void CanMapLeagueStandingDictionaryToEntities()
	{
		var models = _fixture.Build<KeyValuePair<string, int>>().CreateMany();

		var entities = _mapper.Map<IEnumerable<LeagueStanding>>(models).ToList();

		entities.Should().NotBeNullOrEmpty();
		entities.Should().HaveCount(3);
	}
}
