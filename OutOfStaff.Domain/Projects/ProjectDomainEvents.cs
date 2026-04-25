using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.Projects;

public sealed record ProjectCreatedDomainEvent(Guid ProjectId, string Name, Guid ProjectManagerId) : IDomainEvent;

public sealed record ProjectCompletedDomainEvent(Guid ProjectId) : IDomainEvent;

public sealed record ProjectCancelledDomainEvent(Guid ProjectId) : IDomainEvent;

