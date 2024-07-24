using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.Profession.UpdateProfession
{
    public class UpdateProfessionCommandRequest : IRequest<UpdateProfessionCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
