using MediatR;

namespace OHS_program_api.Application.Features.Queries.Definition.Limb.GetLimbById
{
    public class GetLimbByIdQueryRequest : IRequest<GetLimbByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
