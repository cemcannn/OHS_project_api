using MediatR;
using OHS_program_api.Application.Repositories;

namespace OHS_program_api.Application.Features.Queries.Personnel.GetPersonnelByID
{
    public class GetByIdPersonnelQueryHandler : IRequestHandler<GetPersonnelByIdQueryRequest, GetPersonnelByIdQueryResponse>
    {

        readonly IPersonnelReadRepository _personnelReadRepository;
        public GetByIdProductQueryHandler(IPersonnelReadRepository personnelReadRepository)
        {
            _personnelReadRepository = personnelReadRepository;
        }

        public async Task<GetPersonnelByIdQueryResponse> Handle(GetPersonnelByIdQueryRequest request, CancellationToken cancellationToken)
        {

            Domain.Entities.Personnel personnel = await _personnelReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                TRIdNumber = Convert.ToString(personnel.TRIdNumber),
                Name = personnel.Name,
                Surname = personnel.Surname,
                RetiredId = Convert.ToString(personnel.RetiredId),
                InsuranceId = Convert.ToString(personnel.InsuranceId),
                StartDateOfWork = personnel.StartDateOfWork,
                Profession = personnel.Profession.Name,
                TypeOfPlace = Convert.ToString(personnel.TypeOfPlace),
                TKIId = Convert.ToString(personnel.TKIId),
                Unit = personnel.Unit.Name,
                Certificate = personnel.Certificate.Select(c => c.Name).ToList(),
                TaskInstruction = personnel.TaskInstruction.Select(ti => ti.Name).ToList(),
                Accident = personnel.Accident.Select(a => a.TypeOfAccident).ToList()
            };
        }
    }
}
