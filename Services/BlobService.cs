using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LinkedInClone.Models;

namespace LinkedInClone.Services
{
    public class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobServiceClient;

        private BlobContainerClient _client;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _client = _blobServiceClient.GetBlobContainerClient("blob-storage");
        }

        public void DeleteBlob(string name)
        {
            throw new NotImplementedException();
        }

        Task<BlobObject> IBlobService.GetBlobAsync(string name)
        {
            throw new NotImplementedException();
        }

        async Task<string> IBlobService.UploadFileBlobAsync(string filePath, string fileName)
        {
            var blobClient = _client.GetBlobClient(fileName);

            var status = await blobClient.UploadAsync(filePath);

            return blobClient.Uri.AbsoluteUri;
        }
    }
}