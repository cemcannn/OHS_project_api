using OHS_program_api.Application.Repositories.Definition.ProfessionRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Definition.ProfessionRepository
{
    public class ProfessionReadRepository : ReadRepository<Profession>, IProfessionReadRepository
    {
        public ProfessionReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
