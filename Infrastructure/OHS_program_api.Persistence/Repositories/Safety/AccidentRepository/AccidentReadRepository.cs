using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Repositories.Safety.AccidentRepository
{
    public class AccidentReadRepository : ReadRepository<Accident>, IAccidentReadRepository
    {
        public AccidentReadRepository(OHSProgramAPIDbContext context) : base(context)
        {
        }
        public async Task<Accident> GetAccidentsWithPersonnelsByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query
                .Include(p => p.Personnel) // Include ile Personnel'leri ekliyoruz
                .FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }
    }
}
