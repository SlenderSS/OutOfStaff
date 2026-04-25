using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.LeaveRequests;

public sealed record LeaveRequestSubmittedDomainEvent(Guid LeaveRequestId, Guid EmployeeId) : IDomainEvent;

public sealed record LeaveRequestApprovedDomainEvent(Guid LeaveRequestId, Guid ApproverId) : IDomainEvent;

public sealed record LeaveRequestRejectedDomainEvent(Guid LeaveRequestId, Guid ApproverId, string Reason) : IDomainEvent;

public sealed record LeaveRequestCancelledDomainEvent(Guid LeaveRequestId, Guid EmployeeId) : IDomainEvent;

