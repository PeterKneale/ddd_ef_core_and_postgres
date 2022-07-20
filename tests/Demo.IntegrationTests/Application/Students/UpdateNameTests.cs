using Demo.Core.Application.Students.Commands;
using Demo.Core.Application.Students.Queries;

namespace Demo.IntegrationTests.Application.Students;

[Collection(nameof(DatabaseCollection))]
public class UpdateNameTests : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _services;

    public UpdateNameTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services;
    }

    [Fact]
    public async Task Student_name_updated_with_command_can_be_retrieved_with_query()
    {
        // arrange
        var studentId = Guid.NewGuid();
        var firstName = "john";
        var lastName = "smith";
        var firstNameUpdated = "john-Updated";
        var lastNameUpdated = "smith-Updated";

        // act
        await _services.Send(new CreateStudent.Command(studentId, firstName, lastName));
        await _services.Send(new UpdateStudentName.Command(studentId, firstNameUpdated, lastNameUpdated));

        // assert
        var result = await _services.Send(new GetStudent.Query(studentId));
        result.Id.Should().Be(studentId);
        result.FirstName.Should().Be(firstNameUpdated);
        result.LastName.Should().Be(lastNameUpdated);
    }
}