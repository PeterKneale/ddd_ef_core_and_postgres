namespace Demo.Core.Domain.Teachers;

public class TeacherId : BaseValueObject
{
    public TeacherId()
    {
        
    }
    private TeacherId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Teacher id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static TeacherId CreateInstance(Guid value) => new(value);

    public Guid Value { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}