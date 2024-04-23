using NZWalks.API.Models.Domain;

namespace NZWalks.API.Reponsitories
{
    public interface IRegionRepository
    {
       Task<List<Region>> GetAllAsync();
    }
}
