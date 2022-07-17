using Demo.Core.Application;
using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Common;
using Demo.Core.Domain.Students;
using Demo.Core.Infrastructure.Repository;

namespace Demo.IntegrationTests.Infrastructure.Database;

[Collection(nameof(DatabaseCollection))]
public class GetStudentTests : IClassFixture<ServicesFixture>
{
    private readonly IServiceProvider _services;

    public GetStudentTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services.ServiceProvider;
    }

    [Fact]
    public async Task Added_student_can_be_retrieved_with_get()
    {
        // arrange
        await using var scope1 = _services.CreateAsyncScope();
        var repo = scope1.ServiceProvider.GetRequiredService<IStudentRepository>();
        var db = scope1.ServiceProvider.GetRequiredService<DatabaseContext>();
        var studentId = StudentId.CreateInstance(Guid.NewGuid());
        var name = new Name("john", "smith");

        // act
        var student = Student.CreateInstance(studentId, name);
        await repo.Add(student);
        await db.SaveChangesAsync();

        // assert
        await using var scope2 = _services.CreateAsyncScope();
        var repo2 = scope2.ServiceProvider.GetRequiredService<IStudentRepository>();
        var student2 = await repo2.Get(studentId);
        student2.Should().BeEquivalentTo(student);
    }
}