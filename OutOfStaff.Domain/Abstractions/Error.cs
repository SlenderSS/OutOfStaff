namespace OutOfStaff.Domain.Abstractions;

public record Error(string Code, string Name, ErrorType Type = ErrorType.Failure)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");

    public static Error NotFound(string resource, Guid id) =>
        new($"{resource}.NotFound", $"{resource} with id '{id}' was not found.", ErrorType.NotFound);

    public static Error Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);

    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);
}

public enum ErrorType
{
    None,
    Failure,
    NotFound,
    Validation,
    Conflict
}