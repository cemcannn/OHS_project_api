using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.AccidentArea.CreateAccidentArea
{
    public class CreateAccidentAreaCommandRequest : IRequest<CreateAccidentAreaCommandResponse>
    {
        public string Name { get; set; }
    }
}
