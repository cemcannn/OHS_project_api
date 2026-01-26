using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Application.Repositories.Safety.AccidentStatisticRepository;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Domain.Entities.Safety;

namespace OHS_program_api.Infrastructure.Services.ExcelImport
{
    /// <summary>
    /// Excel'den okunan verileri database'e aktaran servis
    /// </summary>
    public interface IExcelImportService
    {
        Task ImportAccidentStatisticsAsync(List<AccidentStatisticExcelData> data);
        Task ImportAccidentsAsync(List<AccidentExcelData> data);
    }

    public class ExcelImportService : IExcelImportService
    {
        private readonly IAccidentStatisticWriteRepository _accidentStatisticWriteRepository;
        private readonly IAccidentStatisticReadRepository _accidentStatisticReadRepository;
        private readonly IPersonnelWriteRepository _personnelWriteRepository;
        private readonly IPersonnelReadRepository _personnelReadRepository;
        private readonly IAccidentWriteRepository _accidentWriteRepository;
        private readonly IAccidentReadRepository _accidentReadRepository;

        public ExcelImportService(
            IAccidentStatisticWriteRepository accidentStatisticWriteRepository,
            IAccidentStatisticReadRepository accidentStatisticReadRepository,
            IPersonnelWriteRepository personnelWriteRepository,
            IPersonnelReadRepository personnelReadRepository,
            IAccidentWriteRepository accidentWriteRepository,
            IAccidentReadRepository accidentReadRepository)
        {
            _accidentStatisticWriteRepository = accidentStatisticWriteRepository;
            _accidentStatisticReadRepository = accidentStatisticReadRepository;
            _personnelWriteRepository = personnelWriteRepository;
            _personnelReadRepository = personnelReadRepository;
            _accidentWriteRepository = accidentWriteRepository;
            _accidentReadRepository = accidentReadRepository;
        }

        /// <summary>
        /// Kaza istatistik verilerini database'e aktarır (Veri Yevmiye)
        /// </summary>
        public async Task ImportAccidentStatisticsAsync(List<AccidentStatisticExcelData> data)
        {
            if (data == null || !data.Any())
                return;

            int addedCount = 0;
            int skippedCount = 0;

            foreach (var item in data)
            {
                try
                {
                    // Aynı yıl, ay ve işletme için kayıt var mı kontrol et
                    var exists = await _accidentStatisticReadRepository
                        .GetAll(false)
                        .AnyAsync(x => x.Year == item.Year && 
                                      x.Month == item.Month && 
                                      x.Directorate == item.Directorate);

                    if (!exists)
                    {
                        var entity = new AccidentStatistic
                        {
                            Year = item.Year,
                            Month = item.Month,
                            Directorate = item.Directorate,
                            EmployeesNumberUnderground = item.EmployeesNumberUnderground,
                            EmployeesNumberSurface = item.EmployeesNumberSurface,
                            ActualDailyWageUnderground = item.ActualDailyWageUnderground,
                            ActualDailyWageSurface = item.ActualDailyWageSurface
                        };

                        await _accidentStatisticWriteRepository.AddAsync(entity);
                        addedCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"AccidentStatistic import hatası: {ex.Message}");
                    skippedCount++;
                }
            }

            if (addedCount > 0)
            {
                await _accidentStatisticWriteRepository.SaveAsync();
            }

            Console.WriteLine($"AccidentStatistic Import - Eklenen: {addedCount}, Atlanan: {skippedCount}");
        }

        /// <summary>
        /// Kaza verilerini database'e aktarır (Veri - Kazalar)
        /// </summary>
        public async Task ImportAccidentsAsync(List<AccidentExcelData> data)
        {
            if (data == null || !data.Any())
                return;

            int personnelAdded = 0;
            int personnelSkipped = 0;
            int accidentAdded = 0;
            int accidentSkipped = 0;

            foreach (var item in data)
            {
                try
                {
                    // Personel kontrolü - TKIId'ye göre
                    var personnel = await _personnelReadRepository
                        .GetAll(false)
                        .FirstOrDefaultAsync(p => p.TKIId == item.TKIId);

                    if (personnel == null)
                    {
                        // Yeni personel ekle
                        personnel = new Personnel
                        {
                            TKIId = item.TKIId,
                            Name = item.Name,
                            Surname = item.Surname,
                            Directorate = item.Directorate,
                            BornDate = item.BornDate,
                            Profession = item.Profession
                        };

                        await _personnelWriteRepository.AddAsync(personnel);
                        await _personnelWriteRepository.SaveAsync(); // Hemen kaydet ki Id'yi alalım
                        personnelAdded++;
                    }
                    else
                    {
                        personnelSkipped++;
                    }

                    // Kaza kontrolü - Aynı personele, aynı tarihte-saatte kaza var mı?
                    var accidentExists = await _accidentReadRepository
                        .GetAll(false)
                        .AnyAsync(a => a.PersonnelId == personnel.Id && 
                                      a.AccidentDate == item.AccidentDate &&
                                      a.AccidentHour == item.AccidentHour);

                    if (!accidentExists)
                    {
                        var accident = new Accident
                        {
                            PersonnelId = personnel.Id,
                            TypeOfAccident = item.TypeOfAccident,
                            Limb = item.Limb,
                            AccidentArea = item.AccidentArea,
                            AccidentDate = item.AccidentDate,
                            AccidentHour = item.AccidentHour,
                            LostDayOfWork = item.LostDayOfWork,
                            Description = item.Description
                        };

                        await _accidentWriteRepository.AddAsync(accident);
                        accidentAdded++;
                    }
                    else
                    {
                        accidentSkipped++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Accident import hatası (TKIId: {item.TKIId}): {ex.Message}");
                }
            }

            if (accidentAdded > 0)
            {
                await _accidentWriteRepository.SaveAsync();
            }

            Console.WriteLine($"Personnel Import - Eklenen: {personnelAdded}, Atlanan: {personnelSkipped}");
            Console.WriteLine($"Accident Import - Eklenen: {accidentAdded}, Atlanan: {accidentSkipped}");
        }
    }
}
