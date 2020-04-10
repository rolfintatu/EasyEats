using Application.Common.Dtos;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AzureStorage : IStorageService
    {
        private readonly IConfiguration configuration;
        private readonly string StorageConnectinString;

        public AzureStorage(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.StorageConnectinString = this.configuration["StorageOptions:connectionString"];
        }

        public async Task<Uri> UploadImage(byte[] photoBase64String, string extension)
        {

            string fileName = $"{Guid.NewGuid().ToString()}.{extension}";

            CloudStorageAccount storageAccount = CloudStorageAccount
              .Parse(StorageConnectinString);
            CloudBlobClient client = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("img");

            await container.CreateIfNotExistsAsync(
              BlobContainerPublicAccessType.Blob,
              new BlobRequestOptions(),
              new OperationContext());
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = $"image/{extension}";

            using (Stream stream = new MemoryStream(photoBase64String, 0, photoBase64String.Length))
            {
                await blob.UploadFromStreamAsync(stream).ConfigureAwait(false);
            }

            return blob.Uri;
        }

        public async Task DeleteImage(string imagePath)
        {
            string fileName = imagePath;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectinString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("img");

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

            using (Stream stream = new MemoryStream())
            {
                await blob.DeleteIfExistsAsync().ConfigureAwait(false);
            }
        }
    }
}
