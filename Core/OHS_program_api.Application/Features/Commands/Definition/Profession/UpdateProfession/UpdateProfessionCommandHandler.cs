using MediatR;
using OHS_program_api.Application.Repositories.Definition.ProfessionRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.Profession.UpdateProfession
{
    public class UpdateProfessionCommandHandler : IRequestHandler<UpdateProfessionCommandRequest, UpdateProfessionCommandResponse>
    {
        readonly IProfessionWriteRepository _professionWriteRepository;
        readonly IProfessionReadRepository _professionReadRepository;

        public UpdateProfessionCommandHandler(IProfessionWriteRepository professionWriteRepository, IProfessionReadRepository professionReadRepository)
        {
            _professionWriteRepository = professionWriteRepository;
            _professionReadRepository = professionReadRepository;
        }

        public async Task<UpdateProfessionCommandResponse> Handle(UpdateProfessionCommandRequest request, CancellationToken UpdateProfessionCommandResponse)
        {
            Domain.Entities.Definitions.Profession? _profession = await _professionReadRepository.GetByIdAsync(request.Id);
            if (_profession != null)
            {
                _profession.Id = new Guid(request.Id);
                _profession.Name = request.Name;

                await _professionWriteRepository.SaveAsync();
            }
            return new UpdateProfessionCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
