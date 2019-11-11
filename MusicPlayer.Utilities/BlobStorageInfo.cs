using System;
using System.Linq;

namespace MusicPlayer.Utilities
{
    public class BlobStorageInfo
    {
        public string Url { get; set; }

        public string AccountName { get; set; }

        public string AccountKey { get; set; }

        public string ContainerName { get; set; }

        public string ImagesFolder { get; set; }

        public static BlobStorageInfo CreateFromEnvironmentVariable(string environmentVariable)
        {
            var environmentPath = Environment
                .GetEnvironmentVariable(environmentVariable, EnvironmentVariableTarget.Machine);
            var sections = environmentPath.Split(';');
            var sectionsDictionary = sections
                .ToDictionary(k => k.Substring(0, k.IndexOf('=')), v => v.Substring(v.IndexOf('=') + 1));

            return new BlobStorageInfo
            {
                AccountKey = sectionsDictionary["AccountKey"],
                ContainerName = sectionsDictionary["ContainerName"],
                Url = sectionsDictionary["Url"],
                AccountName = sectionsDictionary["AccountName"],
                ImagesFolder = sectionsDictionary["ImagesFolder"]
            };
        }
    }
}
