using System.Data;
using Dapper;

namespace Demo.Core.Application.Courses.Queries;

public static class ListCourses
{
    public record Query : IRequest<Result>;

    public record Result(IEnumerable<Model> Courses);

    public record Model(Guid CourseId, string CourseTitle, string CategoryTitle, long ActiveEnrollments);

    private class Handler : IRequestHandler<Query, Result>
    {
        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Result> Handle(Query message, CancellationToken token)
        {
            var sql = "select courses.id as CourseId, courses.title as CourseTitle, categories.title as CategoryTitle, totals.enrollments as ActiveEnrollments " +
                      "from courses " +
                      "inner join categories on courses.category_id = categories.id " +
                      "left join (select course_id, count(1) as enrollments from enrollments where is_active = true group by course_id) totals " +
                      "on totals.course_id = courses.id";    
            var data = await _connection.QueryAsync<Model>(sql);

            return new Result(data);
        }
    }
}