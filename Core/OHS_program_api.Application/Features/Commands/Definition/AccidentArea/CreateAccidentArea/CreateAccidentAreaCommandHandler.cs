using MediatR;
using OHS_program_api.Application.Repositories.Definition.AccidentAreaRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.AccidentArea.CreateAccidentArea
{
    internal class CreateAccidentAreaCommandHandler : IRequestHandler<CreateAccidentAreaCommandRequest, CreateAccidentAreaCommandResponse>
    {
        readonly IAccidentAreaWriteRepository _accidentAreaWriteRepository;

        public CreateAccidentAreaCommandHandler(IAccidentAreaWriteRepository accidentAreaWriteRepository)
        {
            _accidentAreaWriteRepository = accidentAreaWriteRepository;
        }

        public async Task<CreateAccidentAreaCommandResponse> Handle(CreateAccidentAreaCommandRequest request, CancellationToken cancellationToken)
        {
            await _accidentAreaWriteRepository.AddAsync(new()
            {
                Name = request.Name,
            });
            await _accidentAreaWriteRepository.SaveAsync();

            return new CreateAccidentAreaCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
