using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using LinkedInClone.Models;

namespace LinkedInClone.Services
{
    public interface IBlobService
    {
        public Task<BlobObject> GetBlobAsync(string name);

        public Task<string> UploadFileBlobAsync(string filePath, string fileName);

        public void DeleteBlob(string name);

    }
}