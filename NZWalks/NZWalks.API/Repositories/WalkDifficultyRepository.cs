using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkDifficultyRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }
        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDBContext.WalkDifficulties.AddAsync(walkDifficulty);
            await nZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWD = await nZWalksDBContext.WalkDifficulties.FirstOrDefaultAsync(wd => wd.Id == id);
            if (existingWD != null)
            {
                nZWalksDBContext.WalkDifficulties.Remove(existingWD);
                await nZWalksDBContext.SaveChangesAsync();
            }
            return existingWD;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDBContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalksDBContext.WalkDifficulties.FirstOrDefaultAsync(wd => wd.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWdInDB = await nZWalksDBContext.WalkDifficulties.FirstOrDefaultAsync(wd => wd.Id == id);
            if (existingWdInDB != null)
            {
                existingWdInDB.Code = walkDifficulty.Code;
                await nZWalksDBContext.SaveChangesAsync();
            }
            return existingWdInDB;
        }
    }
}
