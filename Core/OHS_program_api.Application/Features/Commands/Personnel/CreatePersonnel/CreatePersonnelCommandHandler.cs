using MediatR;
using OHS_program_api.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Commands.Personnel.CreatePersonnel
{
    public class CreatePersonnelCommandHandler : IRequestHandler<CreatePersonnelCommandRequest, CreatePersonnelCommandResponse>
    {
        readonly IPersonnelWriteRepository _personnelWriteRepository;


        public CreatePersonnelCommandHandler(IPersonnelWriteRepository personnelWriteRepository)
        {
            _personnelWriteRepository = personnelWriteRepository;
        }

        public async Task<CreatePersonnelCommandResponse> Handle(CreatePersonnelCommandRequest request, CancellationToken cancellationToken)
        {
            await _personnelWriteRepository.AddAsync(new()
            {
                TRIdNumber = request.TRIdNumber,
                Name = request.Name,
                Surname = request.Surname,
                RetiredId = request.RetiredId,
                InsuranceId = request.InsuranceId,
                StartDateOfWork = request.StartDateOfWork,
                TKIId = request.TKIId,
            });
            await _personnelWriteRepository.SaveAsync();
            return new();
        }
    }
}
