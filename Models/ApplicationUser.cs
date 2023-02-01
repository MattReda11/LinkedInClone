using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CustomTag { get; set; }

        public ICollection<Connection> SentConnections { get; set; }

        public ICollection<Connection> ReceivedConnections { get; set; }
    }
}