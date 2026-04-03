using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var statistic = await _accidentStatisticWriteRepository.Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id), cancellationToken);
            request.Name = statistic == null ? request.Name : $"{statistic.Year}/{statistic.Month} - {statistic.Directorate}";
            await _accidentStatisticWriteRepository.RemoveAsync(request.Id);
            await _accidentStatisticWriteRepository.SaveAsync();
            return new();
        }

    }
}
