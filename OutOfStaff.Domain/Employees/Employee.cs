using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.Employees;

public class Employee : Entity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Subdivision Subdivision { get; set; }
    public Status Status { get; set; }
    public Position Position { get; set; }
    public byte AvailableDayOffs { get; set; }
}