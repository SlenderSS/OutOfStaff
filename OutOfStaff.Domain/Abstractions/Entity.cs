namespace OutOfStaff.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity( Guid id )
    {
        Id = id;
    }

    protected Entity()
    {
        
    }
    
    public Guid Id { get; init; }

    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent( IDomainEvent domainEvent )
    {
        _domainEvents.Add( domainEvent );
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void SetCreated() => CreatedAt = UpdatedAt = DateTimeOffset.UtcNow;

    protected void SetUpdated() => UpdatedAt = DateTimeOffset.UtcNow;
}