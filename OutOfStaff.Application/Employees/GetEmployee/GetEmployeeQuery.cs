using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Application.Employees.GetAllEmployees;
using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Application.Employees.GetEmployee;

public sealed record GetEmployeeQuery(string EmployeeId) : IQuery<EmployeeResponse>;

public sealed class GetEmployeeQueryHandler: IQueryHandler<GetEmployeeQuery, EmployeeResponse>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeeQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<EmployeeResponse>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    { 
        var employee = await _context.Employees.FindAsync([Guid.Parse(request.EmployeeId)], cancellationToken);
        if (employee == null)
        {
            throw new NullReferenceException($"Employee with id {request.EmployeeId} not found");
        }

        var employeeResponse = new EmployeeResponse()
        {
            Id = employee.Id,
            FullName = employee.FullName,
            Email = employee.Email,
            Position = employee.Position.Name,
            Subdivision = employee.Subdivision.Name,
            AvailableDayOffs = employee.AvailableDayOffs,
            Status = employee.Status.ToString()
        };
        return employeeResponse;
    }
}

