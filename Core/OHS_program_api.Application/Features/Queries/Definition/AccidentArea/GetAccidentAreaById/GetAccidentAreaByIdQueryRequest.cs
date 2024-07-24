using MediatR;

namespace OHS_program_api.Application.Features.Queries.Definition.AccidentArea.GetAccidentAreaById
{
    public class GetAccidentAreaByIdQueryRequest : IRequest<GetAccidentAreaByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
