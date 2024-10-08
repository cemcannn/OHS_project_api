﻿using Microsoft.AspNetCore.Identity;

namespace OHS_program_api.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<string>
    {
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
