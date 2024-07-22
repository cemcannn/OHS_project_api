using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Unit.RemoveUnit
{
    public class RemoveUnitCommandRequest : IRequest<RemoveUnitCommandResponse>
    {
        public string Id { get; set; }
    }
}
