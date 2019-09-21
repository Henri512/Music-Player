using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Data.Entities;
using MusicPlayer.Domain.Profiles;
using MusicPlayer.FileInfoManager;
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
            var dataService = new DataService(_configuration,
                new Mapper(new MapperConfiguration(t => t.AddMaps(typeof(AlbumModelProfile).Assembly))));

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
            try
            {
                var fileName = fileInfo.Name;
                var directoryAbove = Directory.GetParent(directory);
                var fileRelativePath = fileInfo.Directory.FullName
                    .Substring(directoryAbove.FullName.Length);
                var blobFilePath = Path.Combine(fileRelativePath, fileName);

                var songInfo = GetSongInfo(fileInfo);

                songInfo.Extension = fileInfo.Extension;
                songInfo.RelativePath = fileRelativePath;
                songInfo.Album.ImagePath = Path.Combine(blobService.GetBlobUrl() + fileRelativePath);

                dataService.UpdateSongInfo(songInfo);

                blobService.UpdateBlob(blobFilePath, fileInfo.FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.Error(e, "Error occurred while processing file!");
            }
        }

        private SongInfo GetSongInfo(FileInfo fileInfo)
        {
            var mediaPropertiesService = new MediaPropertiesService(fileInfo);

            var author = mediaPropertiesService.GetAuthor(); ;

            var album = mediaPropertiesService.GetAlbum();

            var albumYear = mediaPropertiesService.GetAlbumYear(album);

            var trackNo = mediaPropertiesService.GetTrackNo();

            var bitRate = mediaPropertiesService.GetBitRate();

            var genre = mediaPropertiesService.GetGenre();

            var duration = mediaPropertiesService.GetDuration();

            var songName = mediaPropertiesService.GetSongName(author);

            return new SongInfo
            {
                Album = new Album
                {
                    Name = album,
                    Year = albumYear
                },
                Author = author,
                BitRate = bitRate.Substring(0, bitRate.Length - 3) + "kbps",
                Genre = genre,
                Name = songName,
                Duration = duration,
                TrackNo = trackNo
            };
        }
    }
}
