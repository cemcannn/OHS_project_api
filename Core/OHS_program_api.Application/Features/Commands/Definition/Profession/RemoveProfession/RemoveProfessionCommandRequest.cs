using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Profession.RemoveProfession
{
    public class RemoveProfessionCommandRequest : IRequest<RemoveProfessionCommandResponse>
    {
        public string Id { get; set; }
        public string? Name { get; set; }
    }
}
