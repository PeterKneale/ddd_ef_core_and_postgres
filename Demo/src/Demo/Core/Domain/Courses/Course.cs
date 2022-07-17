using Demo.Core.Domain.Teachers;

namespace Demo.Core.Domain.Courses;

public class Course : BaseEntity
{
    public CourseId Id { get; private init; }
    public TeacherId TeacherId { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public Details Details { get; private set; }

    private Course()
    {
    }

    private Course(CourseId id, TeacherId teacherId, CategoryId categoryId, Details details)
    {
        Id = id;
        TeacherId = teacherId;
        CategoryId = categoryId;
        Details = details;
    }

    public void ChangeTitle(Details name)
    {
        Details = name;
    }

    public void ChangeTeacher(TeacherId teacherId)
    {
        TeacherId = teacherId;
    }
    
    public void ChangeCategory(CategoryId categoryId)
    {
        CategoryId = categoryId;
    }

    public static Course CreateInstance(CourseId courseId, TeacherId teacherId, CategoryId categoryId, Details details) => new(courseId, teacherId, categoryId, details);
}