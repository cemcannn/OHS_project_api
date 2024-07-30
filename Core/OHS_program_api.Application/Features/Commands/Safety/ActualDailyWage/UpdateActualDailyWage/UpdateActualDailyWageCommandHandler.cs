using MediatR;
using OHS_program_api.Application.Repositories.Safety.ActualDailyWageRepository;
using OHS_program_api.Application.Repositories.Safety.ActualWageRepository;

namespace OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.UpdateActualDailyWage
{
    public class UpdateActualDailyWageCommandHandler : IRequestHandler<UpdateActualDailyWageCommandRequest, UpdateActualDailyWageCommandResponse>
    {
        readonly IActualDailyWageWriteRepository _actualDailyWageWriteRepository;
        readonly IActualDailyWageReadRepository _actualDailyWageReadRepository;

        public UpdateActualDailyWageCommandHandler(IActualDailyWageWriteRepository actualDailyWageWriteRepository, IActualDailyWageReadRepository actualDailyWageReadRepository)
        {
            _actualDailyWageWriteRepository = actualDailyWageWriteRepository;
            _actualDailyWageReadRepository = actualDailyWageReadRepository;
        }

        public async Task<UpdateActualDailyWageCommandResponse> Handle(UpdateActualDailyWageCommandRequest request, CancellationToken UpdateActualDailyWageCommandResponse)
        {
            Domain.Entities.Safety.ActualDailyWage? _actualDailyWage = await _actualDailyWageReadRepository.GetByIdAsync(request.Id);
            if (_actualDailyWage != null)
            {
                _actualDailyWage.Id = new Guid(request.Id);
                _actualDailyWage.Month = request.Month;
                _actualDailyWage.Year = request.Year;
                _actualDailyWage.Directorate = request.Directorate;
                _actualDailyWage.ActualWageSurface = request.ActualWageSurface;
                _actualDailyWage.ActualWageUnderground = request.ActualWageUnderground;
                _actualDailyWage.EmployeesNumberSurface = request.EmployeesNumberSurface;
                _actualDailyWage.EmployeesNumberUnderground = request.EmployeesNumberUnderground;

                await _actualDailyWageWriteRepository.SaveAsync();
            }
            return new UpdateActualDailyWageCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
