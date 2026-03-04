using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Domain.Entities.Definitions
{
    public class Profession : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? WorkType { get; set; }
    }
}
