using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext :DbContext
    {
        //ctor
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        //prop

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties
            //Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id =  Guid.Parse("5619c829-cb70-4f87-960d-e4683d48e89b"),
                    Name = "Easy"
                },
                 new Difficulty()
                {
                    Id = Guid.Parse("8b7fbc50-39b2-4b0b-b39b-c8d334accb60"),
                    Name = "Medium"
                },
                  new Difficulty()
                {
                    Id = Guid.Parse("9e752efc-bf7c-45a7-b1ff-9c9d230a9a04"),
                    Name = "Hard"
                }
            };

            //Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("b50c9999-2350-476a-8221-e59fb0496079"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = "https://www.aucklandnz.com/sites/all/themes/custom/aucklandnz/images/auckland-city.jpg"
                },
                 new Region
                {
                    Id = Guid.Parse("7e22f497-a464-48f9-baba-36c8cefacf00"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://www.aucklandnz.com/sites/all/themes/custom/aucklandnz/images/auckland-city.jpg"
                },
                 new Region
                {
                    Id = Guid.Parse("6816f730-820d-43d6-abd0-949b36cea722"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://www.aucklandnz.com/sites/all/themes/custom/aucklandnz/images/auckland-city.jpg"
                },
                  new Region
                {
                    Id = Guid.Parse("b99f4482-fdc7-4cae-be91-6970aacb9f1e"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };
            
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
