namespace Demo.Core.Domain.Courses;

public class EnrollmentId : BaseValueObject
{
    public EnrollmentId()
    {
        
    }
    private EnrollmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Enrollment id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static EnrollmentId CreateInstance(Guid value) => new(value);

    public Guid Value { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}