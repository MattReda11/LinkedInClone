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

        private readonly DbContextOptions _options;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _options = options;
        }

        //  public DbSet<IdentityUser> AspNetUsers { get; set; }

        public DbSet<ApplicationUser> AppUsers { get; set; }

        public DbSet<RecruiterUser> Recruiters { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Connection> Connections { get; set; }

        public DbSet<JobPosting> JobPostings { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }

        //  UserManager<IdentityUser> _userManager;
        // RoleManager<IdentityRole> _roleManager;

        // created user defined function in SQL database that will use SOUNDEX functionality
        [DbFunction(Name = "udfSoundex")]
        public static string SoundsLike(string keyword)
        {
            throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<ApplicationUser>().ToTable("AppUsers"); //<--- this will rename/recreate default user table 


            //Fluent API 



            //AppUser

            modelBuilder.Entity<ApplicationUser>()
           .HasDiscriminator<string>("Discriminator")
           .HasValue<RecruiterUser>("RecruiterUser")
           .HasValue<ApplicationUser>("ApplicationUser");

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(u => u.SentConnections)
                .WithOne(c => c.AccountOwner)
                .HasForeignKey(c => c.SenderId);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.ReceivedConnections)
                .WithOne(c => c.Friend)
                .HasForeignKey(c => c.ReceiverId);

            //Connection

            modelBuilder.Entity<Connection>()
                           .HasOne(c => c.Friend)
                           .WithMany(u => u.ReceivedConnections)
                           .HasForeignKey(c => c.ReceiverId)
                           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Connection>()
                           .HasOne(c => c.AccountOwner)
                           .WithMany(u => u.SentConnections)
                           .HasForeignKey(c => c.SenderId)
                           .OnDelete(DeleteBehavior.Restrict);


            // Other entity


        }





    }
}