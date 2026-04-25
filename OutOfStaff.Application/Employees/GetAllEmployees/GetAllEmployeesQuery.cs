using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;

namespace OutOfStaff.Application.Employees.GetAllEmployees;

public sealed record GetAllEmployeesQuery : IQuery<IEnumerable<EmployeeResponse>>;

public sealed class GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    : IQueryHandler<GetAllEmployeesQuery, IEnumerable<EmployeeResponse>>
{
    public async Task<Result<IEnumerable<EmployeeResponse>>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.GetAllAsync(cancellationToken);

        var response = employees.Select(e => new EmployeeResponse(
            e.Id,
            e.FullName,
            e.Email,
            e.Position.Name,
            e.Subdivision.Name,
            e.Status.ToString(),
            e.AvailableDayOffs));

        return Result.Success(response);
    }
}