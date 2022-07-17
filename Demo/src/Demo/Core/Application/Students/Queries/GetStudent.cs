using Demo.Core.Domain.Students;
using FluentValidation;

namespace Demo.Core.Application.Students.Queries;

public static class GetStudent
{
    public record Query(Guid StudentId) : IRequest<Result>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.StudentId).NotEmpty();
        }
    }
    
    public record Result(Guid Id, string FirstName, string LastName, string FullName);

    private class Handler : IRequestHandler<Query, Result>
    {
        private readonly IStudentRepository _students;

        public Handler(IStudentRepository students)
        {
            _students = students;
        }

        public async Task<Result> Handle(Query query, CancellationToken token)
        {
            var studentId = StudentId.CreateInstance(query.StudentId);
            var student = await _students.Get(studentId);
            if (student == null)
            {
                throw new NotImplementedException();
            }

            return new Result(student.Id.Value, student.Name.FirstName, student.Name.LastName, student.Name.FirstName);
        }
    }
}