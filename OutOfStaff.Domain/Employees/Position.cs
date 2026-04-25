using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.Employees;

public class Position : Entity
{
    private Position() { }

    private Position(Guid id, string name) : base(id)
    {
        Name = name;
        SetCreated();
    }

    public string Name { get; private set; } = string.Empty;

    public static Result<Position> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Position>(
                Error.Validation("Position.NameRequired", "Position name cannot be empty."));

        return Result.Success(new Position(Guid.NewGuid(), name));
    }

    public Result Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure(
                Error.Validation("Position.NameRequired", "Position name cannot be empty."));

        Name = newName;
        SetUpdated();
        return Result.Success();
    }
}