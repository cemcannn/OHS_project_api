using OHS_program_api.Domain.Entities.OccupationalSafety;

namespace OHS_program_api.Application.ViewModels.Personnel
{
    public class VM_Create_Personnel
    {
        public string TRIdNumber { get; set; }
        public string? TKIId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BornDate { get; set; }
        public string? Profession { get; set; }
        public string? Directorate { get; set; }
        public Guid? AccidentId { get; set; }
        public ICollection<Accident>? Accident { get; set; }
    }
}
