using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.LeaveRequests;

public enum AbsenceReason
{
    SickLeave,
    Vacation,
    PersonalLeave,
    UnpaidLeave,
    Other
}