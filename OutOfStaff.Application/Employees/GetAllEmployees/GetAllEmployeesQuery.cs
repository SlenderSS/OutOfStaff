using Microsoft.EntityFrameworkCore;
using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Application.Employees.GetAllEmployees;

public sealed record GetAllEmployeesQuery : IQuery<IEnumerable<EmployeeResponse>>;


public class GetAllEmployeesQueryHandler : IQueryHandler<GetAllEmployeesQuery, IEnumerable<EmployeeResponse>>
{
    private readonly IApplicationDbContext _applicationContext;

    public GetAllEmployeesQueryHandler(IApplicationDbContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task<Result<IEnumerable<EmployeeResponse>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
       List<EmployeeResponse> employees = await _applicationContext.Employees
            .Select(e => new EmployeeResponse
            {
                Id = e.Id,
                FullName = e.FullName,
                Email = e.Email,
                Position = e.Position.Name,
                Subdivision = e.Subdivision.Name,
                AvailableDayOffs = e.AvailableDayOffs,
                Status = e.Status.ToString()
                
            })
            .ToListAsync(cancellationToken);

        return employees;
    }
}