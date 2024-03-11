using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Domain.Entities.Definitions
{
    public class TypeOfExamination : BaseEntity
    {
        public string Name { get; set; }
        public int Period { get; set; }
    }
}
