using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DriverIQ.ViewModels
{
    public class MainViewModel
    {
        public string Environment { get; set; } = null!;

        public MainViewModel(IConfiguration configuration, ILogger<MainViewModel> logger)
        {
            Environment = configuration.GetSection("Environment").Value!;
            logger.LogDebug("Application has been started");
        }
    }
}
