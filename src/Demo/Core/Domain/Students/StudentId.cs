namespace Demo.Core.Domain.Students;

public class StudentId : BaseValueObject
{
    public StudentId()
    {
        
    }
    private StudentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Student id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static StudentId CreateInstance(Guid value) => new(value);

    public Guid Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}