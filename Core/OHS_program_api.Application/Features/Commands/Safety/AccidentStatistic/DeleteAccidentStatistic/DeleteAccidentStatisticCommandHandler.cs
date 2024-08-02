using MediatR;
using OHS_program_api.Application.Repositories.Safety.AccidentStatisticRepository;

namespace OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.DeleteAccidentStatistic
{
    public class DeleteAccidentStatisticCommandHandler : IRequestHandler<DeleteAccidentStatisticCommandRequest, DeleteAccidentStatisticCommandResponse>
    {
        readonly IAccidentStatisticWriteRepository _accidentStatisticWriteRepository;

        public DeleteAccidentStatisticCommandHandler(IAccidentStatisticWriteRepository accidentStatisticWriteRepository)
        {
            _accidentStatisticWriteRepository = accidentStatisticWriteRepository;
        }
        public async Task<DeleteAccidentStatisticCommandResponse> Handle(DeleteAccidentStatisticCommandRequest request, CancellationToken cancellationToken)
        {
            await _accidentStatisticWriteRepository.RemoveAsync(request.Id);
            await _accidentStatisticWriteRepository.SaveAsync();
            return new();
        }

    }
}
