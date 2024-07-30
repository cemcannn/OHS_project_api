using MediatR;

namespace OHS_program_api.Application.Features.Queries.Safety.Accident.GetAccidentById
{
    public class GetAccidentByIdQueryRequest : IRequest<GetAccidentByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
