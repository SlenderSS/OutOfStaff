namespace OutOfStaff.Application.Abstractions.Data;

/// <summary>
/// Marker interface for the application database context.
/// Concrete implementations (EF Core, Cosmos, etc.) live in Infrastructure.
/// </summary>
public interface IApplicationDbContext : IUnitOfWork;

