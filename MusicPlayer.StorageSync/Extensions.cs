using System;
using System.Collections.Generic;
using System.IO;

namespace MusicPlayer.StorageSync
{
    public static class Extensions
    {
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo directory, List<string> extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            var files = new List<FileInfo>();
            extensions.ForEach(extension =>
            {
                files.AddRange(
                    directory
                        .EnumerateFiles("*" + extension, SearchOption.AllDirectories));
            });

            return files;
        }
    }
}
