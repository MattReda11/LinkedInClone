using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInClone.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser StartedBy { get; set; }

        [Required]
        public ApplicationUser ReceivedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public ICollection<Message> Messages { get; set; }

    }
}