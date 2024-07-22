using MediatR;
using OHS_program_api.Application.Repositories.Definition.UnitRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.Unit.GetUnits
{
    public class GetUnitsQueryHandler : IRequestHandler<GetUnitsQueryRequest, GetUnitsQueryResponse>
    {
        readonly IUnitReadRepository _unitReadRepository;

        public GetUnitsQueryHandler(IUnitReadRepository unitReadRepository)
        {
            _unitReadRepository = unitReadRepository;
        }

        public async Task<GetUnitsQueryResponse> Handle(GetUnitsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _unitReadRepository.GetAll(false).Count();
            var unit = _unitReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Name

                }).ToList();

            return new()
            {
                Datas = unit,
                TotalCount = totalCount
            };
        }
    }
}
