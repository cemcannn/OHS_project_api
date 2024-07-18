using MediatR;
using OHS_program_api.Application.Repositories.Definition.LimbRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Limb.CreateLimb
{
    internal class CreateLimbCommandHandler : IRequestHandler<CreateLimbCommandRequest, CreateLimbCommandResponse>
    {
        readonly ILimbWriteRepository _limbWriteRepository;

        public CreateLimbCommandHandler(ILimbWriteRepository limbWriteRepository)
        {
            _limbWriteRepository = limbWriteRepository;
        }

        public async Task<CreateLimbCommandResponse> Handle(CreateLimbCommandRequest request, CancellationToken cancellationToken)
        {
            await _limbWriteRepository.AddAsync(new()
            {
                Name = request.Name,
            });
            await _limbWriteRepository.SaveAsync();

            return new CreateLimbCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
