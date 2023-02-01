using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInClone.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //To be modified, from: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-7.0&tabs=visual-studio

            // using (var context = new AppDbContext(
            //     serviceProvider.GetRequiredService<
            //         DbContextOptions<AppDbContext>>()))
            // {
            // Look for any movies.
            // if (context.Movie.Any())
            // {
            //     return;   // DB has been seeded
            // }
            // context.Movie.AddRange(
            //     new Movie
            //     {
            //         Title = "When Harry Met Sally",
            //         ReleaseDate = DateTime.Parse("1989-2-12"),
            //         Genre = "Romantic Comedy",
            //         Price = 7.99M
            //     },
            //     new Movie
            //     {
            //         Title = "Ghostbusters ",
            //         ReleaseDate = DateTime.Parse("1984-3-13"),
            //         Genre = "Comedy",
            //         Price = 8.99M
            //     },
            //     new Movie
            //     {
            //         Title = "Ghostbusters 2",
            //         ReleaseDate = DateTime.Parse("1986-2-23"),
            //         Genre = "Comedy",
            //         Price = 9.99M
            //     },
            //     new Movie
            //     {
            //         Title = "Rio Bravo",
            //         ReleaseDate = DateTime.Parse("1959-4-15"),
            //         Genre = "Western",
            //         Price = 3.99M
            //     }
            // );
            // context.SaveChanges();
        }
    }
}