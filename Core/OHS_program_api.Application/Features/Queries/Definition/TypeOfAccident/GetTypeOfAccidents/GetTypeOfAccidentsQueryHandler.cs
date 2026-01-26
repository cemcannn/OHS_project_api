using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.TypeOfAccident.GetTypeOfAccident
{
    public class GetTypeOfAccidentsQueryHandler : IRequestHandler<GetTypeOfAccidentsQueryRequest, GetTypeOfAccidentsQueryResponse>
    {
        readonly ITypeOfAccidentReadRepository _typeOfAccidentReadRepository;

        public GetTypeOfAccidentsQueryHandler(ITypeOfAccidentReadRepository typeOfAccidentReadRepository)
        {
            _typeOfAccidentReadRepository = typeOfAccidentReadRepository;
        }

        public async Task<GetTypeOfAccidentsQueryResponse> Handle(GetTypeOfAccidentsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _typeOfAccidentReadRepository.GetAll(false);

            var totalCount = await query.CountAsync(cancellationToken);

            var typeOfAccident = await query
                .Select(p => new
                {
                    p.Id,
                    p.Name
                })
                .ToListAsync(cancellationToken);

            return new GetTypeOfAccidentsQueryResponse
            {
                Datas = typeOfAccident,
                TotalCount = totalCount
            };
        }
    }
}
