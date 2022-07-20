using Demo.Core.Domain.Courses;
using Demo.Core.Domain.Students;
using Demo.Core.Domain.Teachers;
using FluentValidation;
using Details=Demo.Core.Domain.Courses.Details;

namespace Demo.Core.Application.Courses.Commands;

public static class EnrollInCourse
{
    public record Command(Guid CourseId, Guid StudentId) : IRequest;
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.CourseId).NotEmpty();
            RuleFor(m => m.StudentId).NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly ICourseRepository _courses;
    
        public Handler(ICourseRepository courses)
        {
            _courses = courses;
        }
    
        public async Task<Unit> Handle(Command command, CancellationToken token)
        {
            var courseId = CourseId.CreateInstance(command.CourseId);
            var studentId = StudentId.CreateInstance(command.StudentId);

            var course = await _courses.Get(courseId);
            course.Enroll(studentId);
    
            return Unit.Value;
        }
    }
}