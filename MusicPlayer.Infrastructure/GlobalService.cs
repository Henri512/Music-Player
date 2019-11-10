using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MusicPlayer.Infrastructure
{
    public abstract class GlobalService
    {
        protected readonly IConfiguration _configuration;
        protected readonly string _blobStorageUrl;
        protected readonly string _defaultAlbumLogoImageUrl;
        protected readonly string _imageBlobFolderUrl;

        public GlobalService(IConfiguration configuration)
        {
            _configuration = configuration;
            var blobSection = _configuration.GetSection("BlobStorage");
            _blobStorageUrl = blobSection
                .GetValue<string>("SongsBlobUrl") ?? string.Empty;
            _defaultAlbumLogoImageUrl = blobSection
                .GetValue<string>("DefaultAlbumLogoImageUrl") ?? string.Empty;
            _imageBlobFolderUrl = blobSection
                .GetValue<string>("ImagesFolder") ?? string.Empty;
        }
        protected string[] GetAlbumImagesUrls(string[] albumImages)
        {
            return albumImages
                .Select(i => GetImageBlobFileUriLocation(i))
                .ToArray();
        }

        protected string GetBlobFileUriLocation(string fileName)
        {
            return new Uri(_blobStorageUrl +
                        fileName.Replace('\\', '/'))
                .OriginalString;
        }

        protected string GetImageBlobFileUriLocation(string fileName)
        {
            return new Uri(_blobStorageUrl + "/" + _imageBlobFolderUrl
                        + fileName.Replace('\\', '/'))
                .OriginalString;
        }
    }
}
