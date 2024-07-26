using MediatR;

namespace OHS_program_api.Application.Features.Commands.Personnel.CreatePersonnel
{
    public class CreatePersonnelCommandRequest : IRequest<CreatePersonnelCommandResponse>
    {
        public string TRIdNumber { get; set; }
        public string? TKIId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BornDate { get; set; }
        public string Profession { get; set; }
        public string? Directorate { get; set; }
    }
}
