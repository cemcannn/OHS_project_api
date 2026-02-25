using MediatR;
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
            var query = _accidentReadRepository
                .GetAll(false)
                .Include(p => p.Personnel);

            var totalAccidentCount = await query.CountAsync(cancellationToken);

            var accidents = await query
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
                    Name = p.Personnel != null ? p.Personnel.Name : null,
                    Surname = p.Personnel != null ? p.Personnel.Surname : null,
                    TRIdNumber = p.Personnel != null ? p.Personnel.TRIdNumber : null,
                    TKIId = p.Personnel != null ? p.Personnel.TKIId : null,
                    Directorate = p.Personnel != null ? p.Personnel.Directorate : null,
                    Profession = p.Personnel != null ? p.Personnel.Profession : null,
                    BornDate = p.Personnel != null ? p.Personnel.BornDate : null
                })
                .ToListAsync(cancellationToken);

            return new()
            {
                Datas = accidents,
                TotalCount = totalAccidentCount
            };
        }
    }
}
