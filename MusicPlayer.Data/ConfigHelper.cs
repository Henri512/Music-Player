using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;

namespace MusicPlayer.Data
{
    public class ConfigHelper
    {
        /// <summary>
        /// Gets the connectionstring from the appsettings.development.json file in the solution root if there is no environment variable to be found
        /// </summary>
        /// <param name="solutionBasePath">Optional to not auto resolve the solution base path</param>
        /// <returns></returns>
        public static string GetConnectionString(string solutionBasePath = null)
        {
            //how to set it on IIS on the server: https://stackoverflow.com/a/36836533/1343595
            var environmentString = Environment.GetEnvironmentVariable("ConnectionStrings__MusicPlayerCN");

            if (!string.IsNullOrEmpty(environmentString))
                return environmentString;

            if (!string.IsNullOrEmpty(solutionBasePath))
                return GetStringFromConfig(Path.Combine(solutionBasePath, "appsettings.development.json"));

            var filePath = Path.Combine(GetSolutionBasePath(), "appsettings.development.json");

            return GetStringFromConfig(filePath);
        }

        private static string GetStringFromConfig(string filePath)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile(filePath) //you can change the value of the connectionstring in the appsettings file and add it to gitignore so the change will not effect others
                .Build();

            var connectionString = config.GetConnectionString("MusicPlayerCN");
            return connectionString;
        }

        /// <summary>
        /// Gets the current solution base path
        /// </summary>
        /// <returns></returns>
        public static string GetSolutionBasePath()
        {
            var appPath = PlatformServices.Default.Application.ApplicationBasePath;
            var binPosition = appPath.IndexOf("\\bin", StringComparison.Ordinal);
            var basePath = appPath.Remove(binPosition);

            var backslashPosition = basePath.LastIndexOf("\\", StringComparison.Ordinal);
            basePath = basePath.Remove(backslashPosition);
            return basePath;
        }
    }
}
