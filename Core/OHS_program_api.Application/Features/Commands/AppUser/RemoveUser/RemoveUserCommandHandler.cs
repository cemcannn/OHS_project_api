using MediatR;
using OHS_program_api.Application.Abstractions.Services;

namespace OHS_program_api.Application.Features.Commands.AppUser.RemoveUser
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommandRequest, RemoveUserCommandResponse>
    {
        private readonly IUserService _userService;

        public RemoveUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<RemoveUserCommandResponse> Handle(RemoveUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.Id);
            request.Name = string.IsNullOrWhiteSpace(user?.Name) ? request.Name : user.Name;
            await _userService.RemoveUserAsync(request.Id);
            return new();
        }
    }
}

