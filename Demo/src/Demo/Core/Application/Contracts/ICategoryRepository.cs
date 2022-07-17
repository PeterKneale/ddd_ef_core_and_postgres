namespace Demo.Core.Application.Contracts;

public interface ICategoryRepository
{
    Task Add(Category category);
    Task<Category?> Get(CategoryId courseId);
}