using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(s => s.Name).IsRequired().HasMaxLength(250);
            builder.Property(s => s.Address).IsRequired(false).HasMaxLength(250);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(250);
            builder.HasData(new List<Student>() 
            {
                new Student
                {
                    Id = 1,
                    Name = "Vamsi",
                    Email = "vamsi@example.com",
                    Address = "Hyd, IND",
                    DOB = new DateOnly(2000, 1, 1)
                }, new Student
                {
                    Id = 2,
                    Name = "Ramesh",
                    Email = "ramesh@example.com",
                    Address = "Vja, IND",
                    DOB = new DateOnly(1999, 5, 15)
                } 
            });
        }
    }
}
