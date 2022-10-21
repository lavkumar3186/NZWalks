using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await walkRepository.GetAllAsync();
            var walksDto = mapper.Map<IEnumerable<WalkDto>>(walks);
            return Ok(walksDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walkRepository.GetAsync(id);
            if (walk == null)
                return NotFound();
            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody]AddWalkRequest addWalkRequest)
        {
            //convert domain from addrequest
            var walkDomain = mapper.Map<Walk>(addWalkRequest);
            //call add method
            var walk = await walkRepository.AddAsync(walkDomain);
            //convert back to dto
            var walkDto = mapper.Map<WalkDto>(walk);
            //return
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDto.Id }, walkDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute]Guid id, [FromBody]UpdateWalkRequest updateWalkRequest)
        {
            //convert updaterequest to domain walk
            var walk = mapper.Map<Walk>(updateWalkRequest);
            //update walk
            var updatedWalk = await walkRepository.UpdateAsync(id, walk);
            if (updatedWalk == null)
                return NotFound();
            //convert back to walkdto
            var walkDto = mapper.Map<WalkDto>(updatedWalk);
            //return dto
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //delete
            var deleteWalk = await walkRepository.DeleteAsync(id);
            //check null
            if (deleteWalk == null)
                return NotFound();
            //convert to dto
            var walkDto = mapper.Map<WalkDto>(deleteWalk);
            //retun dto
            return Ok(walkDto);
        }
    }
}
