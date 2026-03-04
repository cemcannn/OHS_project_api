using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Profession.CreateProfession
{
    public class CreateProfessionCommandRequest : IRequest<CreateProfessionCommandResponse>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? WorkType { get; set; }
    }
}
