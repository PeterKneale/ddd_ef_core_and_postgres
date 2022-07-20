namespace Demo.Core.Domain;

public abstract class BaseEntity
{
    private readonly List<BaseEvent> _domainEvents = new();

    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}