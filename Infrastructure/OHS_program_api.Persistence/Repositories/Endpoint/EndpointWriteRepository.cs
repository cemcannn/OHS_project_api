using OHS_program_api.Persistence.Contexts;
using OHS_program_api.Application.Repositories;

namespace OHS_program_api.Persistence.Repositories.Endpoint
{
    public class EndpointWriteRepository : WriteRepository<Domain.Entities.Identity.Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
