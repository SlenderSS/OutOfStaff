using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Application.Employees.RegistryEmployee;

public sealed record RegistryEmployeeCommand(
    string FullName, 
    string Email, 
    string Password,
    Guid PositionId,
    Guid SubdivisionId
    ) : ICommand<Guid>;


public sealed class RegistryEmployeeCommandHandler(IApplicationDbContext context)
    : ICommandHandler<RegistryEmployeeCommand, Guid>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<Guid>> Handle(RegistryEmployeeCommand request, CancellationToken cancellationToken)
    {
        // TODO : Implement logic for check if employee already exists
        
        var positionTask =  _context.Positions.FindAsync([request.PositionId], cancellationToken);
        var subdivisionTask =  _context.Subdivisions.FindAsync([request.SubdivisionId], cancellationToken);
        await Task.WhenAll(positionTask.AsTask(), subdivisionTask.AsTask());
        var position = positionTask.Result;
        var subdivision = subdivisionTask.Result;
        
        Employee employee = new Employee
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password,
            Position = position,
            Subdivision = subdivision,
            Status = Status.Active,
            AvailableDayOffs = 20
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
        
    }
}