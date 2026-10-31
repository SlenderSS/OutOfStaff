using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;
using OutOfStaff.Domain.Projects;

namespace OutOfStaff.Application.Employees.UnassignFromProject;

public sealed record UnassignEmployeeFromProject(Guid EmployeeId, Guid ProjectId, DateOnly EndDate) : ICommand;

public sealed class UnassignEmployeeFromProjectCommandHandler(
    IEmployeeRepository employeeRepository,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UnassignEmployeeFromProject>
{
    public async Task<Result> Handle(UnassignEmployeeFromProject request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            return Result.Failure(Error.NotFound(nameof(Employee), request.EmployeeId));

        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
            return Result.Failure(Error.NotFound(nameof(Project), request.ProjectId));

        var result = employee.UnassignFromProject(project.Id, request.EndDate);
        if (!result.IsSuccess)
            return Result.Failure(result.Error);

        await employeeRepository.UpdateAsync(employee, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}