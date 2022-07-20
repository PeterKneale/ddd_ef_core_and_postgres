using Demo.Core.Application.Categories.Commands;
using Demo.Core.Application.Courses.Commands;
using Demo.Core.Application.Courses.Queries;
using Demo.Core.Application.Students.Commands;
using Demo.Core.Application.Teachers.Commands;

namespace Demo.IntegrationTests.Application;

[Collection(nameof(DatabaseCollection))]
public class SmokeTests : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _services;

    public SmokeTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services;
    }

    [Fact]
    public async Task Course_created_with_command_can_be_retrieved_with_query()
    {
        // arrange
        var teacherId = Guid.NewGuid();
        var teacherFirstName = "john";
        var teacherLastName = "smith";
        var studentId = Guid.NewGuid();
        var studentFirstName = "john";
        var studentLastName = "smith";
        var categoryId = Guid.NewGuid();
        var categoryTitle = "programming";
        var categoryDescription = null as string;
        var courseId = Guid.NewGuid();
        var courseTitle = "intro to C#";
        var courseDescription = "an introductory course";

        // act
        await _services.Send(new CreateTeacher.Command(teacherId, teacherFirstName, teacherLastName));
        await _services.Send(new CreateStudent.Command(studentId, studentFirstName, studentLastName));
        await _services.Send(new CreateCategory.Command(categoryId, categoryTitle, categoryDescription));
        await _services.Send(new CreateCourse.Command(courseId, teacherId, categoryId, courseTitle, courseDescription));
        await _services.Send(new EnrollInCourse.Command(courseId, studentId));

        // assert
        var results0 = await _services.Send(new ListCourses.Query());
        results0.Courses.Should().BeEquivalentTo(new ListCourses.Model[] {new(courseId, courseTitle, categoryTitle, 1L)});
        
        var results1 = await _services.Send(new ListCoursesByTeacher.Query(teacherId));
        results1.Courses.Should().BeEquivalentTo(new ListCoursesByTeacher.Model[] {new(courseId, courseTitle, categoryTitle)});
        
        var results2 = await _services.Send(new ListCoursesByStudent.Query(studentId));
        results2.Courses.Should().BeEquivalentTo(new ListCoursesByStudent.Model[] {new(courseId, courseTitle, categoryTitle)});
    }
}