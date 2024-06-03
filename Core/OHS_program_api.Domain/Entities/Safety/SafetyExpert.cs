using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;

namespace OHS_program_api.Domain.Entities.OccupationalSafety
{
    public class SafetyExpert : BaseEntity
    {
        public int TRIdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
        public Guid TypeOfCertificateId { get; set; }
        public TypeOfCertificate TypeOfCertificate { get; set; }
        public int WorkPlaceId { get; set; }
        public string HazardClass { get; set; }
        public bool IsBidAssignment { get; set; }
        public int UnitPrice { get; set; }
        public int TotalAmount { get;set; }
    }
}
