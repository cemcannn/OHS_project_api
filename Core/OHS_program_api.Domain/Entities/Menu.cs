using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Identity;

namespace OHS_program_api.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
