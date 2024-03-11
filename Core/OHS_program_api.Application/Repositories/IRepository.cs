using Microsoft.EntityFrameworkCore;
using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
