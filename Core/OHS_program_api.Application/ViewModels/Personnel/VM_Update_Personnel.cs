﻿using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Domain.Entities.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.ViewModels.Personnel
{
    public class VM_Update_Personnel
    {
        public string Id { get; set; }
        public string TRIdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? RetiredId { get; set; }
        public string? InsuranceId { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public string? TKIId { get; set; }
        public string? Unit { get; set; }
    }
}
