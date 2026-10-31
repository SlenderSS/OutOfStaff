using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;

namespace OutOfStaff.Application.Employees.DeactivateEmployee;

public sealed record DeactivateEmployeeCommand(Guid EmployeeId) : ICommand;

public sealed class DeactivateEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeactivateEmployeeCommand>
{
    public async Task<Result> Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            return Result.Failure(Error.NotFound(nameof(Employee), request.EmployeeId));

        var result = employee.Deactivate();
        if (result.IsFailure)
            return result;

        await employeeRepository.UpdateAsync(employee, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}