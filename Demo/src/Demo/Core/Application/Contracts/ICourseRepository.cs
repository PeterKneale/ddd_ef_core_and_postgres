using Demo.Core.Domain.Courses;

namespace Demo.Core.Application.Contracts;

public interface ICourseRepository
{
    Task Add(Course course);
    Task<Course?> Get(CourseId courseId);
}