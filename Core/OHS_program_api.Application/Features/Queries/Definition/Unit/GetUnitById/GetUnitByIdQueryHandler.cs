using MediatR;
using OHS_program_api.Application.Repositories.Definition.UnitRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.Unit.GetUnitById
{
    public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQueryRequest, GetUnitByIdQueryResponse>
    {
        readonly IUnitReadRepository _unitReadRepository;

        public GetUnitByIdQueryHandler(IUnitReadRepository unitReadRepository)
        {
            _unitReadRepository = unitReadRepository;
        }

        public async Task<GetUnitByIdQueryResponse> Handle(GetUnitByIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the Unit by Id
            var unit = await _unitReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Id = unit.Id.ToString(),
                Name = unit.Name
            };
        }
    }
}
