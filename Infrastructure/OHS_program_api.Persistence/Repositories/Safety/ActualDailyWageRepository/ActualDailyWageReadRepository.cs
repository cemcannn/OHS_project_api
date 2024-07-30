using OHS_program_api.Application.Repositories.Safety.ActualWageRepository;
using OHS_program_api.Domain.Entities.Safety;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Safety.ActualDailyWageRepository
{
    public class ActualDailyWageReadRepository : ReadRepository<ActualDailyWage>, IActualDailyWageReadRepository
    {
        public ActualDailyWageReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
