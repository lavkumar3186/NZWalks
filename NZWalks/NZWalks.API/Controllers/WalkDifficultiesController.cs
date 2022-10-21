using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.DTO.WalkDifficulty;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var walkDifficulties = await walkDifficultyRepository.GetAllAsync();
            var walkDifficultiesDto = mapper.Map<IEnumerable<WalkDifficultyDto>>(walkDifficulties);
            return Ok(walkDifficultiesDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficult = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficult == null)
                return NotFound();
            var walkDifficultyDto = mapper.Map<WalkDifficultyDto>(walkDifficult);
            return Ok(walkDifficultyDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] WalkDifficultyRequest walkDifficultyRequest)
        {
            var walkDifficulty = mapper.Map<WalkDifficulty>(walkDifficultyRequest);
            var savedWalkDifficulty = await walkDifficultyRepository.AddAsync(walkDifficulty);
            var walkDifficultyDto = mapper.Map<WalkDifficultyDto>(savedWalkDifficulty);
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDto.Id }, walkDifficultyDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] WalkDifficultyRequest walkDifficultyRequest)
        {
            //convert to domain
            var walkDifficultyDm = mapper.Map<WalkDifficulty>(walkDifficultyRequest);
            //update
            var updatedWd = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDm);
            if (updatedWd == null)
                return NotFound();
            return Ok(updatedWd);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var deletedWd = await walkDifficultyRepository.DeleteAsync(id);
            if (deletedWd == null)
                return NotFound();
            var walkDifficultyDto = mapper.Map<WalkDifficultyDto>(deletedWd);
            return Ok(walkDifficultyDto);
        }

    }
}
