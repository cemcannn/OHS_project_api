using MediatR;
using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Domain.Entities.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Commands.Personnel.CreatePersonnel
{
    public class CreatePersonnelCommandRequest : IRequest<CreatePersonnelCommandResponse>
    {
        public string TRIdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? RetiredId { get; set; }
        public int? InsuranceId { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public int? TKIId { get; set; }
    }
}
