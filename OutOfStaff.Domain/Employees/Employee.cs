using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.Employees;

public class Employee : Entity
{
    private const byte DefaultAvailableDayOffs = 20;

    private Employee() { }

    private Employee(
        Guid id,
        string fullName,
        string email,
        Position position,
        Subdivision subdivision) : base(id)
    {
        FullName = fullName;
        Email = email;
        Position = position;
        Subdivision = subdivision;
        Status = Status.Active;
        AvailableDayOffs = DefaultAvailableDayOffs;
        SetCreated();
    }

    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public Position Position { get; private set; } = null!;
    public Subdivision Subdivision { get; private set; } = null!;
    public Status Status { get; private set; }
    public byte AvailableDayOffs { get; private set; }

    public static Result<Employee> Register(
        string fullName,
        string email,
        Position position,
        Subdivision subdivision)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return Result.Failure<Employee>(
                Error.Validation("Employee.FullNameRequired", "Full name cannot be empty."));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            return Result.Failure<Employee>(
                Error.Validation("Employee.InvalidEmail", "A valid email address is required."));

        if (position is null)
            return Result.Failure<Employee>(
                Error.Validation("Employee.PositionRequired", "Position is required."));

        if (subdivision is null)
            return Result.Failure<Employee>(
                Error.Validation("Employee.SubdivisionRequired", "Subdivision is required."));

        var employee = new Employee(Guid.NewGuid(), fullName, email, position, subdivision);
        employee.RaiseDomainEvent(new EmployeeRegisteredDomainEvent(employee.Id, email));

        return Result.Success(employee);
    }

    public Result Deactivate()
    {
        if (Status == Status.Inactive)
            return Result.Failure(
                Error.Conflict("Employee.AlreadyInactive", "Employee is already inactive."));

        Status = Status.Inactive;
        SetUpdated();
        RaiseDomainEvent(new EmployeeDeactivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result Activate()
    {
        if (Status == Status.Active)
            return Result.Failure(
                Error.Conflict("Employee.AlreadyActive", "Employee is already active."));

        Status = Status.Active;
        SetUpdated();
        RaiseDomainEvent(new EmployeeActivatedDomainEvent(Id));

        return Result.Success();
    }

    public Result UseAvailableDayOff(byte days)
    {
        if (Status == Status.Inactive)
            return Result.Failure(
                Error.Conflict("Employee.Inactive", "Cannot use day-offs for an inactive employee."));

        if (days > AvailableDayOffs)
            return Result.Failure(
                Error.Validation("Employee.InsufficientDayOffs",
                    $"Requested {days} days but only {AvailableDayOffs} available."));

        AvailableDayOffs -= days;
        SetUpdated();
        return Result.Success();
    }

    public Result RestoreDayOffs(byte days)
    {
        AvailableDayOffs += days;
        SetUpdated();
        return Result.Success();
    }
}