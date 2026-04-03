using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Application.Helpers
{
    public static class DefinitionValidationHelper
    {
        public static string? ExtractCode(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return null;

            foreach (var rawLine in description.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                if (rawLine.StartsWith("__code__:", StringComparison.OrdinalIgnoreCase))
                    return Normalize(rawLine["__code__:".Length..]);
            }

            return null;
        }

        public static bool HasCode(string? description)
            => !string.IsNullOrWhiteSpace(ExtractCode(description));

        public static string? Normalize(string? value)
            => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        public static string? NormalizeForComparison(string? value)
            => Normalize(value)?.ToUpperInvariant();

        public static async Task<bool> HasDuplicateNameAsync<TEntity>(IReadRepository<TEntity> repository, string? name, string? excludedId = null)
            where TEntity : BaseEntity
        {
            var normalizedName = NormalizeForComparison(name);
            if (string.IsNullOrWhiteSpace(normalizedName))
                return false;

            var records = await repository.GetAll(false).ToListAsync();
            return records.Any(record =>
                record.Id.ToString() != excludedId &&
                NormalizeForComparison(GetName(record)) == normalizedName);
        }

        public static async Task<bool> HasDuplicateDescriptionCodeAsync<TEntity>(IReadRepository<TEntity> repository, string? description, string? excludedId = null)
            where TEntity : BaseEntity
        {
            var normalizedCode = NormalizeForComparison(ExtractCode(description));
            if (string.IsNullOrWhiteSpace(normalizedCode))
                return false;

            var records = await repository.GetAll(false).ToListAsync();
            return records.Any(record =>
                record.Id.ToString() != excludedId &&
                NormalizeForComparison(ExtractCode(GetDescription(record))) == normalizedCode);
        }

        public static async Task<bool> HasDuplicateCodeAsync<TEntity>(IReadRepository<TEntity> repository, string? code, string? excludedId = null)
            where TEntity : BaseEntity
        {
            var normalizedCode = NormalizeForComparison(code);
            if (string.IsNullOrWhiteSpace(normalizedCode))
                return false;

            var records = await repository.GetAll(false).ToListAsync();
            return records.Any(record =>
                record.Id.ToString() != excludedId &&
                NormalizeForComparison(GetCode(record)) == normalizedCode);
        }

        private static string? GetName<TEntity>(TEntity entity)
            where TEntity : BaseEntity
            => entity switch
            {
                Domain.Entities.Definitions.TypeOfAccident typeOfAccident => typeOfAccident.Name,
                Domain.Entities.Definitions.Profession profession => profession.Name,
                Domain.Entities.Definitions.Limb limb => limb.Name,
                Domain.Entities.Definitions.AccidentArea accidentArea => accidentArea.Name,
                Domain.Entities.Definitions.Directorate directorate => directorate.Name,
                _ => null
            };

        private static string? GetDescription<TEntity>(TEntity entity)
            where TEntity : BaseEntity
            => entity switch
            {
                Domain.Entities.Definitions.TypeOfAccident typeOfAccident => typeOfAccident.Description,
                Domain.Entities.Definitions.Profession profession => profession.Description,
                Domain.Entities.Definitions.Limb limb => limb.Description,
                Domain.Entities.Definitions.AccidentArea accidentArea => accidentArea.Description,
                _ => null
            };

        private static string? GetCode<TEntity>(TEntity entity)
            where TEntity : BaseEntity
            => entity switch
            {
                Domain.Entities.Definitions.Directorate directorate => directorate.Code,
                _ => null
            };
    }
}