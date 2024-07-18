using MediatR;
using OHS_program_api.Application.Repositories.Definition.LimbRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.Limb.GetLimbs
{
    public class GetLimbsQueryHandler : IRequestHandler<GetLimbsQueryRequest, GetLimbsQueryResponse>
    {
        readonly ILimbReadRepository _limbReadRepository;

        public GetLimbsQueryHandler(ILimbReadRepository limbReadRepository)
        {
            _limbReadRepository = limbReadRepository;
        }

        public async Task<GetLimbsQueryResponse> Handle(GetLimbsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _limbReadRepository.GetAll(false).Count();
            var limb = _limbReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Name

                }).ToList();

            return new()
            {
                Datas = limb,
                TotalCount = totalCount
            };
        }
    }
}
