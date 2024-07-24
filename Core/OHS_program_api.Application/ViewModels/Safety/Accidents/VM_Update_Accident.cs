namespace OHS_program_api.Application.ViewModels.Safety.Accidents
{
    public class VM_Update_Accident
    {
        public string Id { get; set; }
        public string TypeOfAccident { get; set; }
        public string Limb {  get; set; }
        public string AccidentArea { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentHour { get; set; }
        public DateTime? OnTheJobDate { get; set; }
        public string? Description { get; set; }
    }
}
