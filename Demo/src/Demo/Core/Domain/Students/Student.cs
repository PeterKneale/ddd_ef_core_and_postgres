using Demo.Core.Domain.Common;

namespace Demo.Core.Domain.Students;

public class Student : BaseEntity
{
    public StudentId Id { get; private init; }

    public Name Name { get; private set; }
    
    private Student()
    {
    }

    public Student(StudentId id, Name name)
    {
        Id = id;
        Name = name;
    }

    public void ChangeName(Name name)
    {
        Name = name;
        AddDomainEvent(new StudentNameChangedEvent(this));
    }

    public static Student CreateInstance(StudentId studentId, Name name) => new(studentId, name);
}