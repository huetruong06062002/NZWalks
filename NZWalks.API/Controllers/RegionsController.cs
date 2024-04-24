using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Reponsitories;

namespace NZWalks.API.Controllers
{
    //https://localhost:portname/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionReponsitory;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionReponsitory, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionReponsitory = regionReponsitory;
            this.mapper = mapper;
        }

        //GET ALL REGIONS
        //GET: https://localhost:portname/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain model
            var regionsDomain =  await regionReponsitory.GetAllAsync();


            //Map Domain model to DTOs
            //var regionsDto = new List<RegionDto>();
            //foreach(var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    }); ;
            // }

            //Map Domain model to DTOs
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

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

            var regionDomain = await regionReponsitory.GetByIdAsync(id);

            

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region DTO

            
            
            //Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //POST To Create New Region
        //POST: https://localhost:portname/api/regions

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            //Use Domain Model to create Region
            //await dbContext.Regions.AddAsync(newRegionDomain);
            //await dbContext.SaveChangesAsync();


            //Use Domain Model to create Region
            regionDomainModel = await regionReponsitory.CreateAsync(regionDomainModel);

            //Map Domain Model backto to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        //Update region
        //PUT: https://localhost:portname/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequstDto updateRegionRequstDto)
        {
            //Map DTO to Domain Model

            var regionDomainModel = mapper.Map<Region>(updateRegionRequstDto);


            //Check if region exists
            regionDomainModel = await regionReponsitory.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }


            //Convert Domain Model to DTO

            return Ok(mapper.Map<RegionDto>(regionDomainModel));

        }

        //Delete Region
        //DELETE: https://localhost:portname/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        { 
            var regionDomainModel = await regionReponsitory.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //return delete Region back
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            //map Domain Model to DTO
            return Ok(regionDto);
        }
    }
}
