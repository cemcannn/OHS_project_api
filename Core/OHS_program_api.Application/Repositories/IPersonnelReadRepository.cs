using OHS_program_api.Domain.Entities;

namespace OHS_program_api.Application.Repositories
{
        public interface IPersonnelReadRepository : IReadRepository<Personnel>
        {
            Task<Personnel> GetPersonnelWithAccidentsByIdAsync(string id, bool tracking = true);
        }
}


