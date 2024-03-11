using OHS_program_api.Domain.Entities.Common;
using System;

namespace OHS_program_api.Domain.Entities.OccupationalHealth
{
    public class HealthPersonnel : BaseEntity
    {
        public Guid PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        //public int UnitId { get; set; }
        //public Unit Unit { get; set; }
    }
}
