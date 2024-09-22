using MediatR;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Domain.Entities;

namespace OHS_program_api.Application.Features.Commands.Safety.Accident.CreateAccident
{
    public class CreateAccidentCommandRequest : IRequest<CreateAccidentCommandResponse>
    {
        public string PersonnelId { get; set; }
        public string TypeOfAccident { get; set; }
        public string Limb { get; set; }
        public string AccidentArea { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentHour { get; set; }
        public int? LostDayOfWork { get; set; }
        public string? Description { get; set; }
    }
}
