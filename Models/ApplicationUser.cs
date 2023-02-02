using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkedInClone.Models
{

    
    public class ApplicationUser : IdentityUser
    {

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public ICollection<Connection> SentConnections { get; set; }
        
        public ICollection<Connection> ReceivedConnections { get; set; }
        
    }
}