using Microsoft.Extensions.DependencyInjection;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.Abstractions.Services.Configurations;
using OHS_program_api.Application.Abstractions.Storage;
using OHS_program_api.Application.Abstractions.Token;
using OHS_program_api.Infrastructure.Enums;
using OHS_program_api.Infrastructure.Services;
using OHS_program_api.Infrastructure.Services.Configurations;
using OHS_program_api.Infrastructure.Services.Storage;
using OHS_program_api.Infrastructure.Services.Storage.Azure;
using OHS_program_api.Infrastructure.Services.Storage.Local;
using OHS_program_api.Infrastructure.Services.Token;
using OHS_program_api.Infrastructure.Services.ExcelImport;

namespace OHS_program_api.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IApplicationService, ApplicationService>();
            
            // Excel Import Servisleri
            serviceCollection.AddScoped<IExcelReaderService, ExcelReaderService>();
            serviceCollection.AddScoped<IExcelImportService, ExcelImportService>();
            serviceCollection.AddHostedService<ExcelAutoImportBackgroundService>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
