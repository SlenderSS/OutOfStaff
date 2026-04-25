namespace OutOfStaff.Application.Employees;

public sealed record EmployeeResponse(
    Guid Id,
    string FullName,
    string Email,
    string Position,
    string Subdivision,
    string Status,
    byte AvailableDayOffs);
