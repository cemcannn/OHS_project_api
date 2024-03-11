using OHS_program_api.Application.Repositories;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Menu
{
    public class MenuReadRepository : ReadRepository<Domain.Entities.Menu>, IMenuReadRepository
    {
        public MenuReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
