
using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;
using System.Collections.Generic;

namespace OHS_program_api.Domain.Entities.OccupationalHealth
{
    public class HealthSurveillance : BaseEntity
    {
        public Guid TypeOfExaminationId { get; set; }
        public ICollection<TypeOfExamination> TypeOfExamination { get; set; }
        public DateTime ExaminationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
