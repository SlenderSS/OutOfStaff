namespace OutOfStaff.Application.Employees.GetAllEmployees;

public sealed class EmployeeResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Subdivision { get; set; }
    public string Status { get; set; }
    public string Position { get; set; }
    public byte AvailableDayOffs { get; set; }
}