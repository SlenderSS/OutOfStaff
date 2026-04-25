using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Application.Employees.DeactivateEmployee;

public sealed record DeactivateEmployeeCommand(string EmployeeId) : ICommand;

public sealed class DeactivateEmployeeCommandHandler : ICommandHandler<DeactivateEmployeeCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeactivateEmployeeCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result> Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _applicationDbContext.Employees.FindAsync(request.EmployeeId, cancellationToken);

        if (employee is null)
            return Result.Failure(new Error("404",  "Employee not found."));

        employee.Status = Status.Inactive;

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}