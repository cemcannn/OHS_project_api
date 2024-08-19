using MediatR;
using OHS_program_api.Application.Repositories;

namespace OHS_program_api.Application.Features.Commands.Personnel.RemovePersonnel
{
    public class RemovePersonnelCommandHandler : IRequestHandler<RemovePersonnelCommandRequest, RemovePersonnelCommandResponse>
    {
        readonly IPersonnelWriteRepository _personnelWriteRepository;

        public RemovePersonnelCommandHandler(IPersonnelWriteRepository personnelWriteRepository)
        {
            _personnelWriteRepository = personnelWriteRepository;
        }

        public async Task<RemovePersonnelCommandResponse> Handle(RemovePersonnelCommandRequest request, CancellationToken cancellationToken)
        {
            await _personnelWriteRepository.RemoveAsync(request.Id);
            await _personnelWriteRepository.SaveAsync();
            return new();
        }
    }
}
