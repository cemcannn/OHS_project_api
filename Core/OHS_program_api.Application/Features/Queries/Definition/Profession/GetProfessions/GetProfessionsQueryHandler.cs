using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories.Definition.ProfessionRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.Profession.GetProfessions
{
    public class GetProfessionsQueryHandler : IRequestHandler<GetProfessionsQueryRequest, GetProfessionsQueryResponse>
    {
        readonly IProfessionReadRepository _professionReadRepository;

        public GetProfessionsQueryHandler(IProfessionReadRepository professionReadRepository)
        {
            _professionReadRepository = professionReadRepository;
        }

        public async Task<GetProfessionsQueryResponse> Handle(GetProfessionsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _professionReadRepository.GetAll(false);

            var totalCount = await query.CountAsync(cancellationToken);

            var profession = await query
                .Select(p => new
                {
                    p.Id,
                    p.Name
                })
                .ToListAsync(cancellationToken);

            return new GetProfessionsQueryResponse
            {
                Datas = profession,
                TotalCount = totalCount
            };
        }
    }
}
