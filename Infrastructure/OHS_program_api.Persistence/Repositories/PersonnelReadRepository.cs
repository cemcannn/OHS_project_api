using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Persistence.Repositories
{
    public class PersonnelReadRepository : ReadRepository<Personnel>, IPersonnelReadRepository
    {
        public PersonnelReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }

        public async Task<Personnel> GetPersonnelWithAccidentsByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query
                .Include(p => p.Accident) // Include ile Accident'leri ekliyoruz
                .FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }
    }

}
