using MediatR;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.UpdateTypeOfAccident;
using OHS_program_api.Application.Repositories.Definition.AccidentAreaRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.AccidentArea.UpdateAccidentArea
{
    public class UpdateAccidentAreaCommandHandler : IRequestHandler<UpdateAccidentAreaCommandRequest, UpdateAccidentAreaCommandResponse>
    {
        readonly IAccidentAreaWriteRepository _accidentAreaWriteRepository;
        readonly IAccidentAreaReadRepository _accidentAreaReadRepository;

        public UpdateAccidentAreaCommandHandler(IAccidentAreaWriteRepository accidentAreaWriteRepository, IAccidentAreaReadRepository accidentAreaReadRepository)
        {
            _accidentAreaWriteRepository = accidentAreaWriteRepository;
            _accidentAreaReadRepository = accidentAreaReadRepository;
        }

        public async Task<UpdateAccidentAreaCommandResponse> Handle(UpdateAccidentAreaCommandRequest request, CancellationToken UpdateAccidentAreaCommandResponse)
        {
            Domain.Entities.Definitions.AccidentArea? _accidentArea = await _accidentAreaReadRepository.GetByIdAsync(request.Id);
            if (_accidentArea != null)
            {
                _accidentArea.Id = new Guid(request.Id);
                _accidentArea.Name = request.Name;

                await _accidentAreaWriteRepository.SaveAsync();
            }
            return new UpdateAccidentAreaCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
