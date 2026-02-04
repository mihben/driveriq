using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace DriverIQ
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainWindow>();

            services.AddSingleton<IConfiguration>(_ =>
            {
                return new ConfigurationBuilder()
                                .AddJsonFile("Appsettings.json")
                                .AddJsonFile($"Appsettings.{Environment.GetEnvironmentVariable("Environment")}.json", optional: true)
                                .Build();
            });

            services.ConfigureServices();

            _serviceProvider = services.BuildServiceProvider();

            _serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }

}
