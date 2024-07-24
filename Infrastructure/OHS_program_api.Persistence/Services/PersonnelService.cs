using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.ViewModels.Personnel;
using OHS_program_api.Domain.Entities;

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
                TKIId = createPersonnel.TKIId,
                Name = createPersonnel.Name,
                Surname = createPersonnel.Surname,
                StartDateOfWork = createPersonnel.StartDateOfWork,
                Profession = createPersonnel.Profession,
                Directorate = createPersonnel.Directorate
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
                _personnel.TKIId = personnel.TKIId;
                _personnel.Name = personnel.Name;
                _personnel.Surname = personnel.Surname;
                _personnel.StartDateOfWork = personnel.StartDateOfWork;
                _personnel.Profession = personnel.Profession;
                _personnel.Directorate = personnel.Directorate;


                await _personnelWriteRepository.SaveAsync();
            }
        }
    }
}
