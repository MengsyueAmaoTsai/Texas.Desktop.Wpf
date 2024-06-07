using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using RichillCapital.Texas.Domain;
using RichillCapital.Texas.Modules;

namespace RichillCapital.Texas.Desktop;

public partial class App : Application
{
    [STAThread]
    private static void Main(string[] args) => StartAsync(args).GetAwaiter().GetResult();

    private static async Task StartAsync(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();

        await host.StartAsync().ConfigureAwait(true);

        var app = new App();

        app.InitializeComponent();

        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Show();

        app.Run();

        await host
            .StopAsync()
            .ConfigureAwait(true);
    }

    private static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            configurationBuilder.AddUserSecrets(typeof(App).Assembly))
        .ConfigureServices((hostContext, services) =>
        {
            services.AddDomainServices();

            services.AddMediators();

            services.AddControls();
        });
}
