using Demo.Core.Domain.Students;

namespace Demo.Core.Domain.Courses;

public class Enrollment : BaseEntity
{
    public EnrollmentId Id { get; private init; }
    public StudentId StudentId { get; private init; }
    public CourseId CourseId { get; private init; }
    public DateTime EnrolledAt { get; private init; }
    public bool IsActive { get; private set; }
    public DateTime UnenrolledAt { get; private set; }

    private Enrollment()
    {
    }
    private Enrollment(StudentId studentId, CourseId courseId)
    {
        Id = EnrollmentId.CreateInstance(Guid.NewGuid());
        StudentId = studentId;
        CourseId = courseId;
        IsActive = true;
        EnrolledAt = DateTime.UtcNow;
    }

    public void Leave()
    {
        IsActive = false;
        UnenrolledAt = DateTime.UtcNow;
    }

    public bool IsEnrolled()
    {
        return IsActive;
    }

    public static Enrollment CreateInstance(StudentId studentId, CourseId courseId) => new(studentId, courseId);
}