using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.CreateTypeOfAccident
{
    public class CreateTypeOfAccidentCommandRequest : IRequest<CreateTypeOfAccidentCommandResponse>
    {
        public string Name { get; set; }
    }
}
