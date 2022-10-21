using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await nZWalksDBContext.Walks
                .Include(w => w.Region)
                .Include(w => w.WalkDifficulty) //this include will fetch navigation properties values
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await nZWalksDBContext.Walks
                .Include(w => w.Region)
                .Include(w => w.WalkDifficulty)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            //assign new walk id
            walk.Id = Guid.NewGuid();
            await nZWalksDBContext.Walks.AddAsync(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id,Walk walk)
        {
            var existingWalk = await nZWalksDBContext.Walks
                .Include(w => w.Region)
                .Include(w => w.WalkDifficulty)
                .FirstOrDefaultAsync(w => w.Id == id);
            if(existingWalk != null)
            {
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;

                await nZWalksDBContext.SaveChangesAsync();
            }

            return existingWalk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await nZWalksDBContext.Walks
                .Include(w => w.Region)
                .Include(w => w.WalkDifficulty)
                .FirstOrDefaultAsync(w => w.Id == id);
            if( existingWalk != null)
            {
                nZWalksDBContext.Walks.Remove(existingWalk);
                await nZWalksDBContext.SaveChangesAsync();
            }
            return existingWalk;
        }
    }
}
