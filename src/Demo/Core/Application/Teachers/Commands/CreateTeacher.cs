using Demo.Core.Domain.Common;
using Demo.Core.Domain.Teachers;
using FluentValidation;

namespace Demo.Core.Application.Teachers.Commands;

public static class CreateTeacher
{
    public record Command(Guid TeacherId, string FirstName, string LastName) : IRequest;
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.TeacherId).NotEmpty();
            RuleFor(m => m.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(m => m.LastName).NotEmpty().MaximumLength(50);
        }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly ITeacherRepository _teachers;
    
        public Handler(ITeacherRepository teachers)
        {
            _teachers = teachers;
        }
    
        public async Task<Unit> Handle(Command command, CancellationToken token)
        {
            var teacherId = TeacherId.CreateInstance(command.TeacherId);
            var name = new Name(command.FirstName, command.LastName);
    
            var teacher = Teacher.CreateInstance(teacherId, name);
    
            await _teachers.Add(teacher);
    
            return Unit.Value;
        }
    }
}
