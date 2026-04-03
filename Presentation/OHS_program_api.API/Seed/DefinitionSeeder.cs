using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Domain.Entities.Definitions;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.API.Seed
{
    public static class DefinitionSeeder
    {
        private const int CurrentVersion = 1;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration, ILogger logger)
        {
            var enabled = configuration.GetValue<bool?>("DefinitionSeed:Enabled") ?? true;
            if (!enabled)
            {
                logger.LogInformation("Definition seed disabled via configuration.");
                return;
            }

            var dbContext = services.GetRequiredService<OHSProgramAPIDbContext>();

            await EnsureDirectorateCodeColumnAsync(dbContext);
            await EnsureSeedHistoryTableAsync(dbContext);

            var assembly = typeof(DefinitionSeeder).Assembly;
            var accidentAreas = ReadSeedItems(assembly, "accident_areas.json");
            var typeOfAccidents = ReadSeedItems(assembly, "type_of_accident.json");
            var professions = ReadSeedItems(assembly, "professions.json");
            var limbs = ReadSeedItems(assembly, "limbs.json");

            var areaResult = await SeedAccidentAreasAsync(dbContext, accidentAreas);
            var typeResult = await SeedTypeOfAccidentAsync(dbContext, typeOfAccidents);
            var professionResult = await SeedProfessionsAsync(dbContext, professions);
            var limbResult = await SeedLimbsAsync(dbContext, limbs);

            await UpsertSeedHistoryAsync(dbContext, "AccidentAreas", areaResult.inserted, areaResult.skipped);
            await UpsertSeedHistoryAsync(dbContext, "TypeOfAccident", typeResult.inserted, typeResult.skipped);
            await UpsertSeedHistoryAsync(dbContext, "Professions", professionResult.inserted, professionResult.skipped);
            await UpsertSeedHistoryAsync(dbContext, "Limbs", limbResult.inserted, limbResult.skipped);

            logger.LogInformation(
                "Definition seed completed. AccidentAreas +{AreaInserted} (skip {AreaSkipped}), TypeOfAccident +{TypeInserted} (skip {TypeSkipped}), Professions +{ProfessionInserted} (skip {ProfessionSkipped}), Limbs +{LimbInserted} (skip {LimbSkipped})",
                areaResult.inserted,
                areaResult.skipped,
                typeResult.inserted,
                typeResult.skipped,
                professionResult.inserted,
                professionResult.skipped,
                limbResult.inserted,
                limbResult.skipped);
        }

        private static List<DefinitionSeedItem> ReadSeedItems(Assembly assembly, string fileName)
        {
            var resourceName = assembly
                .GetManifestResourceNames()
                .FirstOrDefault(name => name.EndsWith($".Seed.Data.{fileName}", StringComparison.OrdinalIgnoreCase));

            if (resourceName is null)
                throw new InvalidOperationException($"Seed resource not found: {fileName}");

            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Seed resource stream not found: {resourceName}");
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            return JsonSerializer.Deserialize<List<DefinitionSeedItem>>(json, JsonOptions) ?? new List<DefinitionSeedItem>();
        }

        private static async Task<(int inserted, int skipped)> SeedAccidentAreasAsync(OHSProgramAPIDbContext dbContext, IReadOnlyCollection<DefinitionSeedItem> items)
        {
            var existingCodes = await ReadCodesAsync(dbContext.AccidentAreas.AsNoTracking().Select(x => x.Description));
            var inserted = 0;
            var skipped = 0;

            foreach (var item in items)
            {
                var code = Normalize(item.Code);
                if (string.IsNullOrEmpty(code) || existingCodes.Contains(code))
                {
                    skipped++;
                    continue;
                }

                await dbContext.AccidentAreas.AddAsync(new AccidentArea
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Description = BuildDescription(item)
                });

                existingCodes.Add(code);
                inserted++;
            }

            if (inserted > 0)
                await dbContext.SaveChangesAsync();

            return (inserted, skipped);
        }

        private static async Task<(int inserted, int skipped)> SeedTypeOfAccidentAsync(OHSProgramAPIDbContext dbContext, IReadOnlyCollection<DefinitionSeedItem> items)
        {
            var existingCodes = await ReadCodesAsync(dbContext.TypeOfAccident.AsNoTracking().Select(x => x.Description));
            var inserted = 0;
            var skipped = 0;

            foreach (var item in items)
            {
                var code = Normalize(item.Code);
                if (string.IsNullOrEmpty(code) || existingCodes.Contains(code))
                {
                    skipped++;
                    continue;
                }

                await dbContext.TypeOfAccident.AddAsync(new TypeOfAccident
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Description = BuildDescription(item)
                });

                existingCodes.Add(code);
                inserted++;
            }

            if (inserted > 0)
                await dbContext.SaveChangesAsync();

            return (inserted, skipped);
        }

        private static async Task<(int inserted, int skipped)> SeedProfessionsAsync(OHSProgramAPIDbContext dbContext, IReadOnlyCollection<DefinitionSeedItem> items)
        {
            var existingCodes = await ReadCodesAsync(dbContext.Professions.AsNoTracking().Select(x => x.Description));
            var inserted = 0;
            var skipped = 0;

            foreach (var item in items)
            {
                var code = Normalize(item.Code);
                if (string.IsNullOrEmpty(code) || existingCodes.Contains(code))
                {
                    skipped++;
                    continue;
                }

                await dbContext.Professions.AddAsync(new Profession
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    WorkType = item.WorkType,
                    Description = BuildDescription(item)
                });

                existingCodes.Add(code);
                inserted++;
            }

            if (inserted > 0)
                await dbContext.SaveChangesAsync();

            return (inserted, skipped);
        }

        private static async Task<(int inserted, int skipped)> SeedLimbsAsync(OHSProgramAPIDbContext dbContext, IReadOnlyCollection<DefinitionSeedItem> items)
        {
            var existingCodes = await ReadCodesAsync(dbContext.Limbs.AsNoTracking().Select(x => x.Description));
            var inserted = 0;
            var skipped = 0;

            foreach (var item in items)
            {
                var code = Normalize(item.Code);
                if (string.IsNullOrEmpty(code) || existingCodes.Contains(code))
                {
                    skipped++;
                    continue;
                }

                await dbContext.Limbs.AddAsync(new Limb
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Description = BuildDescription(item)
                });

                existingCodes.Add(code);
                inserted++;
            }

            if (inserted > 0)
                await dbContext.SaveChangesAsync();

            return (inserted, skipped);
        }

        private static async Task<HashSet<string>> ReadCodesAsync(IQueryable<string?> descriptions)
        {
            var codes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var allDescriptions = await descriptions.ToListAsync();

            foreach (var description in allDescriptions)
            {
                var code = ExtractCode(description);
                if (!string.IsNullOrEmpty(code))
                    codes.Add(code);
            }

            return codes;
        }

        private static string? ExtractCode(string? description)
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

        private static string BuildDescription(DefinitionSeedItem item)
        {
            var lines = new List<string>();

            var code = Normalize(item.Code);
            if (!string.IsNullOrWhiteSpace(code))
                lines.Add($"__code__:{code}");

            var parentCode = Normalize(item.ParentCode);
            if (!string.IsNullOrWhiteSpace(parentCode))
                lines.Add($"__parent_code__:{parentCode}");

            var workType = Normalize(item.WorkType);
            if (!string.IsNullOrWhiteSpace(workType))
                lines.Add($"__worktype__:{workType}");

            var description = Normalize(item.Description);
            if (!string.IsNullOrWhiteSpace(description))
                lines.Add(description);

            return string.Join('\n', lines);
        }

        private static string? Normalize(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private static async Task EnsureSeedHistoryTableAsync(OHSProgramAPIDbContext dbContext)
        {
            const string sql = @"
CREATE TABLE IF NOT EXISTS ""DefinitionSeedHistory"" (
    ""SeedName"" text PRIMARY KEY,
    ""Version"" integer NOT NULL,
    ""InsertedCount"" integer NOT NULL,
    ""SkippedCount"" integer NOT NULL,
    ""LastAppliedUtc"" timestamp with time zone NOT NULL
);";

            await dbContext.Database.ExecuteSqlRawAsync(sql);
        }

        private static async Task EnsureDirectorateCodeColumnAsync(OHSProgramAPIDbContext dbContext)
        {
            const string sql = @"
ALTER TABLE IF EXISTS ""Directorates""
ADD COLUMN IF NOT EXISTS ""Code"" text;

CREATE UNIQUE INDEX IF NOT EXISTS ""IX_Directorates_Code""
ON ""Directorates"" (""Code"");";

            await dbContext.Database.ExecuteSqlRawAsync(sql);
        }

        private static async Task UpsertSeedHistoryAsync(OHSProgramAPIDbContext dbContext, string seedName, int insertedCount, int skippedCount)
        {
            await dbContext.Database.ExecuteSqlInterpolatedAsync($@"
INSERT INTO ""DefinitionSeedHistory"" (""SeedName"", ""Version"", ""InsertedCount"", ""SkippedCount"", ""LastAppliedUtc"")
VALUES ({seedName}, {CurrentVersion}, {insertedCount}, {skippedCount}, {DateTime.UtcNow})
ON CONFLICT (""SeedName"")
DO UPDATE SET
    ""Version"" = EXCLUDED.""Version"",
    ""InsertedCount"" = EXCLUDED.""InsertedCount"",
    ""SkippedCount"" = EXCLUDED.""SkippedCount"",
    ""LastAppliedUtc"" = EXCLUDED.""LastAppliedUtc"";");
        }
    }
}
