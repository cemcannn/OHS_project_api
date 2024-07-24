using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Definition.DirectorateRepository
{
    public class DirectorateWriteRepository : WriteRepository<Directorate>, IDirectorateWriteRepository
    {
        public DirectorateWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
