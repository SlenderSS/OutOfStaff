using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.Projects;

public sealed record ProjectCreatedDomainEvent(Guid ProjectId, string Name, Guid ProjectManagerId) : IDomainEvent;

public sealed record ProjectCompletedDomainEvent(Guid ProjectId) : IDomainEvent;

public sealed record ProjectCancelledDomainEvent(Guid ProjectId) : IDomainEvent;

public sealed record ProjectCommentUpdatedDomainEvent(Guid ProjectId, string? Comment) : IDomainEvent;

public sealed record ProjectAssignmentCreatedDomainEvent(Guid AssignmentId, Guid EmployeeId, Guid ProjectId) : IDomainEvent;
public sealed record ProjectAssignmentClosedDomainEvent(Guid AssignmentId, Guid Id, Guid ProjectId) : IDomainEvent;

