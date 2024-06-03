using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.Abstractions.Services.Authentications;
using OHS_program_api.Application.Abstractions.Services.Safety;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Domain.Entities.Identity;
using OHS_program_api.Persistence.Contexts;
using OHS_program_api.Persistence.Repositories;
using OHS_program_api.Persistence.Repositories.Definition.TypeOfAccidentRepository;
using OHS_program_api.Persistence.Repositories.Endpoint;
using OHS_program_api.Persistence.Repositories.Menu;
using OHS_program_api.Persistence.Repositories.Safety.AccidentRepository;
using OHS_program_api.Persistence.Services;
using OHS_program_api.Persistence.Services.Safety;

namespace OHS_program_api.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<OHSProgramAPIDbContext>(options => options.UseSqlServer(Configurations.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<OHSProgramAPIDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
            services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            services.AddScoped<IAccidentReadRepository, AccidentReadRepository>();
            services.AddScoped<IAccidentWriteRepository, AccidentWriteRepository>();
            services.AddScoped<IPersonnelReadRepository, PersonnelReadRepository>();
            services.AddScoped<IPersonnelWriteRepository, PersonnelWriteRepository>();
            services.AddScoped<ITypeOfAccidentReadRepository, TypeOfAccidentReadRepository>();
            services.AddScoped<ITypeOfAccidentWriteRepository, TypeOfAccidentWriteRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPersonnelService, PersonnelService>();
            services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();
            services.AddScoped<IAccidentService, AccidentService>();

        }
    }
}
