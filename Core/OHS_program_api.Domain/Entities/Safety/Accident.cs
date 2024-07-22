using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;

namespace OHS_program_api.Domain.Entities.OccupationalSafety
{
    public class Accident : BaseEntity
    {
        public Guid PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        public string? TypeOfAccident { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentHour { get; set; }
        public DateTime? OnTheJobDate { get; set; }
        public string? Description { get; set; }
        public string? Limb { get; set; }
        //public string? Reason { get; set; }
        //public string? Site { get; set; }
    }
}


