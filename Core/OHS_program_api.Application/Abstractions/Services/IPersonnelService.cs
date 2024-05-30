using OHS_program_api.Application.ViewModels.Personnel;
using OHS_program_api.Application.ViewModels.Safety.Accidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Abstractions.Services
{
    public interface IPersonnelService
    {
        Task<bool> AddPersonnelAsync(VM_Create_Personnel createPersonnel);
        Task UpdatePersonnelAsync(VM_Update_Personnel personnel);
        Task<List<VM_List_Personnel>> GetAllPersonnelsAsync();
        Task<VM_List_Personnel> GetPersonnelByIdAsync(string id);
        Task RemovePersonnelAsync(string id);
    }
}
