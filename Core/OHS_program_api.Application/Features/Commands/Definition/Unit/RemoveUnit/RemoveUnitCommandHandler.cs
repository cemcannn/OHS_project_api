using MediatR;
using OHS_program_api.Application.Repositories.Definition.UnitRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Unit.RemoveUnit
{
    public class RemoveUnitCommandHandler : IRequestHandler<RemoveUnitCommandRequest, RemoveUnitCommandResponse>
    {
        readonly IUnitWriteRepository _unitWriteRepository;

        public RemoveUnitCommandHandler(IUnitWriteRepository unitWriteRepository)
        {
            _unitWriteRepository = unitWriteRepository;
        }
        public async Task<RemoveUnitCommandResponse> Handle(RemoveUnitCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitWriteRepository.RemoveAsync(request.Id);
            await _unitWriteRepository.SaveAsync();
            return new();
        }

    }
}
