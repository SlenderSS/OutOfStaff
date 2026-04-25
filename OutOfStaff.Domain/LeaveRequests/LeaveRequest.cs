using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.LeaveRequests;

public class LeaveRequest : Entity
{
    private LeaveRequest() { }

    private LeaveRequest(
        Guid id,
        Guid employeeId,
        AbsenceReason absenceReason,
        DateOnly startDate,
        DateOnly endDate,
        string? comment) : base(id)
    {
        EmployeeId = employeeId;
        AbsenceReason = absenceReason;
        StartDate = startDate;
        EndDate = endDate;
        Comment = comment;
        Status = RequestStatus.New;
        SetCreated();
    }

    public Guid EmployeeId { get; private set; }
    public AbsenceReason AbsenceReason { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public RequestStatus Status { get; private set; }
    public string? Comment { get; private set; }

    public static Result<LeaveRequest> Create(
        Guid employeeId,
        AbsenceReason absenceReason,
        DateOnly startDate,
        DateOnly endDate,
        string? comment = null)
    {
        if (startDate > endDate)
            return Result.Failure<LeaveRequest>(
                Error.Validation("LeaveRequest.InvalidDateRange", "Start date must be before or equal to end date."));

        if (endDate < DateOnly.FromDateTime(DateTime.UtcNow))
            return Result.Failure<LeaveRequest>(
                Error.Validation("LeaveRequest.PastDate", "Leave request cannot be created for past dates."));

        var leaveRequest = new LeaveRequest(Guid.NewGuid(), employeeId, absenceReason, startDate, endDate, comment);
        leaveRequest.RaiseDomainEvent(new LeaveRequestSubmittedDomainEvent(leaveRequest.Id, employeeId));

        return Result.Success(leaveRequest);
    }

    public Result Approve(Guid approverId)
    {
        if (Status != RequestStatus.New)
            return Result.Failure(
                Error.Conflict("LeaveRequest.AlreadyProcessed", $"Leave request is already in '{Status}' status."));

        Status = RequestStatus.Approved;
        SetUpdated();
        RaiseDomainEvent(new LeaveRequestApprovedDomainEvent(Id, approverId));

        return Result.Success();
    }

    public Result Reject(Guid approverId, string reason)
    {
        if (Status != RequestStatus.New)
            return Result.Failure(
                Error.Conflict("LeaveRequest.AlreadyProcessed", $"Leave request is already in '{Status}' status."));

        Status = RequestStatus.Rejected;
        Comment = reason;
        SetUpdated();
        RaiseDomainEvent(new LeaveRequestRejectedDomainEvent(Id, approverId, reason));

        return Result.Success();
    }

    public Result Cancel(Guid employeeId)
    {
        if (Status == RequestStatus.Approved)
            return Result.Failure(
                Error.Conflict("LeaveRequest.CannotCancel", "Approved leave request cannot be cancelled directly."));

        if (EmployeeId != employeeId)
            return Result.Failure(
                Error.Conflict("LeaveRequest.NotOwner", "Only the owner can cancel this leave request."));

        Status = RequestStatus.Cancelled;
        SetUpdated();
        RaiseDomainEvent(new LeaveRequestCancelledDomainEvent(Id, employeeId));

        return Result.Success();
    }
}