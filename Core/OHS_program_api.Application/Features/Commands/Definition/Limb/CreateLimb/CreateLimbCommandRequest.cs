using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Limb.CreateLimb
{
    public class CreateLimbCommandRequest : IRequest<CreateLimbCommandResponse>
    {
        public string Name { get; set; }
    }
}
