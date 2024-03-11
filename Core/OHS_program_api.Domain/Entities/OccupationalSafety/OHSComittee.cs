using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;

namespace OHS_program_api.Domain.Entities.OccupationalSafety
{
    public class OHSComittee : BaseEntity
    {
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
