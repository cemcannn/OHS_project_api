using MediatR;
using OHS_program_api.Application.Repositories.Definition.UnitRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Unit.UpdateUnit
{
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommandRequest, UpdateUnitCommandResponse>
    {
        readonly IUnitWriteRepository _unitWriteRepository;
        readonly IUnitReadRepository _unitReadRepository;

        public UpdateUnitCommandHandler(IUnitWriteRepository unitWriteRepository, IUnitReadRepository unitReadRepository)
        {
            _unitWriteRepository = unitWriteRepository;
            _unitReadRepository = unitReadRepository;
        }

        public async Task<UpdateUnitCommandResponse> Handle(UpdateUnitCommandRequest request, CancellationToken UpdateUnitCommandResponse)
        {
            Domain.Entities.Definitions.Unit? _unit = await _unitReadRepository.GetByIdAsync(request.Id);
            if (_unit != null)
            {
                _unit.Id = new Guid(request.Id);
                _unit.Name = request.Name;

                await _unitWriteRepository.SaveAsync();
            }
            return new UpdateUnitCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
