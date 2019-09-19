using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Events;

namespace MusicPlayer.StorageSync
{
    class Program
    {
        private static ILogger logger;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console()
                .CreateLogger();

            logger = Log.Logger;
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

                var mediaService = new MediaService(configuration);

                mediaService.SynchronizeStorage();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error ocurred during the synchronization.");
            }
        }
    }
}
