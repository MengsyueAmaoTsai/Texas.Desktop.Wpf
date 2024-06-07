using Microsoft.Extensions.DependencyInjection;
using RichillCapital.Texas.Domain.Common;
using RichillCapital.Texas.Domain.Services;

namespace RichillCapital.Texas.Domain;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<ITexasService, TexasService>();

        return services;
    }
}
