using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this._regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await _regionRepository.GetAllAsync();
            //return DTO regions
            var regionsDto = mapper.Map<List<RegionDto>>(regions);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")] //restricting route to get guid only
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _regionRepository.GetRegionAsync(id);
            if (region == null)
                return NotFound();
            var regionsDto = mapper.Map<RegionDto>(region);
            return Ok(regionsDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //commenting it out because using fluent validation
            //if (!await ValidateAddRegionAsync(addRegionRequest))
            //    return BadRequest();

            //convert to region model
            var region = mapper.Map<Region>(addRegionRequest);
            //pass region to save
            var savedRegion = await _regionRepository.AddAsync(region);
            //again map to dto and return
            var regionsDto = mapper.Map<RegionDto>(savedRegion);
            //return regionDto
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionsDto.Id }, regionsDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //check and delete
            var region = await _regionRepository.DeleteAsync(id);
            //if region doesn't exist
            if (region == null)
                return NotFound();
            //map to dto
            var regionDto = mapper.Map<RegionDto>(region);
            //return
            return Ok(regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody]UpdateRegionRequest updateRegionRequest)
        {
            //map from dto to model region
            var region = mapper.Map<Region>(updateRegionRequest);
            //save to db
            var savedRegion = await _regionRepository.UpdateAsync(id, region);
            if (savedRegion == null)
                return NotFound();
            //map back to regiondto
            var regionsDto = mapper.Map<RegionDto>(savedRegion);
            return Ok(regionsDto);
        }

        /// <summary>
        /// This is validation and this is apply on properties and all CRUD opertions. In all Models
        /// </summary>
        /// <param name="addRegionRequest"></param>
        /// <returns></returns>
        private async Task<bool> ValidateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if(addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(AddRegionRequest), "$Add region data required");
                return false;
            }
            if (string.IsNullOrEmpty(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Code), $"{nameof(UpdateRegionRequest.Code)} is required");
                return false;
            }

            return ModelState.ErrorCount > 0 ? false : true;
        }
    }
}
