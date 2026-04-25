using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.ApprovalRequests;

public sealed record ApprovalRequestCreatedDomainEvent(Guid ApprovalRequestId, Guid LeaveRequestId, Guid ApproverId) : IDomainEvent;

public sealed record ApprovalRequestApprovedDomainEvent(Guid ApprovalRequestId, Guid LeaveRequestId, Guid ApproverId) : IDomainEvent;

public sealed record ApprovalRequestRejectedDomainEvent(Guid ApprovalRequestId, Guid LeaveRequestId, Guid ApproverId, string Comment) : IDomainEvent;

