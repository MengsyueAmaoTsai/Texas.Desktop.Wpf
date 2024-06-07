using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Texas.Domain;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<ITexasService, TexasService>();

        return services;
    }
}
