using MediatR;
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
            await _accidentWriteRepository.RemoveAsync(request.Id);
            await _accidentWriteRepository.SaveAsync();
            return new();
        }
    }
}

