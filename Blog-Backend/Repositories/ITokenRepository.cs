using Microsoft.AspNetCore.Identity;

namespace Blog_Backend.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
