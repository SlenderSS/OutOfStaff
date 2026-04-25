using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.Employees;

public class Subdivision : Entity
{
    private Subdivision() { }

    private Subdivision(Guid id, string name) : base(id)
    {
        Name = name;
        SetCreated();
    }

    public string Name { get; private set; } = string.Empty;

    public static Result<Subdivision> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Subdivision>(
                Error.Validation("Subdivision.NameRequired", "Subdivision name cannot be empty."));

        return Result.Success(new Subdivision(Guid.NewGuid(), name));
    }

    public Result Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure(
                Error.Validation("Subdivision.NameRequired", "Subdivision name cannot be empty."));

        Name = newName;
        SetUpdated();
        return Result.Success();
    }
}