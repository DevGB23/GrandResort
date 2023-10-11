using Microsoft.EntityFrameworkCore;
using Resort_Web.Models;

namespace Resort_Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                new Villa { 
                    Id = 1, 
                    Name = "Royal Villa", 
                    Details = "Etiam auctor pellentesque metus sit amet accumsan. Proin laoreet viverra nibh, eget eleifend dui condimentum pulvinar. Duis auctor in ipsum ut rutrum. Aliquam posuere sollicitudin quam. In in lobortis felis. Fusce ultricies, eros nec vestibulum pulvinar, ex massa interdum nunc, at pretium ipsum justo ac ex. Suspendisse ut luctus lectus. Proin congue sodales porta. Proin eleifend semper neque eu iaculis. Donec feugiat risus enim, vitae consectetur mauris porta id. Donec euismod, massa non porttitor tincidunt, diam dolor dignissim nisl",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200.0,
                    SqFt = 580,
                    Amenity = "",
                    CreatedAt = DateTime.UtcNow                      
                },
                     
                    
                new Villa { 
                    Id = 2, 
                    Name = "Diamond Villa", 
                    Details = "Sed mollis odio in justo volutpat semper. Etiam eget interdum ipsum, id placerat dui. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus",
                    ImageUrl = "",
                    Occupancy = 7,
                    Rate = 170.0,
                    SqFt = 480,
                    Amenity = "",
                    CreatedAt = DateTime.UtcNow
                },
                new Villa { 
                    Id = 3, 
                    Name = "Safire Royal Villa", 
                    Details = "Donec est lacus, pharetra sagittis eleifend ac, sodales sit amet tortor. Aliquam massa odio, ullamcorper sit amet tristique in, facilisis at libero. Aenean in magna mi. ",
                    ImageUrl = "",
                    Occupancy = 9,
                    Rate = 400.0,
                    SqFt = 660,
                    Amenity = "",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }

    }
}
