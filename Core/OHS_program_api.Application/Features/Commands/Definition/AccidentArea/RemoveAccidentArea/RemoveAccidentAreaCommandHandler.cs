using MediatR;
using OHS_program_api.Application.Repositories.Definition.AccidentAreaRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.AccidentArea.RemoveAccidentArea
{
    public class RemoveAccidentAreaCommandHandler : IRequestHandler<RemoveAccidentAreaCommandRequest, RemoveAccidentAreaCommandResponse>
    {
        readonly IAccidentAreaWriteRepository _accidentAreaWriteRepository;

        public RemoveAccidentAreaCommandHandler(IAccidentAreaWriteRepository accidentAreaWriteRepository)
        {
            _accidentAreaWriteRepository = accidentAreaWriteRepository;
        }
        public async Task<RemoveAccidentAreaCommandResponse> Handle(RemoveAccidentAreaCommandRequest request, CancellationToken cancellationToken)
        {
            await _accidentAreaWriteRepository.RemoveAsync(request.Id);
            await _accidentAreaWriteRepository.SaveAsync();
            return new();
        }

    }
}
