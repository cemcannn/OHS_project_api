using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OHS_program_api.Domain.Entities.OccupationalSafety;
using OHS_program_api.Domain.Entities;

namespace OHS_program_api.Persistence.Extensions
{
    /// <summary>
    /// Veritabanı indeks optimizasyonları
    /// </summary>
    public static class IndexExtensions
    {
        /// <summary>
        /// Accident entity'si için performans indexleri ekler
        /// </summary>
        public static void ConfigureAccidentIndexes(this EntityTypeBuilder<Accident> builder)
        {
            // Tarih bazlı sorgular için index
            builder.HasIndex(a => a.AccidentDate)
                .HasDatabaseName("IX_Accidents_AccidentDate");

            // PersonnelId için index (FK)
            builder.HasIndex(a => a.PersonnelId)
                .HasDatabaseName("IX_Accidents_PersonnelId");

            // Composite index - tarih + personel
            builder.HasIndex(a => new { a.AccidentDate, a.PersonnelId })
                .HasDatabaseName("IX_Accidents_Date_Personnel");
        }

        /// <summary>
        /// Personnel entity'si için performans indexleri ekler
        /// </summary>
        public static void ConfigurePersonnelIndexes(this EntityTypeBuilder<Personnel> builder)
        {
            // Ad soyad arama için index
            builder.HasIndex(p => new { p.Name, p.Surname })
                .HasDatabaseName("IX_Personnels_Name");

            // TC kimlik no için unique index
            builder.HasIndex(p => p.TRIdNumber)
                .IsUnique()
                .HasDatabaseName("IX_Personnels_TRIdNumber");

            // Meslek bazlı sorgular için index (Profession string olduğu için)
            builder.HasIndex(p => p.Profession)
                .HasDatabaseName("IX_Personnels_Profession");

            // Doğum tarihi için index
            builder.HasIndex(p => p.BornDate)
                .HasDatabaseName("IX_Personnels_BornDate");
        }
    }
}
