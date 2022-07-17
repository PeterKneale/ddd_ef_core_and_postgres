using Demo.Core.Domain.Courses;
using Demo.Core.Domain.Teachers;

namespace Demo.Core.Infrastructure.Repository;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(e => e.Id)
            .HasConversion(x => x.Value, x => CourseId.CreateInstance(x));
        
        builder
            .Property(e => e.TeacherId)
            .HasConversion(x => x.Value, x => TeacherId.CreateInstance(x));

        builder
            .Property(e => e.CategoryId)
            .HasConversion(x => x.Value, x => CategoryId.CreateInstance(x));
        
        builder
            .OwnsOne(p => p.Details, name =>
            {
                name.Property(p => p.Title).HasColumnName("title");
                name.Property(p => p.Description).HasColumnName("description");
            });

        builder
            .Ignore(x => x.DomainEvents);
    }
}