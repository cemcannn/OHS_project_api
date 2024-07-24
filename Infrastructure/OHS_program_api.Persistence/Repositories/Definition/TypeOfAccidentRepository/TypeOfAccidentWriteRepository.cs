using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Definition.TypeOfAccidentRepository
{
    public class TypeOfAccidentWriteRepository : WriteRepository<TypeOfAccident>, ITypeOfAccidentWriteRepository
    {
        public TypeOfAccidentWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
