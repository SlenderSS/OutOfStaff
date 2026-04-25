using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.Projects;

public class Project : Entity
{
    private Project() { }

    private Project(
        Guid id,
        string name,
        ProjectType projectType,
        DateOnly startDate,
        Guid projectManagerId,
        string? comment) : base(id)
    {
        Name = name;
        ProjectType = projectType;
        StartDate = startDate;
        ProjectManagerId = projectManagerId;
        Comment = comment;
        Status = Status.Active;
        SetCreated();
    }

    public string Name { get; private set; } = string.Empty;
    public ProjectType ProjectType { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public Guid ProjectManagerId { get; private set; }
    public string? Comment { get; private set; }
    public Status Status { get; private set; }

    public static Result<Project> Create(
        string name,
        ProjectType projectType,
        DateOnly startDate,
        Guid projectManagerId,
        string? comment = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Project>(
                Error.Validation("Project.NameRequired", "Project name cannot be empty."));

        if (projectManagerId == Guid.Empty)
            return Result.Failure<Project>(
                Error.Validation("Project.ManagerRequired", "Project manager is required."));

        var project = new Project(Guid.NewGuid(), name, projectType, startDate, projectManagerId, comment);
        project.RaiseDomainEvent(new ProjectCreatedDomainEvent(project.Id, name, projectManagerId));

        return Result.Success(project);
    }

    public Result Complete(DateOnly endDate)
    {
        if (Status == Status.Inactive)
            return Result.Failure(
                Error.Conflict("Project.AlreadyClosed", "Project is already closed."));

        if (endDate < StartDate)
            return Result.Failure(
                Error.Validation("Project.InvalidEndDate", "End date cannot be before start date."));

        EndDate = endDate;
        Status = Status.Inactive;
        SetUpdated();
        RaiseDomainEvent(new ProjectCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel(string? reason = null)
    {
        if (Status == Status.Inactive)
            return Result.Failure(
                Error.Conflict("Project.AlreadyClosed", "Project is already closed."));

        Comment = reason ?? Comment;
        Status = Status.Inactive;
        SetUpdated();
        RaiseDomainEvent(new ProjectCancelledDomainEvent(Id));

        return Result.Success();
    }

    public Result UpdateComment(string? comment)
    {
        Comment = comment;
        SetUpdated();
        return Result.Success();
    }
}