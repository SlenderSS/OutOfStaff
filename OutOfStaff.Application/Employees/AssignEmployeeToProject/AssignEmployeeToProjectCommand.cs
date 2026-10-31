using System.Runtime.InteropServices.JavaScript;
using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;
using OutOfStaff.Domain.Projects;

namespace OutOfStaff.Application.Employees.AssignEmployeeToProject;

public sealed record AssignEmployeeToProjectCommand(Guid EmployeeId, Guid ProjectId, DateOnly StartDate): ICommand;

public sealed class AssignEmployeeToProjectCommandHandler : ICommandHandler<AssignEmployeeToProjectCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignEmployeeToProjectCommandHandler(
        IEmployeeRepository employeeRepository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AssignEmployeeToProjectCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            return Result.Failure(Error.NotFound(nameof(Employee), request.EmployeeId));

        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
            return Result.Failure(Error.NotFound(nameof(Project), request.ProjectId));
        
        var result = employee.AssignToProject(project, request.StartDate);
        
        if (!result.IsSuccess)
            return Result.Failure(result.Error);

        await _employeeRepository.UpdateAsync(employee, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}