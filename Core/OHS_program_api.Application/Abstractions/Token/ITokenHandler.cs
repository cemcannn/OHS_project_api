using OHS_program_api.Application.Abstractions.Storage;
using OHS_program_api.Domain.Entities.Identity;

namespace OHS_program_api.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(int second, AppUser appUser);
        string CreateRefreshToken();
    }
}
