using MediatR;

namespace OHS_program_api.Application.Features.Queries.Definition.Profession.GetProfessionById
{
    public class GetProfessionByIdQueryRequest : IRequest<GetProfessionByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
