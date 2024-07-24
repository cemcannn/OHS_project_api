using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Domain.Entities.Identity;
using OHS_program_api.Domain.Entities.OccupationalSafety;

namespace OHS_program_api.Persistence.Contexts
{
    public class OHSProgramAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public OHSProgramAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<AccidentArea> AccidentAreas { get; set; }
        public DbSet<Directorate> Directorates { get; set; }
        public DbSet<Limb> Limbs { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<TypeOfAccident> TypeOfAccident { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
