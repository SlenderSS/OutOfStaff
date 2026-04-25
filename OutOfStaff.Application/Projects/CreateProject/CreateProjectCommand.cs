using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;
using OutOfStaff.Domain.Projects;

namespace OutOfStaff.Application.Projects.CreateProject;

public sealed record CreateProjectCommand(
    string Name,
    Guid ProjectManagerId,
    DateOnly StartDate,
    ProjectType ProjectType,
    string? Comment = null
) : ICommand<Guid>;

public sealed class CreateProjectCommandHandler(
    IProjectsRepository projectsRepository,
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProjectCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var managerExists = await employeeRepository.GetByIdAsync(request.ProjectManagerId, cancellationToken);
        if (managerExists is null)
            return Result.Failure<Guid>(Error.NotFound(nameof(Employee), request.ProjectManagerId));

        var result = Project.Create(
            request.Name,
            request.ProjectType,
            request.StartDate,
            request.ProjectManagerId,
            request.Comment);

        if (result.IsFailure)
            return Result.Failure<Guid>(result.Error);

        projectsRepository.Add(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
