using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;

namespace OutOfStaff.Application.Employees.RegistryEmployee;

public sealed record RegistryEmployeeCommand(
    string FullName,
    string Email,
    Guid PositionId,
    Guid SubdivisionId
) : ICommand<Guid>;

public sealed class RegistryEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IPositionRepository positionRepository,
    ISubdivisionRepository subdivisionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegistryEmployeeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegistryEmployeeCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await employeeRepository.ExistsByEmailAsync(request.Email, cancellationToken);
        if (emailExists)
            return Result.Failure<Guid>(
                Error.Conflict("Employee.EmailTaken", $"An employee with email '{request.Email}' already exists."));

        var position = await positionRepository.GetByIdAsync(request.PositionId, cancellationToken);
        if (position is null)
            return Result.Failure<Guid>(Error.NotFound(nameof(Position), request.PositionId));

        var subdivision = await subdivisionRepository.GetByIdAsync(request.SubdivisionId, cancellationToken);
        if (subdivision is null)
            return Result.Failure<Guid>(Error.NotFound(nameof(Subdivision), request.SubdivisionId));

        var result = Employee.Register(request.FullName, request.Email, position, subdivision);
        if (result.IsFailure)
            return Result.Failure<Guid>(result.Error);

        await employeeRepository.AddAsync(result.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}