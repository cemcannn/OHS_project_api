using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;

namespace OHS_program_api.Domain.Entities.OccupationalSafety
{
    public class PeriodicControl : BaseEntity
    {
        public Guid TypeOfWorkEquipmentId { get; set; }
        public TypeOfWorkEquipment TypeOfWorkEquipment { get; set; }
        public DateTime CheckDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
        public string Description { get; set; }
    }
}
