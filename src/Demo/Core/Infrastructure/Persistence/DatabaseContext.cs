using Demo.Core.Domain.Courses;
using Demo.Core.Domain.Students;
using Demo.Core.Domain.Teachers;

namespace Demo.Core.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}