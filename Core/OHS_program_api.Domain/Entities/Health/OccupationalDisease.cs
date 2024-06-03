using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;
using System.Collections.Generic;

namespace OHS_program_api.Domain.Entities.OccupationalHealth
{
    public class OccupationalDisease : BaseEntity
    {
        public Guid TypeOfDiseaseId { get; set; }
        public ICollection<TypeOfDisease> TypeOfDisease { get; set; }
        public DateTime NotificationDate { get; set; }
        public DateTime OnTheJobDate { get; set; }
    }
}
