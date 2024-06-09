using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CollegeApp.Data.Config
{
    public class StudentConfig: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.Name).IsRequired();
            builder.Property(n => n.Name).HasMaxLength(250);
            builder.Property(n => n.Age).IsRequired();
            builder.Property(n => n.Email).IsRequired();
            builder.Property(n => n.Email).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false);
            builder.Property(n => n.Address).HasMaxLength(500);

            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    Name = "Idris",
                    Age = 24,
                    Email = "olayinkayakub@yahoo.com",
                    Address = "femi cooker drive"
                },
                new Student
                {
                    Id = 2,
                    Name = "Yakub",
                    Age = 26,
                    Email = "Idrisyakub@yahoo.com",
                    Address = "odenike street"
                },
                new Student
                {
                    Id = 3,
                    Name = "olayinka",
                    Age = 25,
                    Email = "driiisdev@yahoo.com",
                    Address = "biadu road"
                }
            });
        }
    }
}
