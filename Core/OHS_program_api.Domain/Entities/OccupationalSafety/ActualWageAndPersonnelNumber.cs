using OHS_program_api.Domain.Entities.Common;
using System;

namespace OHS_program_api.Domain.Entities.OccupationalSafety
{
    public class ActualWageAndPersonnelNumber : BaseEntity
    {
        public DateTime Year { get; set; }
        public DateTime Month { get; set; }
        public PlaceEnum TypeOfPlace { get; set; }
        public int OfficerNumber { get; set; }
        public int WagePersonNumber { get; set; }
        public int InternNumber { get; set; }
        public int ApprenticeNumber { get; set; }
        public int SalableProduction { get; set; }
        public int WageEmployeeActualWageNumber { get; set; }
        public int InternActualWageNumber { get; set; }
        public int ApprenticeActualWageNumber { get; set; }
    }
}
