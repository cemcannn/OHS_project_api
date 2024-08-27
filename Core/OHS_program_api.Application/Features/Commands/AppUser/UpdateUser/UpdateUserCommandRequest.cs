using MediatR;

namespace OHS_program_api.Application.Features.Commands.AppUser.UpdateUser
{
    public class UpdateUserCommandRequest : IRequest<UpdateUserCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
