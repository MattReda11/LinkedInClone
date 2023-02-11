using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    // messages between users
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public ApplicationUser SentBy { get; set; }

        public ApplicationUser ReceivedBy { get; set; }

        [Required, MinLength(1), MaxLength(20000)]
        public string Content { get; set; }

        public DateTime SentDate { get; set; }

        public Conversation Conversation { get; set; }
    }
}