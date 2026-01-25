using MediatR;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.UpdateTypeOfAccident;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Directorate.UpdateDirectorate
{
    public class UpdateDirectorateCommandHandler : IRequestHandler<UpdateDirectorateCommandRequest, UpdateDirectorateCommandResponse>
    {
        readonly IDirectorateWriteRepository _directorateWriteRepository;
        readonly IDirectorateReadRepository _directorateReadRepository;

        public UpdateDirectorateCommandHandler(IDirectorateWriteRepository directorateWriteRepository, IDirectorateReadRepository directorateReadRepository)
        {
            _directorateWriteRepository = directorateWriteRepository;
            _directorateReadRepository = directorateReadRepository;
        }

        public async Task<UpdateDirectorateCommandResponse> Handle(UpdateDirectorateCommandRequest request, CancellationToken UpdateDirectorateCommandResponse)
        {
            Domain.Entities.Definitions.Directorate? _directorate = await _directorateReadRepository.GetByIdAsync(request.Id);
            if (_directorate != null)
            {
                _directorate.Id = new Guid(request.Id);
                _directorate.Name = request.Name;

                await _directorateWriteRepository.SaveAsync();
            }
            return new UpdateDirectorateCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
