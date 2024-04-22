﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            //Get Data From Database - Domain model
            var regionsDomain = dbContext.Regions.ToList();


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
        public IActionResult GetById([FromRoute] Guid id)
        {
            //Get Data From Database - Domain model

            //var regions = dbContext.Regions.Find(id);

            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            

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
    }
}