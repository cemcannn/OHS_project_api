using OHS_program_api.Application.Repositories.Safety.ActualDailyWageRepository;
using OHS_program_api.Domain.Entities.Safety;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Safety.ActualDailyWageRepository
{
    public class ActualDailyWageWriteRepository : WriteRepository<ActualDailyWage>, IActualDailyWageWriteRepository
    {
        public ActualDailyWageWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
