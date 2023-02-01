using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedInClone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Data
{
    //main DB context that will contain Identity context
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<IdentityUser> AspNetUsers { get; set; }

        //public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        //public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Connection> Connections { get; set; }

        //  UserManager<IdentityUser> _userManager;
        // RoleManager<IdentityRole> _roleManager;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relationships should be specified here
            base.OnModelCreating(modelBuilder);

            //         modelBuilder.Entity<Connection>()
            // .HasOne(c => c.AccountOwner)        
            // .HasForeignKey(c => c.AccountOwner)
            // .WillCascadeOnDelete(false);

        }
    }
}