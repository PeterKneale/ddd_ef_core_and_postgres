namespace Demo.Core.Domain.Common;

public class Name : BaseValueObject
{
    private Name()
    {
    }

    public Name(string firstName, string lastName)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName), "first name is null");
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName), "last name is null");
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}