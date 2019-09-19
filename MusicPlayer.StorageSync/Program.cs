using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicPlayer.StorageSync
{
    class Program
    {
        private static ILogger logger;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("consoleapp.log")
            .CreateLogger();

            logger = Log.Logger;
            try
            {
                var builder = new ConfigurationBuilder()
                       .AddJsonFile("storageConfig.json", optional: false, reloadOnChange: true);

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

                SynchronizeStorage(blobStorageLocation, searchFolders, extensions);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error ocurred during the synchronization.");
            }

        }

        private static void SynchronizeStorage(string blobStorageLocation, string[] searchFolders, IEnumerable<string> extensions)
        {
            searchFolders.ToList().ForEach((f) =>
            {
                var files = Directory.GetFiles(f, string.Join(',', extensions.Select(t => "*." + t)));

                files.AsParallel().ForAll((file) =>
                {
                    //file
                });
            });
        }
    }
}
