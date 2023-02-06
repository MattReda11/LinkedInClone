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

        public string RoleId { get; set; }
        public virtual IdentityRole Role { get; set; }

        //might have to merge RecruiterUser + AppUser, having trouble with user/signinManager
        
        public string Company { get; set; }
        
        public string FullName { get; set; }
        
    }
}