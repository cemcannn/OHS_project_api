using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Domain.Entities.Trainings;

namespace OHS_program_api.Domain.Entities
{
    public class Personnel : BaseEntity
    {
        public int TRIdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? RetiredId { get; set; }
        public int? InsuranceId { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public Guid? ProfessionId { get; set; }
        public Profession? Profession { get; set; }
        public PlaceEnum? TypeOfPlace { get; set; }
        public int? TKIId { get; set; }
        public Guid? UnitId { get; set; }
        public Unit? Unit { get; set; }
        public Guid? CertificateId { get; set; }
        public ICollection<Certificate>? Certificate { get; set; }
        public Guid? TaskInstructionID { get; set; }
        public ICollection<TaskInstruction>? TaskInstruction { get; set; }
        public Guid? AccidentId { get; set; }
        public ICollection<Accident>? Accident { get; set; }
    }
}
