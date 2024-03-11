using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;

namespace OHS_program_api.Domain.Entities.OccupationalHealth
{
    public class WorkplaceTestAndAnalysis : BaseEntity
    {
        public Guid TypeOfAnalysisId { get; set; }
        public TypeOfAnalysis TypeOfAnalysis { get; set; }
        public DateTime AnalysisDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
