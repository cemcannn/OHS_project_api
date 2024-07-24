using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.AccidentArea.RemoveAccidentArea
{
    public class RemoveAccidentAreaCommandRequest : IRequest<RemoveAccidentAreaCommandResponse>
    {
        public string Id { get; set; }
    }
}
