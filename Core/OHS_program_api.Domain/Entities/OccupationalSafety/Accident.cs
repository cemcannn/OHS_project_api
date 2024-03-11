using OHS_program_api.Domain.Entities;
using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using System;
using System.ComponentModel.DataAnnotations;

namespace OHS_program_api.Domain.Entities.OccupationalSafety
{
    public class Accident : BaseEntity
    {
        public Guid PersonnelId { get; set; }
        public Personnel Personnel { get; set; }
        //public Guid? TypeOfAccidentId { get; set; }
        public string? TypeOfAccident { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentHour { get; set; }
        public DateTime? OnTheJobDate { get; set; }
        public string? Description { get; set; }
        //public Guid? LimbId { get; set; }
        //public Limb? Limb { get; set; }
        //public Reason? Reason { get; set; }
        //public Guid? SiteId { get; set; }
        //public Site? Site { get; set; }
        //public Guid? UnitId { get; set; }
        //public Unit? Unit { get; set; }
        //public bool IsUnsafeEnviroment { get; set; }
        //public bool IsUnsafeBehavior { get; set; }
    }
}


