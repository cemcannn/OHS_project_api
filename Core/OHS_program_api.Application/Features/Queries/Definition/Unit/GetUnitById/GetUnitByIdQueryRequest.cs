using MediatR;

namespace OHS_program_api.Application.Features.Queries.Definition.Unit.GetUnitById
{
    public class GetUnitByIdQueryRequest : IRequest<GetUnitByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
