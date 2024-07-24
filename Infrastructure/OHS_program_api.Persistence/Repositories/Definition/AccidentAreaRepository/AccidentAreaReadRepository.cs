using OHS_program_api.Application.Repositories.Definition.AccidentAreaRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Definition.AccidentAreaRepository
{
    public class AccidentAreaReadRepository : ReadRepository<AccidentArea>, IAccidentAreaReadRepository
    {
        public AccidentAreaReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
