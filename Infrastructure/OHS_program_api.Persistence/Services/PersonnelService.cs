using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.ViewModels.Personnel;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Domain.Entities.OccupationalSafety;
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

        public async Task<bool> AddPersonnelAsync(VM_Create_Personnel createPersonnel)
        {
            // Check TRIdNumber exist
            var existingPersonnel = await _personnelReadRepository.GetSingleAsync(p => p.TRIdNumber == createPersonnel.TRIdNumber);
            if (existingPersonnel != null)
            {
                // Hata döndürün
                throw new Exception("Bu TC Kimlik Numarası ile kayıtlı bir personel zaten mevcut.");
            }

            Personnel _personnel = new()
            {
                TRIdNumber = createPersonnel.TRIdNumber,
                Name = createPersonnel.Name,
                Surname = createPersonnel.Surname,
                RetiredId = createPersonnel.RetiredId,
                InsuranceId = createPersonnel.InsuranceId,
                StartDateOfWork = createPersonnel.StartDateOfWork,
                TKIId = createPersonnel.TKIId,
                Unit = createPersonnel.Unit,
            };


            await _personnelWriteRepository.AddAsync(_personnel);

            // Değişiklikleri kaydedin
            await _personnelWriteRepository.SaveAsync();

            return true;
        }

        public Task<List<VM_List_Personnel>> GetAllPersonnelsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VM_List_Personnel> GetPersonnelByIdAsync(string id)
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

        public async Task UpdatePersonnelAsync(VM_Update_Personnel personnel)
        {
            Personnel? _personnel = await _personnelReadRepository.GetByIdAsync(personnel.Id);
            if (_personnel != null)
            {
                _personnel.Id = new Guid(personnel.Id);
                _personnel.TRIdNumber = personnel.TRIdNumber;
                _personnel.Name = personnel.Name;
                _personnel.Surname = personnel.Surname;
                _personnel.RetiredId = personnel.RetiredId;
                _personnel.InsuranceId = personnel.InsuranceId;
                _personnel.StartDateOfWork = personnel.StartDateOfWork;
                _personnel.TKIId = personnel.TKIId;

                await _personnelWriteRepository.SaveAsync();
            }
        }
    }
}
