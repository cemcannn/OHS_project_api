using OHS_program_api.Application.Repositories.Definition.UnitRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Definition.UnitRepository
{
    public class UnitReadRepository : ReadRepository<Unit>, IUnitReadRepository
    {
        public UnitReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
