using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.ApprovalRequests;

public class ApprovalRequest : Entity
{
    public Guid LeaveRequestId { get; set; }
    public Guid ApproverId { get; set; }
    public RequestStatus Status { get; set; }
    public string Comment { get; set; }
    
}