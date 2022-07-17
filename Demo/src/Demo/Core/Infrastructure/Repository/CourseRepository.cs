using Demo.Core.Domain.Courses;
using Demo.Core.Infrastructure.Persistence;

namespace Demo.Core.Infrastructure.Repository;

public class CourseRepository : ICourseRepository
{
    private readonly DatabaseContext _db;

    public CourseRepository(DatabaseContext db)
    {
        _db = db;
    }
    
    public async Task Add(Course course)
    {
        await _db.Courses.AddAsync(course);
    }

    public async Task<Course?> Get(CourseId courseId)
    {
        return await _db.Courses.FindAsync(courseId);
    }
}