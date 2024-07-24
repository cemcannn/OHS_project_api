using MediatR;
using OHS_program_api.Application.Repositories.Definition.ProfessionRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Profession.CreateProfession
{
    internal class CreateProfessionCommandHandler : IRequestHandler<CreateProfessionCommandRequest, CreateProfessionCommandResponse>
    {
        readonly IProfessionWriteRepository _professionWriteRepository;

        public CreateProfessionCommandHandler(IProfessionWriteRepository professionWriteRepository)
        {
            _professionWriteRepository = professionWriteRepository;
        }

        public async Task<CreateProfessionCommandResponse> Handle(CreateProfessionCommandRequest request, CancellationToken cancellationToken)
        {
            await _professionWriteRepository.AddAsync(new()
            {
                Name = request.Name,
            });
            await _professionWriteRepository.SaveAsync();

            return new CreateProfessionCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
