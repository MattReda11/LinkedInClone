using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInClone.Models.Blobs
{
    public class BlobObject
    {
        public Stream? Content { get; set; }

        public string? ContentType { get; set; }
    }
}