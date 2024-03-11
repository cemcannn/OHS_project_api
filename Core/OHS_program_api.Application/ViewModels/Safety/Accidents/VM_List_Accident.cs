﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.ViewModels.Safety.Accidents
{
    public class VM_List_Accident
    {
        public string Id { get; set; }
        public string PersonnelId { get; set; }
        public string TypeOfAccident { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentHour { get; set; }
        public DateTime? OnTheJobDate { get; set; }
        public string? Description { get; set; }
    }
}
