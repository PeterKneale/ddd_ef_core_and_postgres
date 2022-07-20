using System.Data;
using Dapper;

namespace Demo.Core.Application.Courses.Queries;

public static class ListCoursesByStudent
{
    public record Query(Guid StudentId) : IRequest<Result>;

    public record Result(IEnumerable<Model> Courses);

    public record Model(Guid CourseId, string CourseTitle, string CategoryTitle);

    private class Handler : IRequestHandler<Query, Result>
    {
        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Result> Handle(Query message, CancellationToken token)
        {
            var studentId = message.StudentId;

            var sql = "select courses.id as CourseId, courses.title as CourseTitle, categories.title as CategoryTitle " +
                      "from courses " +
                      "inner join enrollments on courses.id = enrollments.course_id " +
                      "inner join categories on courses.category_id = categories.id " +
                      "where enrollments.student_id = @studentId and enrollments.is_active = true;";
            var data = await _connection.QueryAsync<Model>(sql, new {studentId});

            return new Result(data);
        }
    }
}