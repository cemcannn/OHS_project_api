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
            await _userService.RemoveUserAsync(request.Id);
            return new();
        }
    }
}

