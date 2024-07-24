using OHS_program_api.Application.Repositories.Definition.AccidentAreaRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Definition.AccidentAreaRepository
{
    public class AccidentAreaWriteRepository : WriteRepository<AccidentArea>, IAccidentAreaWriteRepository
    {
        public AccidentAreaWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
