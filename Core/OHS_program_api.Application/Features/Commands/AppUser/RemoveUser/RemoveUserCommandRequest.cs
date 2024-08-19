using MediatR;

namespace OHS_program_api.Application.Features.Commands.AppUser.RemoveUser
{
    public class RemoveUserCommandRequest : IRequest<RemoveUserCommandResponse>
    {
        public string Id { get; set; }
    }
}
