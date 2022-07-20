namespace Demo.Core.Domain.Courses;

public class CourseId : BaseValueObject
{
    public CourseId()
    {
        
    }
    private CourseId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Course id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static CourseId CreateInstance(Guid value) => new(value);

    public Guid Value { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}