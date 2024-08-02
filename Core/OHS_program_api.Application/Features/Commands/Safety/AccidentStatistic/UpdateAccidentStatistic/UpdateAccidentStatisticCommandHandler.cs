using MediatR;
using OHS_program_api.Application.Repositories.Safety.AccidentStatisticRepository;

namespace OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.UpdateAccidentStatistic
{
    public class UpdateAccidentStatisticCommandHandler : IRequestHandler<UpdateAccidentStatisticCommandRequest, UpdateAccidentStatisticCommandResponse>
    {
        readonly IAccidentStatisticWriteRepository _accidentStatisticWriteRepository;
        readonly IAccidentStatisticReadRepository _accidentStatisticReadRepository;

        public UpdateAccidentStatisticCommandHandler(IAccidentStatisticWriteRepository accidentStatisticWriteRepository, IAccidentStatisticReadRepository accidentStatisticReadRepository)
        {
            _accidentStatisticWriteRepository = accidentStatisticWriteRepository;
            _accidentStatisticReadRepository = accidentStatisticReadRepository;
        }

        public async Task<UpdateAccidentStatisticCommandResponse> Handle(UpdateAccidentStatisticCommandRequest request, CancellationToken UpdateActualDailyWageCommandResponse)
        {
            Domain.Entities.Safety.AccidentStatistic? _accidentStatistic = await _accidentStatisticReadRepository.GetByIdAsync(request.Id);
            if (_accidentStatistic != null)
            {
                _accidentStatistic.Id = new Guid(request.Id);
                _accidentStatistic.Month = request.Month;
                _accidentStatistic.Year = request.Year;
                _accidentStatistic.Directorate = request.Directorate;
                _accidentStatistic.ActualDailyWageSurface = request.ActualDailyWageSurface;
                _accidentStatistic.ActualDailyWageUnderground = request.ActualDailyWageUnderground;
                _accidentStatistic.EmployeesNumberSurface = request.EmployeesNumberSurface;
                _accidentStatistic.EmployeesNumberUnderground = request.EmployeesNumberUnderground;

                await _accidentStatisticWriteRepository.SaveAsync();
            }
            return new UpdateAccidentStatisticCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
