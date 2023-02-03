using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LinkedInClone.Models.Blobs;

namespace LinkedInClone.Services
{
    public class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobServiceClient;

        private BlobContainerClient _client;

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".PNG", ".GIF" };

        public static readonly List<string> VideoExtensions = new List<string> { ".MP4", ".MOV", ".AVI" };

        public static readonly List<string> DocExtensions = new List<string> { ".TXT", ".DOC", ".DOCX", ".PDF" };

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _client = _blobServiceClient.GetBlobContainerClient("blob-storage");
        }

        public void DeleteBlob(string name)
        {
            throw new NotImplementedException();
        }

        async Task<BlobObject> IBlobService.GetBlobAsync(string url)
        {
            var fileName = new Uri(url).Segments.LastOrDefault();

            try
            {

                var blobClient = _client.GetBlobClient(fileName);

                if (await blobClient.ExistsAsync())
                {
                    BlobDownloadResult content = await blobClient.DownloadContentAsync();
                    var downloadData = content.Content.ToArray();

                    if (ImageExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant())))
                    {
                        var extension = Path.GetExtension(fileName);
                        return new BlobObject { Content = downloadData, ContentType = "image/" + extension.Remove(0, 1) };
                    }
                    else if (VideoExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant())))
                    {
                        var extension = Path.GetExtension(fileName);
                        return new BlobObject { Content = downloadData, ContentType = "video/" + extension.Remove(0, 1) };
                    }
                    else if (DocExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant())))
                    {
                        var extension = Path.GetExtension(fileName);
                        return new BlobObject { Content = downloadData, ContentType = "document/" + extension.Remove(0, 1) };
                    }
                    else
                    {
                        return new BlobObject { Content = downloadData, ContentType = content.Details.ContentType };
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task<string> IBlobService.UploadFileBlobAsync(string filePath, string fileName)
        {
            var blobClient = _client.GetBlobClient(fileName);

            var status = await blobClient.UploadAsync(filePath);

            return blobClient.Uri.AbsoluteUri;
        }
    }
}