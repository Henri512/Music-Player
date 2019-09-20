using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Linq;

namespace MusicPlayer.StorageSync
{
    class Program
    {
        private static ILogger _logger;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console()
                .CreateLogger();

            _logger = Log.Logger;
            try
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();

                var blobStorageLocation = configuration
                    .GetSection("BlobStorages")
                    .GetValue<string>("SongsBlobUrl");

                var searchFolders = configuration
                    .GetSection("SearchFolders")
                    .Get<string[]>();

                var extensions = configuration
                    .GetValue<string>("Extensions")
                    .Split(',')
                    .Select(t => t.Trim(' '));

                var mediaService = new MediaService(configuration, _logger);

                mediaService.SynchronizeStorage();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error ocurred during the synchronization.");
            }
        }
    }
}
