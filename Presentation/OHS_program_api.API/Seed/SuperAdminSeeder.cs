using Microsoft.AspNetCore.Identity;
using OHS_program_api.Domain.Entities.Identity;

namespace OHS_program_api.API.Seed
{
    public static class SuperAdminSeeder
    {
        private const string SuperAdminRoleName = "SuperAdmin";
        private const string AdminRoleName = "Admin";
        private const string ObserverRoleName = "Observer";

        /// <summary>
        /// Development ortamında super admin kullanıcı/rol seed etmek için.
        /// Şifreyi repo içine koymamak için env/config üzerinden okunur.
        /// </summary>
        public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration, ILogger logger)
        {
            // Enable flag (opsiyonel). True değilse sadece password varsa seed ederiz.
            var enabled = configuration.GetValue<bool?>("SuperAdmin:Seed:Enabled");

            // Şifreyi öncelikle env var ile bekleyelim.
            // Örnek: SUPERADMIN_PASSWORD=...
            var password =
                Environment.GetEnvironmentVariable("SUPERADMIN_PASSWORD")
                ?? configuration["SuperAdmin:Seed:Password"];

            var userName =
                Environment.GetEnvironmentVariable("SUPERADMIN_USERNAME")
                ?? configuration["SuperAdmin:Seed:UserName"]
                ?? "superadmin";

            var email =
                Environment.GetEnvironmentVariable("SUPERADMIN_EMAIL")
                ?? configuration["SuperAdmin:Seed:Email"]
                ?? "superadmin@local";

            var displayName =
                Environment.GetEnvironmentVariable("SUPERADMIN_NAME")
                ?? configuration["SuperAdmin:Seed:Name"]
                ?? "Super Admin";

            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            var roleNames = new[] { SuperAdminRoleName, AdminRoleName, ObserverRoleName };

            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName))
                    continue;

                var roleResult = await roleManager.CreateAsync(new AppRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = roleName
                });

                if (!roleResult.Succeeded)
                {
                    logger.LogWarning("{RoleName} rolü oluşturulamadı: {Errors}", roleName, string.Join(" | ", roleResult.Errors.Select(e => e.Description)));
                    return;
                }
            }

            if (enabled != true && string.IsNullOrWhiteSpace(password))
            {
                logger.LogInformation("SuperAdmin kullanıcı seed atlandı (SUPERADMIN_PASSWORD yok). Standart roller hazırlandı.");
                return;
            }

            // User (email veya username ile bul)
            var user = await userManager.FindByNameAsync(userName) ?? await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userName,
                    Email = email,
                    Name = displayName
                };

                var createResult = await userManager.CreateAsync(user, password!);
                if (!createResult.Succeeded)
                {
                    logger.LogWarning("SuperAdmin kullanıcı oluşturulamadı: {Errors}", string.Join(" | ", createResult.Errors.Select(e => e.Description)));
                    return;
                }

                logger.LogInformation("SuperAdmin kullanıcı oluşturuldu. userName={UserName} email={Email}", userName, email);
            }

            // Role assignment
            if (!await userManager.IsInRoleAsync(user, SuperAdminRoleName))
            {
                var addRoleResult = await userManager.AddToRoleAsync(user, SuperAdminRoleName);
                if (!addRoleResult.Succeeded)
                {
                    logger.LogWarning("SuperAdmin rol ataması başarısız: {Errors}", string.Join(" | ", addRoleResult.Errors.Select(e => e.Description)));
                    return;
                }
            }

            logger.LogInformation("SuperAdmin seed tamamlandı. userName={UserName}", user.UserName);
        }
    }
}

