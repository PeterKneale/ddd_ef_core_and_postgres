namespace Demo.Core.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DatabaseContext _db;

    public CategoryRepository(DatabaseContext db)
    {
        _db = db;
    }
    
    public async Task Add(Category category)
    {
        await _db.Categories.AddAsync(category);
    }

    public async Task<Category?> Get(CategoryId categoryId)
    {
        return await _db.Categories.FindAsync(categoryId);
    }
}