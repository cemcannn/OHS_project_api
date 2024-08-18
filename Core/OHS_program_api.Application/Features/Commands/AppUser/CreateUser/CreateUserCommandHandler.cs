using MediatR;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.DTOs.User;

namespace OHS_program_api.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username,
            });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };

            //throw new UserCreateFailedException();
        }
    }
}
