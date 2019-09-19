using System.Linq;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace MusicPlayer.StorageSync
{
    public class BlobService
    {
        private readonly IConfiguration _configuration;
        private readonly DataService _dataService;
        private readonly CloudBlobContainer _container;

        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dataService = new DataService(configuration);

            var blobStorageInfo = _configuration
                .GetSection("BlobStorages")
                .Get<BlobStorageInfo[]>().First();
            

            var storageCredentials = new StorageCredentials(blobStorageInfo.AccountName, blobStorageInfo.AccountKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            _container = cloudBlobClient.GetContainerReference(blobStorageInfo.ContainerName);

            _container.CreateIfNotExists();

            // Regex regex = new Regex(@"\d{4}-\d{2}-\d{2}_\d{2}-\d{2}-\d{2}");
            UploadFile(
                @"D:\Temp\Muzika\Nikad spremni\Razno\Nikad Spremni - Spijunke.mp3",
                @"Muzika\Nikad spremni\Razno\Nikad Spremni - Spijunke.mp3");
        }

        private void UploadFile(string filePath, string blobFilePath)
        {
            var newBlob = _container.GetBlockBlobReference(blobFilePath);
            newBlob.UploadFromFile(filePath);
        }

        public void UpdateSongInBlob(string filePath)
        {

        }
    }
}
