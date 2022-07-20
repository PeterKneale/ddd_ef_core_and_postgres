namespace Demo.Core.Infrastructure.Repository;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(e => e.Id)
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