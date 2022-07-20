namespace Demo.Core.Domain.Students;

public class StudentNameChangedEvent : BaseEvent
{
    public Student Student { get; }

    public StudentNameChangedEvent(Student student)
    {
        Student = student;
    }
}