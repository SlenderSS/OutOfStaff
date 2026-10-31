using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;

namespace OutOfStaff.Domain.Projects;

public class ProjectAssignment : Entity
{
    private ProjectAssignment() { }

    private ProjectAssignment(
        Guid id,
        Guid employeeId,
        Guid projectId,
        DateOnly startDate,
        DateOnly? endDate = null) : base(id)
    {
        EmployeeId = employeeId;
        ProjectId = projectId;
        StartDate = startDate;
        EndDate = endDate;
        SetCreated();
    }

    public Guid EmployeeId { get; private set; }
    public Guid ProjectId { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public Employee Employee { get; private set; } = null!;
    public Project Project { get; private set; } = null!;

    public static Result<ProjectAssignment> Create(
        Guid employeeId,
        Guid projectId,
        DateOnly startDate,
        DateOnly? endDate = null)
    {
        if (employeeId == Guid.Empty)
            return Result.Failure<ProjectAssignment>(Error.Validation("ProjectAssignment.EmployeeRequired", "Employee is required."));

        if (projectId == Guid.Empty)
            return Result.Failure<ProjectAssignment>(Error.Validation("ProjectAssignment.ProjectRequired", "Project is required."));

        if (endDate.HasValue && endDate < startDate)
            return Result.Failure<ProjectAssignment>(Error.Validation("ProjectAssignment.InvalidEndDate", "End date cannot be before start date."));

        return Result.Success(new ProjectAssignment(
            Guid.NewGuid(), employeeId, projectId, startDate, endDate));
    }

    public Result Close(DateOnly endDate)
    {
        if (EndDate.HasValue)
            return Result.Failure(Error.Conflict("ProjectAssignment.AlreadyClosed", "Project assignment is already closed."));

        if (endDate < StartDate)
            return Result.Failure(Error.Validation("ProjectAssignment.InvalidEndDate", "End date cannot be before start date."));

        EndDate = endDate;
        SetUpdated();
        return Result.Success();
    }
}