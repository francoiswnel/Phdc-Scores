using AutoMapper;
using PhdcScores.Shared.Common.Entities;
using MatchScore = PhdcScores.Shared.Common.Models.MatchScore;

namespace PhdcScores.Shared.Common.Mapping;

public class ModelToEntityMappingProfile : Profile
{
	public ModelToEntityMappingProfile()
	{
		CreateMap<MatchScore, Entities.MatchScore>()
			.ForMember(dst => dst.HomeTeamName, opts => opts.MapFrom(src => src.HomeTeam.Name))
			.ForMember(dst => dst.HomeGoals, opts => opts.MapFrom(src => src.HomeTeam.Goals))
			.ForMember(dst => dst.AwayTeamName, opts => opts.MapFrom(src => src.AwayTeam.Name))
			.ForMember(dst => dst.AwayGoals, opts => opts.MapFrom(src => src.AwayTeam.Goals))
			.ForMember(dst => dst.Id, opts => opts.Ignore())
			.ForMember(dst => dst.DateCreated, opts => opts.Ignore())
			.ForMember(dst => dst.CreatedBy, opts => opts.Ignore())
			.ForMember(dst => dst.DateModified, opts => opts.Ignore())
			.ForMember(dst => dst.ModifiedBy, opts => opts.Ignore())
			.ForMember(dst => dst.Deleted, opts => opts.Ignore());

		CreateMap<KeyValuePair<string, int>, LeagueStanding>()
			.ForMember(dst => dst.TeamName, opts => opts.MapFrom(src => src.Key))
			.ForMember(dst => dst.Points, opts => opts.MapFrom(src => src.Value))
			.ForMember(dst => dst.Id, opts => opts.Ignore())
			.ForMember(dst => dst.DateCreated, opts => opts.Ignore())
			.ForMember(dst => dst.CreatedBy, opts => opts.Ignore())
			.ForMember(dst => dst.DateModified, opts => opts.Ignore())
			.ForMember(dst => dst.ModifiedBy, opts => opts.Ignore())
			.ForMember(dst => dst.Deleted, opts => opts.Ignore());
	}
}
