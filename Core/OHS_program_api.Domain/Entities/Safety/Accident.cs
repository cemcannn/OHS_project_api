using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Domain.Entities.OccupationalSafety
{
    public class Accident : BaseEntity
    {
        public Guid PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentHour { get; set; }
        public string? AccidentArea { get; set; }
        public string? TypeOfAccident { get; set; }
        public string? Limb { get; set; }
        public string? ReportDay { get; set; }
        public string? Description { get; set; }

    }
}


