using MediatR;
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
            var totalCount = _typeOfAccidentReadRepository.GetAll(false).Count();
            var typeOfAccident = _typeOfAccidentReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Name

                }).ToList();

            return new()
            {
                Datas = typeOfAccident,
                TotalCount = totalCount
            };
        }
    }
}
