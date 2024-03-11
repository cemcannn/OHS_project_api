﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.ViewModels.Personnel;

namespace OHS_program_api.Application.Features.Queries.Personnel.GetPersonnels
{
    public class GetPersonnelsQueryHandler : IRequestHandler<GetPersonnelsQueryRequest, GetPersonnelsQueryResponse>
    {
        readonly IPersonnelReadRepository _personnelReadRepository;

        public GetPersonnelsQueryHandler(IPersonnelReadRepository personnelReadRepository)
        {
            _personnelReadRepository = personnelReadRepository;
        }

        public async Task<GetPersonnelsQueryResponse> Handle(GetPersonnelsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalPersonnelCount = _personnelReadRepository.GetAll(false).Count();
            var personnels = _personnelReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size)
                .Include(p => p.Accident)
                .Select(p => new
                {
                    p.Id,
                    p.TRIdNumber,
                    p.Name,
                    p.Surname,
                    p.RetiredId,
                    p.InsuranceId,
                    p.StartDateOfWork,
                    p.TypeOfPlace,
                    p.TKIId,
                    p.Unit,
                    p.Certificate,
                    p.TaskInstruction,
                    p.Accident,
                }).ToList();

            return new()
            {
                Datas = personnels,
                TotalCount = totalPersonnelCount
            };
        }
    }
}
