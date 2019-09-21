using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using MusicPlayer.Domain.Profiles;
using Serilog;

namespace MusicPlayer.StorageSync
{
    public class BlobService
    {
        private readonly IConfiguration _configuration;
        private readonly DataService _dataService;
        private readonly BlobStorageInfo _blobStorageInfo;
        private readonly CloudBlobContainer _container;
        private readonly ILogger _logger;

        public BlobService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _dataService = new DataService(configuration,
                new Mapper(new MapperConfiguration(t => t.AddMaps(typeof(AlbumModelProfile).Assembly))));

            _blobStorageInfo = _configuration
                .GetSection("BlobStorages")
                .Get<BlobStorageInfo[]>().First();
            
            var storageCredentials = new StorageCredentials(_blobStorageInfo.AccountName, _blobStorageInfo.AccountKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            _container = cloudBlobClient.GetContainerReference(_blobStorageInfo.ContainerName);

            _container.CreateIfNotExists();
        }

        public IEnumerable<IListBlobItem> GetAllBlobs()
        {
            _logger.Information($"Getting all blobs from the container...");
            return _container.ListBlobs().ToList();
        }

        public void UpdateBlob(string bloblFilePath, string filePathOnSystem)
        {
            if(BlobExists(bloblFilePath))
            {
                _logger.Information($"Blob with Url: {_blobStorageInfo.Url + bloblFilePath} already exist");
            }
            else
            {
                UploadFile(filePathOnSystem, bloblFilePath);
            }
        }

        public async void UploadFile(string filePath, string blobFilePath)
        {
            _logger.Information($"Uploading new blob to blob storage...");

            var newBlob = _container.GetBlockBlobReference(blobFilePath);
            await newBlob.UploadFromFileAsync(filePath);

            _logger.Information($"New blob added, Url: {_blobStorageInfo.Url + blobFilePath}");
        }

        public bool BlobExists(string blobFilePath)
        {
            return _container
                .GetBlockBlobReference(blobFilePath)
                .Exists();
        }

        public string GetBlobUrl()
        {
            return _blobStorageInfo.Url;
        }
    }
}
