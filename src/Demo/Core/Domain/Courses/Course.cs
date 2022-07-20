using Demo.Core.Domain.Students;
using Demo.Core.Domain.Teachers;

namespace Demo.Core.Domain.Courses;

public class Course : BaseEntity
{
    private List<Enrollment> _enrollments;

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
        _enrollments = new List<Enrollment>();
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
    public void Enroll(StudentId studentId)
    {
        var enrollment = Enrollment.CreateInstance(studentId, Id);
        _enrollments.Add(enrollment);
    }

    public void Unenroll(StudentId studentId)
    {
        var enrollment = _enrollments.Single(x => x.IsActive && x.StudentId == studentId);
        enrollment.Leave();
    }

    public static Course CreateInstance(CourseId courseId, TeacherId teacherId, CategoryId categoryId, Details details) => new(courseId, teacherId, categoryId, details);
}