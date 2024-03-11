using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.ViewModels.Personnel;
using OHS_program_api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Persistence.Services
{
    public class PersonnelService : IPersonnelService
    {
        readonly IPersonnelReadRepository _personnelReadRepository;
        readonly IPersonnelWriteRepository _personnelWriteRepository;

        public PersonnelService(IPersonnelReadRepository personnelReadRepository, IPersonnelWriteRepository personnelWriteRepository)
        {
            _personnelReadRepository = personnelReadRepository;
            _personnelWriteRepository = personnelWriteRepository;
        }

        public Task<bool> AddPersonnelAsync(VM_Create_Personnel createPersonnel)
        {
            throw new NotImplementedException();
        }

        public Task<List<VM_List_Personnel>> GetAllPersonnelsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VM_List_Personnel> GetOrderByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePersonnelAsync(string id)
        {
            Personnel? personnel = await _personnelReadRepository.GetByIdAsync(id);
            if (personnel != null)
            {
                _personnelWriteRepository.Remove(personnel);
                await _personnelWriteRepository.SaveAsync();
            }
        }

        public Task UpdatePersonnelAsync(VM_Update_Personnel updatePersonnel)
        {
            throw new NotImplementedException();
        }
    }
}
