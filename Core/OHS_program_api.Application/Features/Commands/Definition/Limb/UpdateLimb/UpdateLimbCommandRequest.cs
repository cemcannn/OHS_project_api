using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Limb.UpdateLimb
{
    public class UpdateLimbCommandRequest : IRequest<UpdateLimbCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
