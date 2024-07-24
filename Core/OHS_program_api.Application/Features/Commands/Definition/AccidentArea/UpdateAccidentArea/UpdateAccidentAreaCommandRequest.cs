using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.AccidentArea.UpdateAccidentArea
{
    public class UpdateAccidentAreaCommandRequest : IRequest<UpdateAccidentAreaCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
