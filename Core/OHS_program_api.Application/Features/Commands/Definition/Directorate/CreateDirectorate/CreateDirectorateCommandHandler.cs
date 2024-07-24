using MediatR;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Directorate.CreateDirectorate
{
    internal class CreateDirectorateCommandHandler : IRequestHandler<CreateDirectorateCommandRequest, CreateDirectorateCommandResponse>
    {
        readonly IDirectorateWriteRepository _directorateWriteRepository;

        public CreateDirectorateCommandHandler(IDirectorateWriteRepository directorateWriteRepository)
        {
            _directorateWriteRepository = directorateWriteRepository;
        }

        public async Task<CreateDirectorateCommandResponse> Handle(CreateDirectorateCommandRequest request, CancellationToken cancellationToken)
        {
            await _directorateWriteRepository.AddAsync(new()
            {
                Name = request.Name,
            });
            await _directorateWriteRepository.SaveAsync();

            return new CreateDirectorateCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
