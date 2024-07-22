using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Unit.CreateUnit
{
    public class CreateUnitCommandRequest : IRequest<CreateUnitCommandResponse>
    {
        public string Name { get; set; }
    }
}
