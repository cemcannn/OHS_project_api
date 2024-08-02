using OHS_program_api.Application.Repositories.Safety.AccidentStatisticRepository;
using OHS_program_api.Domain.Entities.Safety;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Safety.AccidentStatisticRepository
{
    public class AccidentStatisticWriteRepository : WriteRepository<AccidentStatistic>, IAccidentStatisticWriteRepository
    {
        public AccidentStatisticWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
