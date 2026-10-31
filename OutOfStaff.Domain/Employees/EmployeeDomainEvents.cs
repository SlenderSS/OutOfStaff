using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.Employees;

public sealed record EmployeeRegisteredDomainEvent(Guid EmployeeId, string Email) : IDomainEvent;

public sealed record EmployeeDeactivatedDomainEvent(Guid EmployeeId) : IDomainEvent;

public sealed record EmployeeActivatedDomainEvent(Guid EmployeeId) : IDomainEvent;

