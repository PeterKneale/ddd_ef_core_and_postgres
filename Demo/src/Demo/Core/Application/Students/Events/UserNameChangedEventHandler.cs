using Demo.Core.Domain.Students;

namespace Demo.Core.Application.Students.Events;

public class StudentNameChangedEventHandler : INotificationHandler<StudentNameChangedEvent>
{
    private readonly ILogger<StudentNameChangedEventHandler> _logger;

    public StudentNameChangedEventHandler(ILogger<StudentNameChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StudentNameChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Student Name Changed: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}