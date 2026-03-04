using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OHS_program_api.Application.Behaviors;

namespace OHS_program_api.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistration));
            collection.AddHttpClient();
            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }
    }
}
