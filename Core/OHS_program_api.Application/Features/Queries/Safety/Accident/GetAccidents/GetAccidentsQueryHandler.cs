﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;

namespace OHS_program_api.Application.Features.Queries.Safety.Accident.GetAccidents
{
    public class GetAccidentsQueryHandler : IRequestHandler<GetAccidentsQueryRequest, GetAccidentsQueryResponse>
    {
        readonly IAccidentReadRepository _accidentReadRepository;

        public GetAccidentsQueryHandler(IAccidentReadRepository accidentReadRepository)
        {
            _accidentReadRepository = accidentReadRepository;
        }

        public async Task<GetAccidentsQueryResponse> Handle(GetAccidentsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalAccidentCount = _accidentReadRepository.GetAll(false).Count();
            var accidents = _accidentReadRepository.GetAll(false)
                .Include(p => p.Personnel)
                .Select(p => new
                {
                    p.Id,
                    p.AccidentDate,
                    p.AccidentHour,
                    p.TypeOfAccident,
                    p.Limb,
                    p.AccidentArea,
                    p.LostDayOfWork,
                    p.Description,
                    p.PersonnelId,
                    p.Personnel.Name,
                    p.Personnel.Surname,
                    p.Personnel.TRIdNumber,
                    p.Personnel.TKIId,
                    p.Personnel.Directorate
                }).ToList();

            return new()
            {
                Datas = accidents,
                TotalCount = totalAccidentCount
            };
        }
    }
}
