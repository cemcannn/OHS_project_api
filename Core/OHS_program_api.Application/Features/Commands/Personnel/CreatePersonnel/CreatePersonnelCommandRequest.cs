using MediatR;

namespace OHS_program_api.Application.Features.Commands.Personnel.CreatePersonnel
{
    public class CreatePersonnelCommandRequest : IRequest<CreatePersonnelCommandResponse>
    {
        public string TRIdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? RetiredId { get; set; }
        public string? InsuranceId { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public string? TKIId { get; set; }
        public string Unit { get; set; }
    }
}
