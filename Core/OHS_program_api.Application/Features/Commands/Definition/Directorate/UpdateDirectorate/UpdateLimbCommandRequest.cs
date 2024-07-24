using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Directorate.UpdateDirectorate
{
    public class UpdateDirectorateCommandRequest : IRequest<UpdateDirectorateCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
