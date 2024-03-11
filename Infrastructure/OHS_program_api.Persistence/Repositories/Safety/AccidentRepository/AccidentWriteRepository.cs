using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Safety.AccidentRepository
{
    public class AccidentWriteRepository : WriteRepository<Accident>, IAccidentWriteRepository
    {
        public AccidentWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
