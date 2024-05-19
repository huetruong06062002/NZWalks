using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Reponsitories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
