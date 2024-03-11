using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Domain.Entities.Common;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Domain.Entities.Identity;
using OHS_program_api.Domain.Entities.OccupationalHealth;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Domain.Entities.Trainings;

namespace OHS_program_api.Persistence.Contexts
{
    public class OHSProgramAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public OHSProgramAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Limb> Limbs { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<TypeOfAccident> TypeOfAccidents { get; set; }
        public DbSet<TypeOfAnalysis> TypeOfAnalyses { get; set; }
        public DbSet<TypeOfCertificate> TypeOfCertificates { get; set; }
        public DbSet<TypeOfDisease> TypeOfDiseases { get; set; }
        public DbSet<TypeOfSafetyEquipment> TypeOfEquipments { get; set; }
        public DbSet<TypeOfExamination> TypeOfExaminations { get; set; }
        public DbSet<TypeOfReport> TypeOfReports { get; set; }
        public DbSet<TypeOfWorkEquipment> TypeOfWorkEquipments { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<HealthPersonnel> HealthPersonnels { get; set; }
        public DbSet<HealthSurveillance> HealthSurveillances { get; set; }
        public DbSet<OccupationalDisease> OccupationalDiseases { get; set; }
        public DbSet<WorkplaceTestAndAnalysis> WorkplaceTestAndAnalyses { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<ActualWageAndPersonnelNumber> ActualWageAndPersonnelNumbers { get; set; }
        public DbSet<OHSComittee> OHSComittees { get; set; }
        public DbSet<PeriodicControl> PeriodicControls { get; set; }
        public DbSet<SafetyEquipment> SafetyEquipments { get; set; }
        public DbSet<SafetyExpert> SafetyExperts { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<TaskInstruction> TaskInstructions { get; set; }
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
