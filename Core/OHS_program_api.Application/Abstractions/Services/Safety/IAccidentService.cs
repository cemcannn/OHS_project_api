using OHS_program_api.Application.ViewModels.Safety.Accidents;

namespace OHS_program_api.Application.Abstractions.Services.Safety
{
    public interface IAccidentService
    {
        Task<bool> AddAccidentToPersonnelAsync(VM_Create_Accident createAccident);
        Task UpdateAccidentAsync(VM_Update_Accident accident);
        Task<List<VM_List_Accident>> GetAllAccidentsAsync();
        Task<VM_List_Accident> GetAccidentByIdAsync(string id);
        Task RemoveAccidentAsync(string id);
    }
}
