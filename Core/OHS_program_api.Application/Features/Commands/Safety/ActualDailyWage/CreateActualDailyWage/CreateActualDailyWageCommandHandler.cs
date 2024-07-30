using MediatR;
using OHS_program_api.Application.Repositories.Safety.ActualDailyWageRepository;

namespace OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.CreateActualDailyWage
{
    public class CreateActualDailyWageCommandHandler : IRequestHandler<CreateActualDailyWageCommandRequest, CreateActualDailyWageCommandResponse>
    {
        readonly IActualDailyWageWriteRepository _actualDailyWageWriteRepository;

        public CreateActualDailyWageCommandHandler(IActualDailyWageWriteRepository actualDailyWageWriteRepository)
        {
            _actualDailyWageWriteRepository = actualDailyWageWriteRepository;
        }

        public async Task<CreateActualDailyWageCommandResponse> Handle(CreateActualDailyWageCommandRequest request, CancellationToken cancellationToken)
        {
            await _actualDailyWageWriteRepository.AddAsync(new()
            {
                Month = request.Month,
                Year = request.Year,
                Directorate = request.Directorate,
                ActualWageSurface = request.ActualWageSurface,
                ActualWageUnderground = request.ActualWageUnderground,
                EmployeesNumberSurface = request.EmployeesNumberSurface,
                EmployeesNumberUnderground = request.EmployeesNumberUnderground,
            });
            await _actualDailyWageWriteRepository.SaveAsync();

            return new CreateActualDailyWageCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
