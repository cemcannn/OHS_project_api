using MediatR;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.Features.Commands.Role.DeleteRole;
using OHS_program_api.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
