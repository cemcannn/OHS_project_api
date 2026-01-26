using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var query = _limbReadRepository.GetAll(false);

            var totalCount = await query.CountAsync(cancellationToken);

            var limb = await query
                .Select(p => new
                {
                    p.Id,
                    p.Name
                })
                .ToListAsync(cancellationToken);

            return new GetLimbsQueryResponse
            {
                Datas = limb,
                TotalCount = totalCount
            };
        }
    }
}
