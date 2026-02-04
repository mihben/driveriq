using Microsoft.Extensions.Configuration;

namespace DriverIQ.ViewModels
{
    public class MainViewModel
    {
        public string Environment { get; set; } = null!;

        public MainViewModel(IConfiguration configuration)
        {
            Environment = configuration.GetSection("Environment").Value!;
        }
    }
}
