using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.LeaveRequests;

public class LeaveRequest : Entity
{
    public Guid EmployeeId { get; set; }
    public AbsenceReason AbsenceReason { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public RequestStatus Status { get; set; }
    public string Comment { get; set; }
}