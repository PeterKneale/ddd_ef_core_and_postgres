using Demo.Core.Domain.Students;

namespace Demo.Core.Infrastructure.Repository;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(e => e.Id)
            .HasConversion(x => x.Value, x => StudentId.CreateInstance(x));

        builder
            .OwnsOne(p => p.Name, name =>
            {
                name.Property(p => p.FirstName).HasColumnName("first_name");
                name.Property(p => p.LastName).HasColumnName("last_name");
            });

        builder
            .Ignore(x => x.DomainEvents);
    }
}