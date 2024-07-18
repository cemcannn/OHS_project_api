using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Abstractions.Services.Safety;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Application.ViewModels.Safety.Accidents;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Persistence.Services.Safety
{
    public class AccidentService : IAccidentService
    {
        readonly IPersonnelReadRepository _personnelReadRepository;
        readonly IPersonnelWriteRepository _personnelWriteRepository;
        readonly IAccidentWriteRepository _accidentWriteRepository;
        readonly IAccidentReadRepository _accidentReadRepository;

        public AccidentService(IPersonnelReadRepository personnelReadRepository,
            IPersonnelWriteRepository personnelWriteRepository,
            IAccidentWriteRepository accidentWriteRepository,
            IAccidentReadRepository accidentReadRepository)
        {
            _personnelReadRepository = personnelReadRepository;
            _personnelWriteRepository = personnelWriteRepository;
            _accidentWriteRepository = accidentWriteRepository;
            _accidentReadRepository = accidentReadRepository;
        }

        public async Task<bool> AddAccidentToPersonnelAsync(VM_Create_Accident createAccident)
        {
            Personnel personnel = await _personnelReadRepository.GetByIdAsync(createAccident.PersonnelId);
            if (personnel == null)
            {
                throw new Exception("Kulllanıcı bulunamadı...");
            }

            //Buraya created Date ile alakalı bir validator koyabilirsin, mesela aynı gün içinde iki kere aynı kazayı girmemesi gibi.

            Accident _accident = new()
            {
                PersonnelId = personnel.Id,
                TypeOfAccident = createAccident.TypeOfAccident,
                Limb = createAccident.Limb,
                AccidentDate = createAccident.AccidentDate,
                AccidentHour = createAccident.AccidentHour,
                OnTheJobDate = createAccident.OnTheJobDate,
                Description = createAccident.Description
            };

            _accident.Personnel = personnel;

            await _accidentWriteRepository.AddAsync(_accident);

            personnel.Accident.Add(_accident);


            // Değişiklikleri kaydedin
            await _accidentWriteRepository.SaveAsync();
            await _personnelWriteRepository.SaveAsync();

            return true;
        }

        public Task<List<VM_List_Accident>> GetAllAccidentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VM_List_Accident> GetOrderByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAccidentAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAccidentAsync(VM_Update_Accident accident)
        {
            Accident? _accident = await _accidentReadRepository.GetByIdAsync(accident.Id);
            if (_accident != null)
            {
                _accident.AccidentDate = accident.AccidentDate;
                _accident.AccidentHour = accident.AccidentHour;
                _accident.TypeOfAccident = accident.TypeOfAccident;
                _accident.Limb = accident.Limb;
                _accident.OnTheJobDate = accident.OnTheJobDate;
                _accident.Description = accident.Description;

                await _accidentWriteRepository.SaveAsync();
            }
        }
    }
}
