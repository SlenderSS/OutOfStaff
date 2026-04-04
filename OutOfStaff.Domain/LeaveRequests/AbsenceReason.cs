using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.LeaveRequests;

public class AbsenceReason : Entity
{
    public string Type { get; set; }
    public string Description { get; set; }
}