using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    // connections are when users 'add each other'
    public class Connection
    {

        public Connection()
        {
            SenderId = AccountOwner.Id;
            ReceiverId = Friend.Id;
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Account Owner")]
        public ApplicationUser AccountOwner { get; set; } // treating AccountOwner as sender

        [Required]
        public ApplicationUser Friend { get; set; } // Friend as receiver

        [Display(Name = "Creation Date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool Accepted { get; set; } = false;
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string ReceiverId { get; set; }

    }
}