using OHS_program_api.Application.Repositories.Definition.LimbRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Definition.LimbRepository
{
    public class LimbWriteRepository : WriteRepository<Limb>, ILimbWriteRepository
    {
        public LimbWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
