using NZWalks.API.Models.Domain;

namespace NZWalks.API.Reponsitories
{
    public interface IWalkRepository
    {
       Task<Walk> CreateAsync(Walk walk);
    }
}
