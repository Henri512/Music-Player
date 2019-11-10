using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
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

            var environmentPath = Environment
                .GetEnvironmentVariable("Blobs__MusicPlayer", EnvironmentVariableTarget.Machine);
            var sections = environmentPath.Split(';');
            var sectionsDictionary = sections
                .ToDictionary(k => k.Substring(0, k.IndexOf('=')), v => v.Substring(v.IndexOf('=')+1));

            _blobStorageInfo = new BlobStorageInfo
            {
                AccountKey = sectionsDictionary["AccountKey"],
                ContainerName = sectionsDictionary["ContainerName"],
                Url =  sectionsDictionary["Url"],
                AccountName = sectionsDictionary["AccountName"],
                ImagesFolder = sectionsDictionary["ImagesFolder"]
            };

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
                UploadFile(filePathOnSystem, blobFilePath);
            }
        }

        public void UpdateImageBlob(string blobFilePath, string filePathOnSystem)
        {
            var blobFileUrl = Path.Combine(_blobStorageInfo.Url
                + _blobStorageInfo.ImagesFolder
                + blobFilePath.Replace('\\', '/'));
            if (BlobExists(blobFileUrl))
            {
                _logger.Information($"Image with Url: {blobFileUrl} already exist");
            }
            else
            {
                UploadFile(filePathOnSystem,
                _blobStorageInfo.ImagesFolder + blobFilePath);
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

        public string GetBlobUrl()
        {
            return _blobStorageInfo.Url;
        }

        private IEnumerable<CloudBlockBlob> GetAllBlobs(CloudBlobClient blobClient)
        {
            var container = blobClient.GetContainerReference(_blobStorageInfo.ContainerName);
            BlobContinuationToken continuationToken = null;

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
