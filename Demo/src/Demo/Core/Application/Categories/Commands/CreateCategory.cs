using FluentValidation;

namespace Demo.Core.Application.Categories.Commands;

public static class CreateCategory
{
    public record Command(Guid CategoryId, string Title, string? Description) : IRequest;
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.CategoryId).NotEmpty();
            RuleFor(m => m.Title).NotEmpty().MaximumLength(50);
            RuleFor(m => m.Description).MaximumLength(50);
        }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly ICategoryRepository _categories;
    
        public Handler(ICategoryRepository categories)
        {
            _categories = categories;
        }
    
        public async Task<Unit> Handle(Command command, CancellationToken token)
        {
            var categoryId = CategoryId.CreateInstance(command.CategoryId);
            var description = new Details(command.Title, command.Description);
    
            var category = Category.CreateInstance(categoryId, description);
    
            await _categories.Add(category);
    
            return Unit.Value;
        }
    }
}
