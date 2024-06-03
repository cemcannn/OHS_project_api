using MediatR;

namespace OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.UpdateTypeOfAccident
{
    public class UpdateTypeOfAccidentCommandRequest : IRequest<UpdateTypeOfAccidentCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
