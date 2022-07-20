using Demo.Core.Application.Students.Commands;
using Demo.Core.Application.Students.Queries;
using FluentValidation;

namespace Demo.IntegrationTests.Application.Students;

[Collection(nameof(DatabaseCollection))]
public class CreateStudentTests : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _services;

    public CreateStudentTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services;
    }

    [Fact]
    public async Task Student_created_with_command_can_be_retrieved_with_query()
    {
        // arrange
        var studentId = Guid.NewGuid();
        var firstName = "john";
        var lastName = "smith";

        // act
        await _services.Send(new CreateStudent.Command(studentId, firstName, lastName));
        var result = await _services.Send(new GetStudent.Query(studentId));

        // assert
        result.Id.Should().Be(studentId);
        result.FirstName.Should().Be(firstName);
        result.LastName.Should().Be(lastName);
    }
    
    [Fact]
    public async Task InvalidRequestsThrow()
    {
        // arrange
        var studentId = Guid.Empty;
        var firstName = "";
        var lastName = "";

        // act
        Func<Task> act = async () =>
        {
            await _services.Send(new CreateStudent.Command(studentId, firstName, lastName));
        };

        // assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}