using MediatR;

namespace OHS_program_api.Application.Features.Queries.AppUser.GetUserById
{
    public class GetUserByIdQueryRequest : IRequest<GetUserByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
