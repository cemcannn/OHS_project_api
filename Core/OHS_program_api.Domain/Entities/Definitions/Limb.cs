using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Domain.Entities.Definitions
{
    public class Limb : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

