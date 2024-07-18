using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Limb.RemoveLimb
{
    public class RemoveLimbCommandRequest : IRequest<RemoveLimbCommandResponse>
    {
        public string Id { get; set; }
    }
}
