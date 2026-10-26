using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CollegeApp.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options): base(options)
        {
                
        }
        //Add the data to students table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Vamsi",
                    Email = "vamsi@example.com",
                    Address = "Hyd, IND",
                    DOB = new DateOnly(2000, 1, 1)
                },                new Student
                {
                    Id = 2,
                    Name = "Ramesh",
                    Email = "ramesh@example.com",
                    Address = "Vja, IND",
                    DOB = new DateOnly(1999, 5, 15)
                }
            );

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(n => n.Name).IsRequired();
                entity.Property(n => n.Name).HasMaxLength(250);
                entity.Property(n => n.Address).IsRequired(false).HasMaxLength(250);
                entity.Property(n => n.Email).IsRequired().HasMaxLength(250);
            });
        }

        //Register the Student class with the DBContext so that it can create a table for the Student class in the database
        DbSet<Student> Students { get; set; }
    }
}
