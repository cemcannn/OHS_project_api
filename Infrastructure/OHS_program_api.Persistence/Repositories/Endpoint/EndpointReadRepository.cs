using OHS_program_api.Persistence.Contexts;
using OHS_program_api.Application.Repositories;

namespace OHS_program_api.Persistence.Repositories.Endpoint
{
    public class EndpointReadRepository : ReadRepository<Domain.Entities.Identity.Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
