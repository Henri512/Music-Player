using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace MusicPlayer.StorageSync
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.development.json",
                                optional: false, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();

                var logFilePath = configuration
                    .GetSection("Logging")
                    .GetValue<string>("LogFilePath");

                Log.Logger = new LoggerConfiguration()
                    .WriteTo
                    .Console()
                    .WriteTo
                    .File(logFilePath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                var mediaService = new MediaService(configuration, Log.Logger);

                mediaService.SynchronizeStorage();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "An error ocurred during the synchronization.");
            }
        }
    }
}
