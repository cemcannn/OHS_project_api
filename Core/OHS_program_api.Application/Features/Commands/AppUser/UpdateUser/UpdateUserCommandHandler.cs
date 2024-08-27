using MediatR;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.DTOs.User;

namespace OHS_program_api.Application.Features.Commands.AppUser.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            UpdateUserResponse response = await _userService.UpdateUserAsync(new()
            {
                Id = request.Id,
                Email = request.Email,
                Name = request.Name,
                Username = request.Username,
            });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };
        }
    }

}
