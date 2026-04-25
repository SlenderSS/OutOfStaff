using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.ApprovalRequests;

public class ApprovalRequest : Entity
{
    private ApprovalRequest() { }

    private ApprovalRequest(Guid id, Guid leaveRequestId, Guid approverId) : base(id)
    {
        LeaveRequestId = leaveRequestId;
        ApproverId = approverId;
        Status = RequestStatus.New;
        SetCreated();
    }

    public Guid LeaveRequestId { get; private set; }
    public Guid ApproverId { get; private set; }
    public RequestStatus Status { get; private set; }
    public string? Comment { get; private set; }

    public static ApprovalRequest Create(Guid leaveRequestId, Guid approverId)
    {
        var approvalRequest = new ApprovalRequest(Guid.NewGuid(), leaveRequestId, approverId);
        approvalRequest.RaiseDomainEvent(
            new ApprovalRequestCreatedDomainEvent(approvalRequest.Id, leaveRequestId, approverId));
        return approvalRequest;
    }

    public Result Approve(string? comment = null)
    {
        if (Status != RequestStatus.New)
            return Result.Failure(
                Error.Conflict("ApprovalRequest.AlreadyProcessed", $"Approval request is already in '{Status}' status."));

        Status = RequestStatus.Approved;
        Comment = comment;
        SetUpdated();
        RaiseDomainEvent(new ApprovalRequestApprovedDomainEvent(Id, LeaveRequestId, ApproverId));

        return Result.Success();
    }

    public Result Reject(string comment)
    {
        if (Status != RequestStatus.New)
            return Result.Failure(
                Error.Conflict("ApprovalRequest.AlreadyProcessed", $"Approval request is already in '{Status}' status."));

        if (string.IsNullOrWhiteSpace(comment))
            return Result.Failure(
                Error.Validation("ApprovalRequest.CommentRequired", "A reason is required when rejecting a request."));

        Status = RequestStatus.Rejected;
        Comment = comment;
        SetUpdated();
        RaiseDomainEvent(new ApprovalRequestRejectedDomainEvent(Id, LeaveRequestId, ApproverId, comment));

        return Result.Success();
    }
}