using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Persistence.Repositories.Definition.TypeOfAccidentRepository
{
    public class TypeOfAccidentWriteRepository : WriteRepository<TypeOfAccident>, ITypeOfAccidentWriteRepository
    {
        public TypeOfAccidentWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
