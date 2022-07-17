using System.Data;
using Dapper;
using Demo.Core.Domain.Students;

namespace Demo.Core.Application.Courses.Queries;

public static class ListByTeacher
{
    public record Query(Guid TeacherId) : IRequest<Result>;

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
            var teacherId = message.TeacherId;

            var sql = "select courses.id as CourseId, courses.title as CourseTitle, categories.title as CategoryTitle " +
                      "from courses " +
                      "inner join categories on courses.category_id = categories.id " +
                      "where courses.teacher_id = @teacherId";
            
            var data = await _connection.QueryAsync<Model>(sql, new {teacherId});
            
            return new Result(data);
        }
    }
}