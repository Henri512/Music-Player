using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MusicPlayer.Data.Entities;
using Serilog;
using FileIO = System.IO.File;

namespace MusicPlayer.StorageSync
{
    public class MediaService
    {
        private readonly IConfiguration _configuration;
        private List<string> _searchFolders;
        private IEnumerable<string> _extensions;
        private readonly ILogger _logger;

        public MediaService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;

            _searchFolders = _configuration
                .GetSection("SearchFolders")
                .Get<string[]>().ToList();

            _extensions = _configuration
                .GetValue<string>("Extensions")
                .Split(',')
                .Select(t => t.Trim(' '));
        }

        public void SynchronizeStorage()
        {
            _logger.Information("Starting synchronization...");
            _logger.Information(
                $"Parameters: \nSearchFolders: \"{string.Join(',', _searchFolders)}\"\nExtensions: {string.Join(',', _extensions)}\n");

            var blobService = new BlobService(_configuration, _logger);
            var dataService = new DataService(_configuration);

            _searchFolders.ForEach(directory =>
            {
                var directoryInfo = new DirectoryInfo(directory);
                var files = directoryInfo
                    .GetFilesByExtensions(_extensions.ToList())
                    .ToList();

                files.ForEach(fileInfo =>
                {
                    processOneFile(fileInfo, directory, blobService, dataService);
                });
            });

            _logger.Information("Synchronization finished...");
        }

        private void processOneFile(
            FileInfo fileInfo,
            string directory,
            BlobService blobService,
            DataService dataService)
        {
            var fileName = fileInfo.Name;
            var directoryAbove = Directory.GetParent(directory);
            var fileRelativePath = fileInfo.Directory.FullName
                .Substring(directoryAbove.FullName.Length);
            var blobFilePath = Path.Combine(fileRelativePath, fileName);

            var properties = ShellFile.FromFilePath(fileInfo.FullName).Properties;

            var songInfo = GetSongInfo(properties);

            songInfo.Extension = fileInfo.Extension;
            songInfo.RelativePath = fileRelativePath;

            //dataService.UpdateSongInfo(songInfo);

            //blobService.UpdateBlob(blobFilePath, fileInfo.FullName);
        }

        private static SongInfo GetSongInfo(ShellProperties properties)
        {
            var propertiesNames = properties.DefaultPropertyCollection
                .Select(t => t.CanonicalName)
                .ToList();

            var albumYear = properties
                    .DefaultPropertyCollection
                    .FirstOrDefault(p => p.CanonicalName.Equals("System.Media.Year"))
                    .ValueAsObject.ToString();

            var author = string.Join(';', properties.System.Author.Value);
            
            var trackNo = properties
                    .DefaultPropertyCollection
                    .FirstOrDefault(p => p.CanonicalName.Equals("System.Music.AlbumID"))
                    .ValueAsObject.ToString();

            var bitRate = properties
                .DefaultPropertyCollection
                .First(p => p.CanonicalName.Equals("System.Audio.EncodingBitrate"))
                .ValueAsObject.ToString();

            var genre = properties
                    .DefaultPropertyCollection
                    .FirstOrDefault(p => p.CanonicalName.Equals("System.Music.Genre"))
                    .ValueAsObject;

            var duration = properties
                    .DefaultPropertyCollection
                    .FirstOrDefault(p => p.CanonicalName.Equals("System.Media.Duration"))
                    .ValueAsObject.ToString();

            var songName = properties.System.OriginalFileName.Value;

            return new SongInfo
            {
                Album = new Album
                {
                    Year = new DateTime(int.Parse(albumYear), 1, 1)
                },
                Author = author,
                BitRate = bitRate.Substring(0, bitRate.Length - 3) + "kbps",
                Genre = string.Join(',', genre as string[]),
                Name = songName
            };
        }
    }
}
