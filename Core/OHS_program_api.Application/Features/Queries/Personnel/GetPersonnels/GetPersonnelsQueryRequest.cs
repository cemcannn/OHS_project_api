using MediatR;

namespace OHS_program_api.Application.Features.Queries.Personnel.GetPersonnels
{
    public class GetPersonnelsQueryRequest : IRequest<GetPersonnelsQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
