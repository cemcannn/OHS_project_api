using OHS_program_api.Application.Repositories;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Persistence.Repositories
{
    public class PersonnelWriteRepository : WriteRepository<Personnel>, IPersonnelWriteRepository
    {
        public PersonnelWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
