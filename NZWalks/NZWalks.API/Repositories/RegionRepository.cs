using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _nZWalksDBContext;
        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            this._nZWalksDBContext = nZWalksDBContext;
        }

       
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nZWalksDBContext.Regions.ToListAsync(); 
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await _nZWalksDBContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _nZWalksDBContext.AddAsync(region);
            await _nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _nZWalksDBContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
            {
                return region;
            }

            _nZWalksDBContext.Regions.Remove(region);
            await _nZWalksDBContext.SaveChangesAsync();
            return region;
        }


        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _nZWalksDBContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion == null)
            {
                return existingRegion;
            }

            existingRegion.Area = region.Area;
            existingRegion.Code= region.Code;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Name = region.Name;
            existingRegion.Population = region.Population;

            await _nZWalksDBContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
