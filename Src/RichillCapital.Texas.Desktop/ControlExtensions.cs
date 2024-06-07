using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Texas.Desktop;

internal static class ControlExtensions
{
    internal static IServiceCollection AddControls(this IServiceCollection services)
    {
        services.AddSingleton<IDialogService, DialogService>();

        services.AddSingleton<MainWindow>();
        services.AddTransient<CreatePlayerDialog>();
        services.AddTransient<BuyInDialog>();
        services.AddTransient<CashOutDialog>();

        services.AddSingleton<MainViewModel>();
        services.AddTransient<CreatePlayerViewModel>();
        services.AddTransient<BuyInViewModel>();
        services.AddTransient<CashOutViewModel>();

        services.AddSingleton<WeakReferenceMessenger>();
        services.AddSingleton<IMessenger, WeakReferenceMessenger>(provider =>
            provider.GetRequiredService<WeakReferenceMessenger>());

        services.AddSingleton(_ => App.Current.Dispatcher);

        return services;
    }
}