using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Directorate.CreateDirectorate
{
    public class CreateDirectorateCommandRequest : IRequest<CreateDirectorateCommandResponse>
    {
        public string Name { get; set; }
    }
}
