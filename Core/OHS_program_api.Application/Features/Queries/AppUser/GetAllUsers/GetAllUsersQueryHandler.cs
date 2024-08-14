using MediatR;
using OHS_program_api.Application.Abstractions.Services;

namespace OHS_program_api.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync();
            return new()
            {
                Users = users,
                TotalUsersCount = _userService.TotalUsersCount
            };
        }
    }
}
