using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profile
{
    public class WalkProfile : AutoMapper.Profile
    {
        public WalkProfile()
        {
            //walk mapping
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<WalkDifficulty, WalkDifficultyDto>().ReverseMap();
            CreateMap<AddWalkRequest, Walk>();
            CreateMap<UpdateWalkRequest, Walk>();
        }
    }
}
