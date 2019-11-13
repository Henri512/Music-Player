using MusicPlayer.Utilities;
using System.Linq;

namespace MusicPlayer.Infrastructure.Blobs
{
    public class BlobStorageService : IBlobStorageService
    {
        public string BlobStorageUrl { get; }
        public string DefaultAlbumLogoImageUrl { get; }
        public string ImageBlobFolderUrl { get; }

        public BlobStorageService(string environmentVariable)
        {
            BlobStorageInfo blobStorageInfo = BlobStorageInfo.CreateFromEnvironmentVariable(environmentVariable);
            BlobStorageUrl = blobStorageInfo.Url;
            DefaultAlbumLogoImageUrl = "../assets/default-album-image.jpg";
            ImageBlobFolderUrl = blobStorageInfo.ImagesFolder ?? string.Empty;
        }

        public string[] GetAlbumImagesUrls(string[] albumImages)
        {
            return albumImages
                .Select(GetImageBlobFileUriLocation)
                .ToArray();
        }

        public string GetBlobFileUriLocation(string fileName)
        {
            return $"{BlobStorageUrl}{fileName.Replace('\\', '/')}";
        }

        public string GetImageBlobFileUriLocation(string fileName)
        {
            return $"{BlobStorageUrl}/{ImageBlobFolderUrl}/{fileName.Replace('\\', '/')}";
        }
    }

    public interface IBlobStorageService
    {
        string BlobStorageUrl { get; }
        string DefaultAlbumLogoImageUrl { get; }
        string ImageBlobFolderUrl { get; }

        string[] GetAlbumImagesUrls(string[] albumImages);

        string GetBlobFileUriLocation(string fileName);

        string GetImageBlobFileUriLocation(string fileName);
    }
}
