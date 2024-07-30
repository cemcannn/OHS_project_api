namespace OHS_program_api.Application.ViewModels.Personnel
{
    public class VM_List_Personnel
    {
        public string Id { get; set; }
        public string? TRIdNumber { get; set; }
        public string? TKIId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? BornDate { get; set; }
        public string? Profession { get; set; }
        public string? Directorate { get; set; }
        public string? AccidentId { get; set; }
        public ICollection<string>? Accident { get; set; }
    }
}
