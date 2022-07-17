namespace Demo.Core.Domain.Categories;

public class Details : BaseValueObject
{
    private Details()
    {
    }

    public Details(string title, string? description)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title), "title is null");
        Description = description;
    }

    public string Title { get; private set; }
    
    public string? Description { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return Description;   
    }
}