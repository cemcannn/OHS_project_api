using MediatR;

namespace OHS_program_api.Application.Features.Commands.Safety.Accident.UpdateAccident
{
    public class UpdateAccidentCommandRequest : IRequest<UpdateAccidentCommandResponse>
    {
        public string Id { get; set; }
        public string TypeOfAccident { get; set; }
        public string Limb {  get; set; }
        public string? AccidentArea { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentHour { get; set; }
        public int? LostDayOfWork { get; set; }
        public string? Description { get; set; }
    }
}
