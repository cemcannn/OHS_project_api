using MediatR;

namespace OHS_program_api.Application.Features.Queries.Definition.TypeOfAccident.GetTypeOfAccidentById
{
    public class GetTypeOfAccidentByIdQueryRequest : IRequest<GetTypeOfAccidentByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
