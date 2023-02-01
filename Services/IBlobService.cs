using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace LinkedInClone.Services
{
    public interface IBlobService
    {
        public Task<BlobInfo> GetBlobAsync(string name);

        public Task<IEnumerable<string>> ListBlobsAsync();

        public Task UploadFileBlobAsync(string filePath, string fileName);

    }
}