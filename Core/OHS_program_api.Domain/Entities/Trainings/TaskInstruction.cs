using OHS_program_api.Domain.Entities.Common;
using System;

namespace OHS_program_api.Domain.Entities.Trainings
{
    public class TaskInstruction : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime StartDateOfWork { get; set; }
        public Guid PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
    }
}
