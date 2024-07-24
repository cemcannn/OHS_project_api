using MediatR;

namespace OHS_program_api.Application.Features.Queries.Definition.Directorate.GetDirectorateById
{
    public class GetDirectorateByIdQueryRequest : IRequest<GetDirectorateByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
