using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;

namespace OHS_program_api.Application.Features.Commands.Safety.Accident.DeleteAccident
{
    public class DeleteAccidentcommandHandler : IRequestHandler<DeleteAccidentCommandRequest, DeleteAccidentCommandResponse>
    {
        readonly IAccidentWriteRepository _accidentWriteRepository;

        public DeleteAccidentcommandHandler(IAccidentWriteRepository accidentWriteRepository)
        {
            _accidentWriteRepository = accidentWriteRepository;
        }

        public async Task<DeleteAccidentCommandResponse> Handle(DeleteAccidentCommandRequest request, CancellationToken cancellationToken)
        {
            var accident = await _accidentWriteRepository.Table
                .Include(x => x.Personnel)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id), cancellationToken);

            request.Name = accident == null
                ? request.Name
                : $"{accident.Personnel?.Name} {accident.Personnel?.Surname} - {accident.AccidentDate:dd.MM.yyyy}".Trim();

            await _accidentWriteRepository.RemoveAsync(request.Id);
            await _accidentWriteRepository.SaveAsync();
            return new();
        }
    }
}

