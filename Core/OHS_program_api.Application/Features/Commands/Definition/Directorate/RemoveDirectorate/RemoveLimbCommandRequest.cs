using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Directorate.RemoveDirectorate
{
    public class RemoveDirectorateCommandRequest : IRequest<RemoveDirectorateCommandResponse>
    {
        public string Id { get; set; }
    }
}
