using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Texas.Modules;

public static class ApplicationExtensions
{
    public static IServiceCollection AddMediators(this IServiceCollection services)
    {
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly));
        
        return services;
    }
}
