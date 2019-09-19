using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Domain.Services;
using FileIO = System.IO.File;

namespace MusicPlayer.StorageSync
{
    public class MediaService
    {
        private readonly IConfiguration _configuration;
        private string[] _searchFolders;
        private IEnumerable<string> _extensions;

        public MediaService(IConfiguration configuration)
        {
            _configuration = configuration;

            _searchFolders = _configuration
                .GetSection("SearchFolders")
                .Get<string[]>();

            _extensions = _configuration
                .GetValue<string>("Extensions")
                .Split(',')
                .Select(t => t.Trim(' '));
        }

        public void SynchronizeStorage()
        {
            var blobService = new BlobService(_configuration);

            _searchFolders.ToList().ForEach(f =>
            {
                var directoryInfo = new DirectoryInfo(f);
                var files = directoryInfo
                    .GetFilesByExtensions(_extensions.ToList());

                files.AsParallel().ForAll(file =>
                {
                    //file
                    var attributes = file.Attributes;
                });
            });
        }
    }
}
