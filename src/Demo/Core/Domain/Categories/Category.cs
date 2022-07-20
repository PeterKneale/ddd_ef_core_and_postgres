namespace Demo.Core.Domain.Categories;

public class Category : BaseEntity
{
    public CategoryId Id { get; private init; }

    public Details Details { get; private set; }

    private Category()
    {
    }

    private Category(CategoryId id, Details details)
    {
        Id = id;
        Details = details;
    }

    public void ChangeTitle(Details name)
    {
        Details = name;
    }

    public static Category CreateInstance(CategoryId courseId, Details details) => new(courseId, details);
}