using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Core.Albums;
using MusicPlayer.Core.SongInfos;
using MusicPlayer.FileInfoManager;
using Serilog;

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

            var blobService = new BlobService(_logger);
            var dataService = new DataService(_configuration);

            _searchFolders.ForEach(directory =>
            {
                var directoryInfo = new DirectoryInfo(directory);
                var files = directoryInfo
                    .GetFilesByExtensions(_extensions.ToList())
                    .ToList();

                files.ForEach(fileInfo =>
                {
                    ProcessOneFile(fileInfo, directory, blobService, dataService);
                });
            });

            _logger.Information("Synchronization finished...");
        }

        private void ProcessOneFile(
            FileInfo fileInfo,
            string directory,
            BlobService blobService,
            DataService dataService)
        {
            try
            {
                var fileName = fileInfo.Name;
                var directoryAbove = Directory.GetParent(directory);
                var fileRelativePath = fileInfo.Directory?.FullName
                    .Substring(directoryAbove.FullName.Length);
                var blobFilePath = Path
                    .Combine(fileRelativePath, fileName)
                    .TrimStart('/', '\\');
                fileRelativePath = Path
                    .Combine(fileRelativePath, fileInfo.Name);

                var songInfo = GetSongInfo(fileInfo, dataService, fileRelativePath);

                songInfo.Extension = fileInfo.Extension.TrimStart('.');
                songInfo.RelativePath = fileRelativePath;

                dataService.UpdateSongInfo(songInfo);

                blobService.UpdateBlob(blobFilePath, fileInfo.FullName);

                if (!string.IsNullOrEmpty(songInfo.Album.ImagePath))
                {
                    songInfo.Album.ImagePath.Split(';')
                        .ToList().ForEach(i =>
                        {
                            var physicalPath = Path
                            .Combine(fileInfo.DirectoryName, i);
                            blobService
                            .UpdateImageBlob(i, physicalPath);
                        });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.Error(e, "Error occurred while processing file!");
            }
        }

        private SongInfo GetSongInfo(
            FileInfo fileInfo,
            DataService dataService,
            string relativePath)
        {
            var mediaPropertiesService = new MediaPropertiesService(fileInfo);
            var author = mediaPropertiesService.GetAuthor();
            var songName = mediaPropertiesService
                .GetSongName(author)?
                .Replace(fileInfo.Extension, "");

            var albumName = mediaPropertiesService.GetAlbum();

            var existingSong = dataService.GetSong(songName, relativePath);
            var songInfo = existingSong ?? new SongInfo
            {
                Album = dataService.GetAlbum(albumName, author) ?? new Album()
            };

            songInfo.Author = author;
            songInfo.Name = songName;

            songInfo.Album.Name = albumName;
            songInfo.Album.Author = author;

            songInfo.Album.Year = mediaPropertiesService
                .GetAlbumYear(songInfo.Album.Name, fileInfo.DirectoryName);

            songInfo.TrackNo = mediaPropertiesService.GetTrackNo();

            songInfo.BitRate = mediaPropertiesService.GetBitRate();

            songInfo.Genre = mediaPropertiesService.GetGenre();

            songInfo.Duration = mediaPropertiesService.GetDuration();

            songInfo.Album.ImagePath = mediaPropertiesService
                .GetImagePath(relativePath.Replace(fileInfo.Name, ""));

            return songInfo;
        }
    }
}
