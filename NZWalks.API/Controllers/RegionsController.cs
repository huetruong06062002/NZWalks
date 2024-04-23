using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    //https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        //GET: https://localhost:portname/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain model
            var regionsDomain =  await dbContext.Regions.ToListAsync();


            //Map Domain model to DTOs
            var regionsDto = new List<RegionDto>();
            foreach(var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                }); ;
            }

            //return DTOs
            return Ok(regionsDto);
        }

        //GET SINGLE REGION (Get Region By ID)
        //GET: https://localhost:portname/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public  async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Data From Database - Domain model

            //var regions = dbContext.Regions.Find(id);

            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region DTO
            //
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            
            //Return DTO back to client
            return Ok(regionDto);
        }

        //POST To Create New Region
        //POST: https://localhost:portname/api/regions

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var newRegionDomain = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            //Use Domain Model to create Region
            await dbContext.Regions.AddAsync(newRegionDomain);
            await dbContext.SaveChangesAsync();

            //Use Domain Model to create Region
            var regionDto = new RegionDto()
            {
                Id = newRegionDomain.Id,
                Code = newRegionDomain.Code,
                Name = newRegionDomain.Name,
                RegionImageUrl = newRegionDomain.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        //Update region
        //PUT: https://localhost:portname/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequstDto updateRegionRequstDto)
        {
            //Check if region exists
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model
            regionDomainModel.Code = updateRegionRequstDto.Code;
            regionDomainModel.Name = updateRegionRequstDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequstDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            //Convert Domain Model to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }

        //Delete Region
        //DELETE: https://localhost:portname/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region 
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChangesAsync();

            //return delete Region back
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            //map Domain Model to DTO
            return Ok(regionDto);
        }
    }
}
