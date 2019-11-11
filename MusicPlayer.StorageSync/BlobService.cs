using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using MusicPlayer.Utilities;
using Serilog;

namespace MusicPlayer.StorageSync
{
    public class BlobService
    {
        private readonly BlobStorageInfo _blobStorageInfo;
        private readonly CloudBlobContainer _container;
        private readonly ILogger _logger;
        private readonly List<CloudBlockBlob> _blobList;

        public BlobService(ILogger logger)
        {
            _logger = logger;

            _blobStorageInfo = BlobStorageInfo.CreateFromEnvironmentVariable("Blobs__MusicPlayer");

            var storageCredentials = new StorageCredentials(_blobStorageInfo.AccountName, _blobStorageInfo.AccountKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            _container = cloudBlobClient.GetContainerReference(_blobStorageInfo.ContainerName);

            _container.CreateIfNotExists();

            _blobList = GetAllBlobs(cloudBlobClient).ToList();
        }

        public IEnumerable<IListBlobItem> GetAllBlobs()
        {
            _logger.Information($"Getting all blobs from the container...");
            return _container.ListBlobs().ToList();
        }

        public void UpdateBlob(string blobFilePath, string filePathOnSystem)
        {
            if (BlobExists(blobFilePath))
            {
                _logger.Information($"Blob with Url: {_blobStorageInfo.Url + blobFilePath} already exist");
            }
            else
            {
                _logger.Information($"File with Url: {_blobStorageInfo.Url + blobFilePath} does not exist");
                UploadFile(filePathOnSystem, blobFilePath);
            }
        }

        public void UpdateImageBlob(string blobFilePath, string filePathOnSystem)
        {
            string relativePath = $"{_blobStorageInfo.ImagesFolder}/{blobFilePath.Replace('\\', '/')}";
            var blobFileUrl = $"{_blobStorageInfo.Url}/{relativePath}";
            if (BlobExists(relativePath))
            {
                _logger.Information($"Image with Url: {blobFileUrl} already exist");
            }
            else
            {
                _logger.Information($"Image with Url: {blobFileUrl} does not exist");
                UploadFile(filePathOnSystem, relativePath);
            }
        }

        public void UploadFile(string filePath, string blobFilePath)
        {
            _logger.Information($"Uploading new file to blob storage...");

            var newBlob = _container
                .GetBlockBlobReference(blobFilePath);
            newBlob.UploadFromFile(filePath);

            _logger.Information($"New file added, Url: {newBlob.Uri}");
        }

        public bool BlobExists(string blobFilePath)
        {
            return _blobList
                .Any(t => t.Name == blobFilePath.Replace('\\', '/'));
        }

        private IEnumerable<CloudBlockBlob> GetAllBlobs(CloudBlobClient blobClient)
        {
            var container = blobClient.GetContainerReference(_blobStorageInfo.ContainerName);
            BlobContinuationToken continuationToken = null;
            _logger.Information($"Getting all blobs from the container...");

            do
            {
                var response = container
                    .ListBlobsSegmented(string.Empty, true,
                        BlobListingDetails.None, new int?(),
                        continuationToken, null, null);
                continuationToken = response.ContinuationToken;
                foreach (var blob in response.Results.OfType<CloudBlockBlob>())
                {
                    yield return blob;
                }
            } while (continuationToken != null);
        }
    }
}
