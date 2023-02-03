using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInClone.Models.Blobs
{
    public class BlobContent
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }

    }
}