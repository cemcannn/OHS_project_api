using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;
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
            var directorate = await _directorateWriteRepository.Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));
            request.Name = directorate?.Name ?? request.Name;
            await _directorateWriteRepository.RemoveAsync(request.Id);
            await _directorateWriteRepository.SaveAsync();
            return new();
        }

    }
}
