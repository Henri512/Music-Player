using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicPlayer.FileInfoManager
{
    public class DirectoryHelper
    {
        public List<string> GetFiles(string searchFolder,
                                        string[] filters,
                                        bool isRecursive)
        {
            var filesFound = new List<string>();
            var searchOption = isRecursive ?
                SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory
                    .GetFiles(searchFolder, string.Format("*.{0}", filter),
                    searchOption));
            }

            return filesFound;
        }

        public List<string> GetFiles(DirectoryInfo searchDirectory,
                                        string[] filters,
                                        bool isRecursive = false)
        {
            var filesFound = new List<string>();
            var searchOptions = isRecursive ?
                SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(searchDirectory
                    .GetFiles($"*.{filter}", searchOptions)
                    .Select(t =>
                    t.FullName
                    .Substring(searchDirectory.FullName.Length)
                    .TrimStart('\\')));
            }

            return filesFound;
        }
    }
}
