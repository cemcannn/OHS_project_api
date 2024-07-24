using MediatR;
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
            var totalCount = _professionReadRepository.GetAll(false).Count();
            var profession = _professionReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Name

                }).ToList();

            return new()
            {
                Datas = profession,
                TotalCount = totalCount
            };
        }
    }
}
