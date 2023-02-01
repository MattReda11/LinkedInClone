using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace LinkedInClone.Services
{
    public class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {

            _blobServiceClient = blobServiceClient;

        }

        public Task<BlobInfo> GetBlobAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> ListBlobsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UploadFileBlobAsync(string filePath, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}