using MediatR;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Directorate.RemoveDirectorate
{
    public class RemoveDirectorateCommandHandler : IRequestHandler<RemoveDirectorateCommandRequest, RemoveDirectorateCommandResponse>
    {
        readonly IDirectorateWriteRepository _directorateWriteRepository;

        public RemoveDirectorateCommandHandler(IDirectorateWriteRepository directorateWriteRepository)
        {
            _directorateWriteRepository = directorateWriteRepository;
        }
        public async Task<RemoveDirectorateCommandResponse> Handle(RemoveDirectorateCommandRequest request, CancellationToken cancellationToken)
        {
            await _directorateWriteRepository.RemoveAsync(request.Id);
            await _directorateWriteRepository.SaveAsync();
            return new();
        }

    }
}
