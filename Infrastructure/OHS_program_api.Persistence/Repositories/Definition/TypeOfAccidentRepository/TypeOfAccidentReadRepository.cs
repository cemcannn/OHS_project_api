using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Definition.TypeOfAccidentRepository
{
    public class TypeOfAccidentReadRepository : ReadRepository<TypeOfAccident>, ITypeOfAccidentReadRepository
    {
        public TypeOfAccidentReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
