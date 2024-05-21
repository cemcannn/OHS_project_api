using OHS_program_api.Application.Repositories;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories
{
    public class PersonnelWriteRepository : WriteRepository<Personnel>, IPersonnelWriteRepository
    {
        public PersonnelWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
