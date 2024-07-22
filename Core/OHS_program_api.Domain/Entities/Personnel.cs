using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Domain.Entities.Trainings;

namespace OHS_program_api.Domain.Entities
{
    public class Personnel : BaseEntity
    {
        public string TRIdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? RetiredId { get; set; }
        public string? InsuranceId { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public Guid? ProfessionId { get; set; }
        public Profession? Profession { get; set; }
        public PlaceEnum? TypeOfPlace { get; set; }
        public string? TKIId { get; set; }
        public string? Unit { get; set; }
        public Guid? CertificateId { get; set; }
        public ICollection<Certificate>? Certificate { get; set; }
        public Guid? TaskInstructionID { get; set; }
        public ICollection<TaskInstruction>? TaskInstruction { get; set; }
        public Guid? AccidentId { get; set; }
        public ICollection<Accident>? Accident { get; set; }
    }
}
