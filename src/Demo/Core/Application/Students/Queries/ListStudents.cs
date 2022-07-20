using Demo.Core.Domain.Students;

namespace Demo.Core.Application.Students.Queries;

public static class ListStudents
{
    public record Query : IRequest<Result>;

    public record Result(IEnumerable<Model> Students);

    public record Model(Guid Id, string Name);

    private static class Mapping
    {
        public static Model Map(Student student)
        {
            return new Model(student.Id.Value, $"{student.Name.FirstName}");
        }
    }

    private class Handler : IRequestHandler<Query, Result>
    {
        private readonly IStudentRepository _students;

        public Handler(IStudentRepository students)
        {
            _students = students;
        }

        public async Task<Result> Handle(Query message, CancellationToken token)
        {
            var students = await _students.List();
            var models = students.Select(Mapping.Map);
            return new Result(models);
        }
    }
}