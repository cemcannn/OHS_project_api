using MediatR;
using OHS_program_api.Application.Repositories.Safety.AccidentStatisticRepository;

namespace OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.CreateAccidentStatistic
{
    public class CreateAccidentStatisticCommandHandler : IRequestHandler<CreateAccidentStatisticCommandRequest, CreateAccidentStatisticCommandResponse>
    {
        readonly IAccidentStatisticWriteRepository _accidentStatisticWriteRepository;

        public CreateAccidentStatisticCommandHandler(IAccidentStatisticWriteRepository accidentStatisticWriteRepository)
        {
            _accidentStatisticWriteRepository = accidentStatisticWriteRepository;
        }

        public async Task<CreateAccidentStatisticCommandResponse> Handle(CreateAccidentStatisticCommandRequest request, CancellationToken cancellationToken)
        {
            await _accidentStatisticWriteRepository.AddAsync(new()
            {
                Month = request.Month,
                Year = request.Year,
                Directorate = request.Directorate,
                ActualDailyWageSurface = request.ActualDailyWageSurface,
                ActualDailyWageUnderground = request.ActualDailyWageUnderground,
                EmployeesNumberSurface = request.EmployeesNumberSurface,
                EmployeesNumberUnderground = request.EmployeesNumberUnderground,
            });
            await _accidentStatisticWriteRepository.SaveAsync();

            return new CreateAccidentStatisticCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
