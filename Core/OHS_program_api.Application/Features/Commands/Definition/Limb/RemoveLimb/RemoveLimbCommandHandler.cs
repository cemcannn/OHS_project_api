using MediatR;
using OHS_program_api.Application.Repositories.Definition.LimbRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Limb.RemoveLimb
{
    public class RemoveLimbCommandHandler : IRequestHandler<RemoveLimbCommandRequest, RemoveLimbCommandResponse>
    {
        readonly ILimbWriteRepository _limbWriteRepository;

        public RemoveLimbCommandHandler(ILimbWriteRepository limbWriteRepository)
        {
            _limbWriteRepository = limbWriteRepository;
        }
        public async Task<RemoveLimbCommandResponse> Handle(RemoveLimbCommandRequest request, CancellationToken cancellationToken)
        {
            await _limbWriteRepository.RemoveAsync(request.Id);
            await _limbWriteRepository.SaveAsync();
            return new();
        }

    }
}
