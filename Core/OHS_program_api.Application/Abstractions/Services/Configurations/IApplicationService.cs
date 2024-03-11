using OHS_program_api.Application.DTOs.Configuration;

namespace OHS_program_api.Application.Abstractions.Services.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
