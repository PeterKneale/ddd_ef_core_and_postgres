using Demo.Core.Domain.Common;

namespace Demo.Core.Domain.Teachers;

public class Teacher : BaseEntity
{
    public TeacherId Id { get; private init; }

    public Name Name { get; private set; }
    

    private Teacher()
    {
    }

    private Teacher(TeacherId id, Name name)
    {
        Id = id;
        Name = name;
    }

    public void ChangeName(Name name)
    {
        Name = name;
    }

    public static Teacher CreateInstance(TeacherId teacherId, Name name) => new(teacherId, name);
}