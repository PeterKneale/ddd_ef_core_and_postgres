using Demo.Core.Application.Categories.Commands;
using Demo.Core.Application.Courses.Commands;
using Demo.Core.Application.Courses.Queries;
using Demo.Core.Application.Teachers.Commands;

namespace Demo.IntegrationTests.Application.Courses;

[Collection(nameof(DatabaseCollection))]
public class CreateCourseTests : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _services;

    public CreateCourseTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services;
    }

    [Fact]
    public async Task Course_created_with_command_can_be_retrieved_with_query()
    {
        // arrange
        var teacherId = Guid.NewGuid();
        var firstName = "john";
        var lastName = "smith";
        var categoryId = Guid.NewGuid();
        var categoryTitle = "programming";
        var categoryDescription = null as string;
        var courseId = Guid.NewGuid();
        var courseTitle = "intro to C#";
        var courseDescription = "an introductory course";

        // act
        await _services.Send(new CreateTeacher.Command(teacherId, firstName, lastName));
        await _services.Send(new CreateCategory.Command(categoryId, categoryTitle, categoryDescription));
        await _services.Send(new CreateCourse.Command(courseId, teacherId, categoryId, courseTitle, courseDescription));

        // assert
        var results = await _services.Send(new ListByTeacher.Query(teacherId));
        results.Courses.Should().BeEquivalentTo(new ListByTeacher.Model[] {new(courseId, courseTitle, categoryTitle)});
    }
}