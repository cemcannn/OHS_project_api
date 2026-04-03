using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var personnel = await _personnelWriteRepository.Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id), cancellationToken);
            request.Name = personnel == null ? request.Name : $"{personnel.Name} {personnel.Surname}".Trim();
            await _personnelWriteRepository.RemoveAsync(request.Id);
            await _personnelWriteRepository.SaveAsync();
            return new();
        }
    }
}
