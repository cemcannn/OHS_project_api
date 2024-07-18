using MediatR;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.UpdateTypeOfAccident;
using OHS_program_api.Application.Repositories.Definition.LimbRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Limb.UpdateLimb
{
    public class UpdateLimbCommandHandler : IRequestHandler<UpdateLimbCommandRequest, UpdateLimbCommandResponse>
    {
        readonly ILimbWriteRepository _limbWriteRepository;
        readonly ILimbReadRepository _limbReadRepository;

        public UpdateLimbCommandHandler(ILimbWriteRepository limbWriteRepository, ILimbReadRepository limbReadRepository)
        {
            _limbWriteRepository = limbWriteRepository;
            _limbReadRepository = limbReadRepository;
        }

        public async Task<UpdateLimbCommandResponse> Handle(UpdateLimbCommandRequest request, CancellationToken UpdateLimbCommandResponse)
        {
            Domain.Entities.Definitions.Limb? _limb = await _limbReadRepository.GetByIdAsync(request.Id);
            if (_limb != null)
            {
                _limb.Id = new Guid(request.Id);
                _limb.Name = request.Name;

                await _limbWriteRepository.SaveAsync();
            }
            return new UpdateLimbCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
