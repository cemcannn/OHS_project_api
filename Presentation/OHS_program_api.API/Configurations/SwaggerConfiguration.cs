using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OHS_program_api.API.Configurations
{
    /// <summary>
    /// OpenAPI/Swagger konfigürasyonu
    /// </summary>
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // API bilgileri
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OHS Program API",
                    Version = "1.0",
                    Description = "İş Sağlığı ve Güvenliği (OSGB) Yönetim Sistemi API'si",
                    Contact = new OpenApiContact
                    {
                        Name = "OHS Team",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // JWT Bearer token desteği
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                // XML comments desteği
                var basePath = AppContext.BaseDirectory;
                var xmlFile = "OHS_program_api.API.xml";
                var xmlPath = Path.Combine(basePath, xmlFile);

                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                // Endpoint siparişini al
                options.OrderActionsBy(x => x.RelativePath);

                // Deprecated endpoint'ler işaretleme
                options.OperationFilter<DeprecatedOperationFilter>();
            });
        }

        public static void UseSwaggerDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "swagger/{documentName}/swagger.json";
                });

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OHS Program API v1");
                    c.RoutePrefix = "swagger";
                    c.EnablePersistAuthorization();
                    c.DisplayOperationId();
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                });
            }
        }
    }

    /// <summary>
    /// Deprecated endpoint'leri işaretleyen filter
    /// </summary>
    public class DeprecatedOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var isDeprecated = context.MethodInfo
                .GetCustomAttributes(false)
                .OfType<ObsoleteAttribute>()
                .Any();

            if (isDeprecated)
            {
                operation.Deprecated = true;
                operation.Description += " (Deprecated)";
            }
        }
    }
}
