using CollegeApp.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CollegeApp.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options): base(options)
        {
            
        }
        //Register the Student class with the DBContext so that it can create a table for the Student class in the database
        DbSet<Student> Students { get; set; }

        //Add the data to students table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfig());
        }

        
        
    }
}
