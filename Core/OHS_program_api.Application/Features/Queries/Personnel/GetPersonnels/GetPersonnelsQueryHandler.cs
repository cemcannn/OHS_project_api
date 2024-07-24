using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories;

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
            var personnels = _personnelReadRepository.GetAll(false)
                .Include(p => p.Accident)
                .Select(p => new
                {
                    p.Id,
                    p.TRIdNumber,
                    p.TKIId,
                    p.Name,
                    p.Surname,
                    p.StartDateOfWork,
                    p.Profession,
                    p.Accident,
                    p.Directorate
                }).ToList();

            return new()
            {
                Datas = personnels,
                TotalCount = totalPersonnelCount
            };
        }
    }
}
