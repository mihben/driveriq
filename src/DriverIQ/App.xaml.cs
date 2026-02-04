using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Settings.Configuration;
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

            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("Appsettings.json")
                                .AddJsonFile($"Appsettings.{Environment.GetEnvironmentVariable("Environment")}.json", optional: true)
                                .Build();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddLogging(builder => builder.AddSerilog(new LoggerConfiguration()
                                                                    .WriteTo
                                                                        .EventLog(source: "DriverIQ", manageEventSource: true)
                                                                    .ReadFrom
                                                                        .Configuration(configuration, new ConfigurationReaderOptions { SectionName = "Logging" }).CreateLogger()));

            services.ConfigureServices();

            _serviceProvider = services.BuildServiceProvider();

            _serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }

}
