using AutoMapper;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.Repositories;

namespace PhdcScores.Shared.Services.Persisters;

public class ResultsPersister : IResultsPersister
{
	private readonly IMapper _mapper;
	private readonly IRepository<MatchScore> _matchScoreRepository;
	private readonly IRepository<LeagueStanding> _leagueStandingRepository;

	public ResultsPersister(
		IMapper mapper,
		IRepository<MatchScore> matchScoreRepository,
		IRepository<LeagueStanding> leagueStandingRepository)
	{
		_mapper = mapper;
		_matchScoreRepository = matchScoreRepository;
		_leagueStandingRepository = leagueStandingRepository;
	}

	// FWN 2023-04-02: The requirements specify "Persist the results to a db schema". There's some ambiguity around
	// the word results for me. I assume that this refers to the raw input results as mentioned by
	// "The input contains the results of a game". In case it refers to the results of the application processing,
	// I've also persisted the league standings.
	public void Persist(List<Common.Models.MatchScore> matchScores, SortedDictionary<string, int> league)
	{
		_matchScoreRepository.Persist(_mapper.Map<IEnumerable<MatchScore>>(matchScores));
		_leagueStandingRepository.Persist(_mapper.Map<IEnumerable<LeagueStanding>>(league));
	}
}
