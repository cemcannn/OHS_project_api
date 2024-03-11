using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Domain.Entities.Definitions
{
    public class Site : BaseEntity
    {
        public string Name { get; set; }
        public PlaceEnum TypeOfPlace { get; set; }
    }
}
