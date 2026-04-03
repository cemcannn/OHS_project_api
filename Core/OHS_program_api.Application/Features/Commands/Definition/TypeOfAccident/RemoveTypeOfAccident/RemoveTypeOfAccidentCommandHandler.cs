using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Features.Commands.Personnel.RemovePersonnel;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.RemoveTypeOfAccident
{
    public class RemoveTypeOfAccidentCommandHandler : IRequestHandler<RemoveTypeOfAccidentCommandRequest, RemoveTypeOfAccidentCommandResponse>
    {
        readonly ITypeOfAccidentWriteRepository _typeOfAccidentWriteRepository;

        public RemoveTypeOfAccidentCommandHandler(ITypeOfAccidentWriteRepository typeOfAccidentWriteRepository)
        {
            _typeOfAccidentWriteRepository = typeOfAccidentWriteRepository;
        }

        public async Task<RemoveTypeOfAccidentCommandResponse> Handle(RemoveTypeOfAccidentCommandRequest request, CancellationToken cancellationToken)
        {
            var typeOfAccident = await _typeOfAccidentWriteRepository.Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));
            request.Name = typeOfAccident?.Name ?? request.Name;
            await _typeOfAccidentWriteRepository.RemoveAsync(request.Id);
            await _typeOfAccidentWriteRepository.SaveAsync();
            return new();
        }
    }
}
