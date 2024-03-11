using OHS_program_api.Application.Abstractions.Services.Authentications;

namespace OHS_program_api.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication
    {
        Task PasswordResetAsnyc(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}
