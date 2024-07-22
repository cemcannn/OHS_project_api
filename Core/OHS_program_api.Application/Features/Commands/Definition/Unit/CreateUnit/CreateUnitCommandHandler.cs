using MediatR;
using OHS_program_api.Application.Repositories.Definition.UnitRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Unit.CreateUnit
{
    internal class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommandRequest, CreateUnitCommandResponse>
    {
        readonly IUnitWriteRepository _unitWriteRepository;

        public CreateUnitCommandHandler(IUnitWriteRepository unitWriteRepository)
        {
            _unitWriteRepository = unitWriteRepository;
        }

        public async Task<CreateUnitCommandResponse> Handle(CreateUnitCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitWriteRepository.AddAsync(new()
            {
                Name = request.Name,
            });
            await _unitWriteRepository.SaveAsync();

            return new CreateUnitCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
