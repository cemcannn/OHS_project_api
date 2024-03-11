using OHS_program_api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OHS_program_api.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OHSProgramAPIDbContext>
    {
        public OHSProgramAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<OHSProgramAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configurations.ConnectionString);
            return new OHSProgramAPIDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
