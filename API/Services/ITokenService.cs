using API.Entities.Identity;

namespace API.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}