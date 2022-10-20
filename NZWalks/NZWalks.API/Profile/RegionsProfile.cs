
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profile
{
    public class RegionsProfile : AutoMapper.Profile
    {
        public RegionsProfile()
        {
            CreateMap<Region, RegionDto>()
                .ReverseMap(); //it will map the reverse also
            CreateMap<AddRegionRequest, Region>();
            CreateMap<UpdateRegionRequest, Region>();
        }
    }
}
