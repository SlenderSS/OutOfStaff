using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;

namespace OutOfStaff.Application.Employees.GetEmployee;

public sealed record GetEmployeeQuery(Guid EmployeeId) : IQuery<EmployeeResponse>;

public sealed class GetEmployeeQueryHandler(IEmployeeRepository employeeRepository)
    : IQueryHandler<GetEmployeeQuery, EmployeeResponse>
{
    public async Task<Result<EmployeeResponse>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            return Result.Failure<EmployeeResponse>(Error.NotFound(nameof(Employee), request.EmployeeId));

        return Result.Success(new EmployeeResponse(
            employee.Id,
            employee.FullName,
            employee.Email,
            employee.Position.Name,
            employee.Subdivision.Name,
            employee.Status.ToString(),
            employee.AvailableDayOffs));
    }
}
