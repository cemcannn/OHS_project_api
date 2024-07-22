using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Unit.UpdateUnit
{
    public class UpdateUnitCommandRequest : IRequest<UpdateUnitCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
