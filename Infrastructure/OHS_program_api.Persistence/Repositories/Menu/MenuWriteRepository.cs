using OHS_program_api.Application.Repositories;
using OHS_program_api.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Persistence.Repositories.Menu
{
    public class MenuWriteRepository : WriteRepository<Domain.Entities.Menu>, IMenuWriteRepository
    {
        public MenuWriteRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
    }
}
