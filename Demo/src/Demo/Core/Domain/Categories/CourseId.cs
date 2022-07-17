namespace Demo.Core.Domain.Categories;

public class CategoryId : BaseValueObject
{
    public CategoryId()
    {
        
    }
    private CategoryId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Category id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static CategoryId CreateInstance(Guid value) => new(value);

    public Guid Value { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}