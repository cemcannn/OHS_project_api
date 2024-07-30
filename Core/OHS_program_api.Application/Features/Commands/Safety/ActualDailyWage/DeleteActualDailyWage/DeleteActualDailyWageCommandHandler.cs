using MediatR;
using OHS_program_api.Application.Features.Commands.Definition.Directorate.RemoveDirectorate;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;
using OHS_program_api.Application.Repositories.Safety.ActualDailyWageRepository;

namespace OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.DeleteActualDailyWage
{
    public class DeleteActualDailyWageCommandHandler : IRequestHandler<DeleteActualDailyWageCommandRequest, DeleteActualDailyWageCommandResponse>
    {
        readonly IActualDailyWageWriteRepository _actualDailyWageWriteRepository;

        public DeleteActualDailyWageCommandHandler(IActualDailyWageWriteRepository actualDailyWageWriteRepository)
        {
            _actualDailyWageWriteRepository = actualDailyWageWriteRepository;
        }
        public async Task<DeleteActualDailyWageCommandResponse> Handle(DeleteActualDailyWageCommandRequest request, CancellationToken cancellationToken)
        {
            await _actualDailyWageWriteRepository.RemoveAsync(request.Id);
            await _actualDailyWageWriteRepository.SaveAsync();
            return new();
        }

    }
}
