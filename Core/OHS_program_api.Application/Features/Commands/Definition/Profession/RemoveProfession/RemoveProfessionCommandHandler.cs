using MediatR;
using OHS_program_api.Application.Repositories.Definition.ProfessionRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Profession.RemoveProfession
{
    public class RemoveProfessionCommandHandler : IRequestHandler<RemoveProfessionCommandRequest, RemoveProfessionCommandResponse>
    {
        readonly IProfessionWriteRepository _professionWriteRepository;

        public RemoveProfessionCommandHandler(IProfessionWriteRepository professionWriteRepository)
        {
            _professionWriteRepository = professionWriteRepository;
        }
        public async Task<RemoveProfessionCommandResponse> Handle(RemoveProfessionCommandRequest request, CancellationToken cancellationToken)
        {
            await _professionWriteRepository.RemoveAsync(request.Id);
            await _professionWriteRepository.SaveAsync();
            return new();
        }

    }
}
