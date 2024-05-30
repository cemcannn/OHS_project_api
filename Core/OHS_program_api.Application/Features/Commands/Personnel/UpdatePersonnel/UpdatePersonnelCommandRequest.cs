using MediatR;

namespace OHS_program_api.Application.Features.Commands.Personnel.UpdatePersonnel
{
    public class UpdatePersonnelCommandRequest : IRequest<UpdatePersonnelCommandResponse>
    {
        public string Id { get; set; }
        public string TRIdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? RetiredId { get; set; }
        public int? InsuranceId { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public int? TKIId { get; set; }
    }
}
