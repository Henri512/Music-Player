using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Utilities;

namespace MusicPlayer.Infrastructure
{
    public abstract class GlobalService
    {
        protected readonly IConfiguration Configuration;
        protected readonly string BlobStorageUrl;
        protected readonly string DefaultAlbumLogoImageUrl;
        protected readonly string ImageBlobFolderUrl;

        protected GlobalService(IConfiguration configuration)
        {
            Configuration = configuration;
            BlobStorageInfo blobStorageInfo = BlobStorageInfo.CreateFromEnvironmentVariable("Blobs__MusicPlayer");
            BlobStorageUrl = blobStorageInfo.Url;
            DefaultAlbumLogoImageUrl = "../assets/default-album-image.jpg" ?? string.Empty;
            ImageBlobFolderUrl = blobStorageInfo.ImagesFolder ?? string.Empty;
        }

        protected string[] GetAlbumImagesUrls(string[] albumImages)
        {
            return albumImages
                .Select(GetImageBlobFileUriLocation)
                .ToArray();
        }

        protected string GetBlobFileUriLocation(string fileName)
        {
            return $"{BlobStorageUrl}{fileName.Replace('\\', '/')}";
        }

        protected string GetImageBlobFileUriLocation(string fileName)
        {
            return $"{BlobStorageUrl}/{ImageBlobFolderUrl}/{fileName.Replace('\\', '/')}";
        }
    }
}
