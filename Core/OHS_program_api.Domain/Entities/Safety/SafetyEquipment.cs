using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;

namespace OHS_program_api.Domain.Entities.OccupationalSafety
{
    public class SafetyEquipment : BaseEntity
    {
        public DateTime GivenDate { get; set; }
        public Guid TypeOfSafetyEquipmentId { get; set; }
        public TypeOfSafetyEquipment TypeOfEquipment { get; set; }
    }
}
