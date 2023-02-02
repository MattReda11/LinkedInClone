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
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //  public DbSet<IdentityUser> AspNetUsers { get; set; }

        public DbSet<ApplicationUser> AppUsers { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Connection> Connections { get; set; }

        public DbSet<JobPosting> JobPosting { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }

        //  UserManager<IdentityUser> _userManager;
        // RoleManager<IdentityRole> _roleManager;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relationships should be specified here
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ApplicationUser>().ToTable("AppUsers");
            modelBuilder.Entity<ApplicationUser>()
               .HasMany(u => u.SentConnections)
                .WithOne(c => c.AccountOwner)
                .HasForeignKey(c => c.SenderId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.ReceivedConnections)
                .WithOne(c => c.Friend)
                .HasForeignKey(c => c.ReceiverId);
        }

    }
}