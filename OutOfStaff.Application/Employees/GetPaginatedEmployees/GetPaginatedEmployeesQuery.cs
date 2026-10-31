using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;

namespace OutOfStaff.Application.Employees.GetPaginatedEmployees;

public sealed record GetPaginatedEmployeesQuery(int Page, int PageSize) : IQuery<IReadOnlyList<Employee>>;

public sealed class GetPaginatedEmployeesQueryHandler : IQueryHandler<GetPaginatedEmployeesQuery, IReadOnlyList<Employee>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetPaginatedEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<IReadOnlyList<Employee>>> Handle(GetPaginatedEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetPaginatedEmployeesAsync(request.Page, request.PageSize, cancellationToken);
        return Result.Success(employees);
    }
}