using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Texas.Notifications;

public static class NotificationExtensions
{
    public static IServiceCollection AddLineNotifications(this IServiceCollection services)
    {
        return services;
    }
}
