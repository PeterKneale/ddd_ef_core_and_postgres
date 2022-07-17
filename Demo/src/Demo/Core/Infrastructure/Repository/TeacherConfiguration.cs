using Demo.Core.Domain.Teachers;

namespace Demo.Core.Infrastructure.Repository;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(e => e.Id)
            .HasConversion(x => x.Value, x => TeacherId.CreateInstance(x));

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